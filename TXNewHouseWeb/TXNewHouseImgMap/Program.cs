using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TXBll.HouseData;
using TXCommons;
using TXCommons.PictureModular;
using TXModel.AdminPVM;

namespace TXNewHouseImgMap
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("开始获取，请稍等。。");
            GetPremisesMapImg();
            Console.WriteLine("完成");
            Console.ReadKey();

        }

        private static List<int> GetAllHasPremisesMap()
        {
            string sql = "SELECT PermitID FROM dbo.PremiseImgMap(NOLOCK) GROUP BY PermitID ";
            DataSet ds = SqlHelper.ExecuteDataset(constr, CommandType.Text, sql);
            List<int> hasids = new List<int>();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        hasids.Add(Convert.ToInt32(row[0]));
                    }
                }
            }
            return hasids;
        }
        static string constr = System.Configuration.ConfigurationSettings.AppSettings["kyj_NewHouseDBEntities"];

        private static void GetPremisesMapImg()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                List<int> hasmappremise = GetAllHasPremisesMap();
                var list = new PremiseImgMapBll().GetAllPremisesList();
                if (list != null && list.Count > 0)
                {
                    int sumimg = 0;
                    int sumpcount = 0;
                    int haspcount = 0;
                    int noimg = 0;
                    Console.WriteLine(string.Format("获取到{0}个楼盘，开始处理", list.Count));
                    foreach (var v in list)
                    {
                        var premise = new PremisesBll().GetPVM_PremisesById(v.Id);
                        if (premise != null && !string.IsNullOrWhiteSpace(premise.InnerCode))
                        {
                            PVE_NH_PremisesAllImg<PremisesPictureInfo> model =
                                new PVE_NH_PremisesAllImg<PremisesPictureInfo>();
                            model.PremisesName = premise.Name;
                            model.PremisesID = premise.Id;
                            model.InnerCode = premise.InnerCode;
                            model.CityID = premise.CityId;
                            sumpcount++;
                            if (!hasmappremise.Contains(model.PremisesID))
                            {
                                Console.WriteLine(string.Format("{2} 开始获取楼盘（{1}）：{0} 图片", model.PremisesName,
                                                                model.PremisesID, sumpcount));
                                List<PremisesPictureInfo> listPremisesPictureOrder =
                                    GetPicture.GetPremisesPictureOrder(premise.InnerCode, true, premise.CityId);

                                if (null != listPremisesPictureOrder && listPremisesPictureOrder.Count > 0)
                                {
                                    model.ImgList = listPremisesPictureOrder;
                                    foreach (var m in model.ImgList)
                                    {
                                        sumimg++;
                                        sb.AppendFormat(@";INSERT INTO dbo.PremiseImgMap
                                               ( CityID ,
                                                 PermitID ,
                                                 PremiseName ,
                                                 InnerCode ,
                                                 ImgID ,
                                                 ImgTitle ,
                                                 ImgDes ,
                                                 ImgPath ,
                                                 PictureType ,
                                                 PictureTypeName ,
                                                 ImgIsDel ,
                                                 CreateDate
                                               )
                                       VALUES  ( {0} , -- CityID - int
                                                 {1} , -- PermitID - int
                                                 '{2}' , -- PremiseName - varchar(500)
                                                 '{3}' , -- InnerCode - varchar(500)
                                                 {4} , -- ImgID - int
                                                 '{5}' , -- ImgTitle - varchar(500)
                                                 '{6}' , -- ImgDes - text
                                                 '{7}' , -- ImgPath - varchar(500)
                                                 '{8}' , -- PictureType - varchar(100)
                                                 '{9}' , -- PictureTypeName - varchar(100)
                                                 {10} , -- ImgIsDel - bit
                                                
                                                 getdate()  -- CreateDate - datetime
                                               )", model.CityID, model.PremisesID, model.PremisesName.ReplaceEngRednik(), model.InnerCode,
                                                        m.ID,
                                                        m.Title.ReplaceEngRednik(), m.Desc.ReplaceEngRednik(), m.Path.ReplaceEngRednik(), m.PictureType.ReplaceEngRednik(), m.PictureTypeName.ReplaceEngRednik(),
                                                        0);

                                    }


                                    SqlHelper.ExecuteNonQuery(constr, CommandType.Text, sb.ToString());
                                    sb.Clear();
                                    Console.WriteLine(string.Format("插入楼盘：{0} 图片信息：{1}条", model.PremisesName,
                                                                    model.ImgList.Count));
                                }
                                else
                                {
                                    noimg++;
                                    Console.WriteLine(string.Format("没获取到 楼盘({0}):{1} 图片", model.PremisesID, model.PremisesName));
                                }
                            }
                            else
                            {
                                haspcount++;
                                Console.WriteLine(string.Format("楼盘：{0} 已处理过,跳过", model.PremisesName));
                            }
                        }
                    }
                    Console.WriteLine(string.Format("共{0}个楼盘，已存在跳过{1}个楼盘,新插入{2}个楼盘图片信息，{3}个楼盘没有获取到图片,", list.Count,
                                                    haspcount, sumimg, noimg));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }

}

