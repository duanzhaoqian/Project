using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Timers;
using TXCommons.PictureModular;
using System.IO;
using ServiceStack;
using Common;
namespace DeleteHouseDala
{
    //    关于删除过期房源的需求
    //1、软删除
    //    正常经济人：
    //        租房，经纪人在指定下架时间自动为该房源下架（每天晚上12点更新下架服务），updatetime未操作天数达到30天的，服务自动IsDel=1，Updatetime=getdate（）
    //        二手房，经纪人在指定下架时间自动为该房源下架（每天晚上12点更新下架服务），updatetime未操作天数达到50天的，服务自动IsDel=1，Updatetime=getdate（）
    //    采集房源：
    //        租房、二手房采集过来时下架时间默认是7天（每天晚上12点更新下架服务），通过fromurl列来判断出采集房源+updatetime>=3天的房源，服务自动IsDel=1，Updatetime=getdate（）
    //2、物理删除
    //    经纪人房源+采集房源已经删除IsDel=1，Updatetime超过3天的，实行物理删除（数据库相关数据（s_longhouse表，S_LongHouseOther表，S_LongHouseBidPrice表），Redis数据，物理图片数据）
    public partial class TXDeleteHouseData : ServiceBase
    {
        Dal.DeleteHouseDataDal dal = null;
        private int LongHouseDay = 0;
        private int SecHouseDay = 0;
        private int DelHouseDay = 0;
        protected string LogPath = string.Empty;

        private int StartHour = 0;
        private int StartMinute = 0;
        private int DelCJLongHouseDay = 0;
        private System.Timers.Timer timerHour = new System.Timers.Timer();
        public TXDeleteHouseData()
        {

        }

        protected override void OnStart(string[] args)
        {
            //Thread.Sleep(20000);
            string s = string.Empty;
            InitializeComponent();
            dal = new Dal.DeleteHouseDataDal();
            LongHouseDay = St.ToInt32(St.ConfigKey("LongHouseDay"), 1000);
            SecHouseDay = St.ToInt32(St.ConfigKey("SecHouseDay"), 1000);
            DelHouseDay = St.ToInt32(St.ConfigKey("DelHouseDay"), 1000);
            LogPath = ConfigurationManager.AppSettings["LogPath"];
            StartHour = St.ToInt32(St.ConfigKey("StatrTime").Split(':')[0]);
            StartMinute = St.ToInt32(St.ConfigKey("StatrTime").Split(':')[1]);
            DelCJLongHouseDay = St.ToInt32(St.ConfigKey("DelCJLongHouseDay"), 1000);
            timerHour.Elapsed += new ElapsedEventHandler(timerHour_Elapsed);
            timerHour.Interval = 1000 * 60;//* 60
            timerHour.AutoReset = true;
            timerHour.Enabled = true;
            string message = "LongHouseDay:" + LongHouseDay + @"\r\n" +
                           "SecHouseDay:" + SecHouseDay + @"\r\n" +
                           "DelHouseDay:" + DelHouseDay + @"\r\n" +
                           "LogPath:" + LogPath + @"\r\n";
            TXCommons.Refurbish.LogTool.Log("删除房源服务启动:", message, LogPath);
        }
        #region  小时定时处理
        protected void timerHour_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Now.Hour == StartHour && DateTime.Now.Minute == StartMinute)//
            {
                UpdateCollectionHouseIsDelete();//修改采集房源删除状态
                UpdateIsDeleteLongHouse(0);//修改出租房删除状态
                UpdateIsDeleteLongHouse(1);//修改出二手房删除状态
                DeleteLongHouse();//物理删除房源
            }
        }
        #endregion

        /// <summary>
        /// 修改房源删除状态
        /// </summary>
        public void UpdateIsDeleteLongHouse(int type)
        {
            string typestring = type == 0 ? "租房" : "二手房";
            try
            {
                DataTable dt = dal.GetWillDeleteData(LongHouseDay, type);
                TXCommons.Refurbish.LogTool.Log("删除" + typestring + "房源服务数量", dt.Rows.Count.ToString(), LogPath);
                foreach (DataRow row in dt.Rows)
                {
                    TXCommons.Refurbish.LogTool.Log("删除" + typestring + "房源Id", row["Id"].ToString().Trim(), LogPath);
                    dal.UpdateIsDelete(row["Id"].ToString().Trim());
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log(typestring + "房源服务错误", ex.Message, LogPath);
            }
        }

        /// <summary>
        /// 修改采集房源删除状态
        /// </summary>
        public void UpdateCollectionHouseIsDelete()
        {
            try
            {
                DataTable dt = dal.GetCollectionDeleteData(DelCJLongHouseDay);
                TXCommons.Refurbish.LogTool.Log("删除采集房源服务数量", dt.Rows.Count.ToString(), LogPath);
                foreach (DataRow row in dt.Rows)
                {
                    TXCommons.Refurbish.LogTool.Log("删除采集房源Id", row["Id"].ToString().Trim(), LogPath);
                    int id = St.ToInt32(row["Id"].ToString().Trim());
                    dal.UpdateIsDelete(id.ToString());
                    if (St.ToInt32(row["state"]) == 1 && St.ToInt32(row["AuthenticationState"]) == 1)
                    {
                        //如果是上架审核通过的房源需要发送MQ把线上的房源下架
                        TXCommons.MsgQueue.SendMessage.SendLongHouseIndexMessage("update", id, St.ToInt32(row["cityid"]));
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("采集房源服务错误", ex.Message, LogPath);
            }
        }

        /// <summary>
        /// 物理删除房源
        /// </summary>
        public void DeleteLongHouse()
        {
            try
            {
                DataTable dt = dal.GetDeleteData(DelHouseDay);
                TXCommons.Refurbish.LogTool.Log("物理删除源服务数量", dt.Rows.Count.ToString(), LogPath);
                foreach (DataRow row in dt.Rows)
                {
                    TXCommons.Refurbish.LogTool.Log("物理删除房源Id", row["Id"].ToString().Trim(), LogPath);
                    dal.DeleteHouseData(row["Id"].ToString().Trim());
                    DeleteImages(row["InnerCode"].ToString().Trim(), int.Parse(row["CityId"].ToString()));
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("删除房源服务错误", ex.Message, LogPath);
            }
        }
        /// <summary>
        /// 删除房源图片
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="cityId"></param>
        public void DeleteImages(string guid, int cityId)
        {
            DeleteRentHousePicture(guid, RentHousePictureType.LONGLIST.ToString(), cityId);

            DeleteRentHousePicture(guid, RentHousePictureType.INNER.ToString(), cityId);

            DeleteRentHousePicture(guid, RentHousePictureType.ROOMTYPE.ToString(), cityId);

            DeleteRentHousePicture(guid, RentHousePictureType.VILLAGE.ToString(), cityId);

            DeleteRentHousePicture(guid, RentHousePictureType.TRAFFIC.ToString(), cityId);

            DeleteRentHousePicture(guid, RentHousePictureType.BEDROOM.ToString(), cityId);

            DeleteRentHousePicture(guid, RentHousePictureType.LIVINGROOM.ToString(), cityId);

            DeleteRentHousePicture(guid, RentHousePictureType.KITCHEN.ToString(), cityId);

            DeleteRentHousePicture(guid, RentHousePictureType.AROUND.ToString(), cityId);

            DeleteRentHousePicture(guid, RentHousePictureType.OTHER.ToString(), cityId);
        }

        /// <summary>
        /// 删除所有图片
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="id"></param>
        /// <param name="picturetype"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public bool DeleteRentHousePicture(string guid, string picturetype, int cityId)
        {
            bool result = false;
            try
            {
                TXCommons.Refurbish.LogTool.Log("删除房源图片类型:", picturetype, LogPath);
                List<RentHousePictureInfo> list = GetPicture.GetRentHousePictureListNoFilter(guid, false, picturetype, cityId);
                if (list != null && list.Count > 0)
                {
                    foreach (RentHousePictureInfo info in list)
                    {
                        string key = KeyManager.GetRentHousePictureKey(guid, picturetype);
                        TXCommons.Refurbish.LogTool.Log("删除房源图片key:", key, LogPath);
                        if (DeleteImages(Redis.IMGPATH_BASE + info.Path, Thumbnail.GetRentHousePictureThumbnail(picturetype)))
                        {
                            result = Redis.RemoveItemFromSortedSet<RentHousePictureInfo>(key, info, FunctionTypeEnum.HouseImage, cityId);
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("删除房源图片错误", ex.Message, LogPath);
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="path"></param>
        /// <param name="thumbnails"></param>
        /// <returns></returns>
        public bool DeleteImages(string path, string[] thumbnails)
        {
            try
            {
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    file.Delete();
                }
                TXCommons.Refurbish.LogTool.Log("删除房源图片路径:", path + ".180_130.jpg", LogPath);
                file = new FileInfo(path + ".180_130.jpg");
                if (file.Exists)
                {
                    file.Delete();
                }
                if (thumbnails != null)
                {
                    for (int i = 0; i < thumbnails.Length; i++)
                    {
                        string nextpath = path + "." + thumbnails[i] + ".jpg";
                        TXCommons.Refurbish.LogTool.Log("删除房源图片路径:", nextpath, LogPath);
                        file = new FileInfo(nextpath);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }

                }
                return true;
            }
            catch (Exception ex)
            {

                TXCommons.Refurbish.LogTool.Log("删除房源图片错误", ex.Message, LogPath);
                return false;
            }
        }

        protected override void OnStop()
        {

        }
    }
}
