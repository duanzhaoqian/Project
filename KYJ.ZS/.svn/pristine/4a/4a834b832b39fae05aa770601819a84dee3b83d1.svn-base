using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using KYJ.ZS.Commons.Common;
using KYJ.ZS.Commons.PictureModular;

namespace KYJ.ZS.GoodsPictureService
{
    public partial class GoodsService : ServiceBase
    {
        protected static string MQIPAddress = string.Empty;
        protected static string MQQueueName = string.Empty;
        protected static string MQConnectionTimeOut = string.Empty;
        protected static string MQRetryCount = string.Empty;
        protected static string LogPath = string.Empty;
        protected static int ThreadCount = 1;

        //静态词典
        protected static Dictionary<string, int> dictionary = new Dictionary<string, int>();
        
        public GoodsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //Thread.Sleep(20000);
            LogPath = ConfigurationManager.AppSettings["LogPath"];
            ThreadCount = int.Parse(ConfigurationManager.AppSettings["ThreadCount"]);
            MQQueueName = ConfigurationManager.AppSettings["MQQueueName"];
            MQIPAddress = ConfigurationManager.AppSettings["MQIPAddress"];

            MQ.ConsumerConfiguration.IPAddress = MQIPAddress;
            MQ.ConsumerConfiguration.ConnectionTimeOut = new TimeSpan(0, 0, Convert.ToInt32(ConfigurationManager.AppSettings["MQConnectionTimeOut"]));
            MQ.ConsumerConfiguration.RetryCount = Convert.ToInt32(ConfigurationManager.AppSettings["MQRetryCount"]);
            Log("Info", "租售商品图片服务启动");
            CreateThread();
            Console.Read();
            //InitializeComponent();
        }

        protected override void OnStop()
        {
        }


        //创建线程
        private static void CreateThread()
        {
            try
            {
                for (int i = 0; i < ThreadCount; i++)
                {
                    Thread thread = new Thread(new ThreadStart(ReceiveMessage));
                    thread.Start();
                }
            }
            catch (Exception ex)
            {
                Log("Error", "CreateThread():" + ex.Message);
            }
        }



        static void ReceiveMessage()
        {
            try
            {
                MQ.IQueueConsumer queueConsumer = MQ.ConsumerFactory.CreateQueueConsumer(MQQueueName);
                while (true)
                {
                    Dictionary<string, object> messageBody = new Dictionary<string, object>();
                    try
                    {
                        messageBody = queueConsumer.ReceiveMapMessage();
                        if (messageBody != null && string.IsNullOrEmpty(queueConsumer.ErrorMessage))
                        {
                            int MethodType = Convert.ToInt32(messageBody["MethodType"]);
                            ArrayList MethodParameter = (ArrayList)messageBody["MethodParameter"];
                            string logMsg = string.Empty;
                            string guid = string.Empty;
                            string goodsid = string.Empty;
                            string picturetype = string.Empty;
                            switch (MethodType)
                            {
                                //商品图片尺寸处理
                                case 1:
                                    //GUID
                                    guid = Convert.ToString(MethodParameter[0]);
                                    //商品ID
                                    goodsid = Convert.ToString(MethodParameter[1]);
                                    //图片类型
                                    picturetype = Convert.ToString(MethodParameter[2]);
                                    //fullpath
                                    string fullpath = Convert.ToString(MethodParameter[3]);
                                    bool isindex = Convert.ToBoolean(MethodParameter[4]);



                                    var message = "update" + "," + goodsid;
                                    Log("Info", "接收消息:商品ID：" + goodsid + "　GUID:" + guid);
                                        try
                                        {
                                            GoodsInfo info = null;
                                            List<GoodsInfo> list = null;
                                            string path = string.Empty;
                                            if (!string.IsNullOrEmpty(fullpath))
                                            {
                                                path = fullpath;
                                            }
                                            else
                                            {
                                                list = GetPicture.GetGoodsPictureList(guid, false, picturetype);
                                                if (list.Count > 1)
                                                {
                                                    info = list[0];
                                                    path = RedisTool.ImgpathBase + info.Path;
                                                }
                                            }
                                            if (null != info || !string.IsNullOrEmpty(path))
                                            {

                                                UploadPicture.DeleteGoodsListPicture(guid, picturetype);
                                                /////////////////////////////////////////////////
                                                Thumbnail.ThumbnailGoodsPicture(path, picturetype);
                                                /////////////////////////////////////////////////
                                                //if (null != info)
                                                //{
                                                //    string key2 = KeyManager.GetPremisesPictureKey(guid,
                                                //        PremisesPictureType.List.ToString());
                                                //    info.IsNew = 0;
                                                //    Redis.AddItemToSortedSet<PremisesPictureInfo>(info, key2, 1, null,
                                                //        FunctionTypeEnum.NewHouseBasicDataPicture, cityId);
                                                //    logMsg = "列表图已生成";
                                                //}
                                            }
                                            else
                                            {
                                                logMsg = "列表图未生成";
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            logMsg = "接收消息并执行方法MakeLongRentHouseThumbnail出现异常，参数guid=" + guid + "　:" + ex.Message;
                                            Log("Info", logMsg);
                                            if (!string.IsNullOrEmpty(goodsid))
                                            {
                                                if (!dictionary.ContainsKey(goodsid))
                                                {
                                                    MQ.IQueueProducer queueProducer =
                                                        MQ.ProducerFactory.CreateQueueProducer(MQQueueName);
                                                    queueProducer.Send(messageBody, Apache.NMS.MsgPriority.Normal);
                                                    dictionary.Add(goodsid, 1);
                                                }
                                                else
                                                {
                                                    dictionary.Remove(goodsid);
                                                }
                                            }
                                        }
                                        try
                                        {
                                            ////更新相应索引
                                            //if (isindex && !string.IsNullOrWhiteSpace(goodsid))
                                            //{
                                            //    TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", int.Parse(goodsid), 0);
                                            //    logMsg += message + "_索引消息已发送";
                                            //}
                                        }
                                        catch (Exception ex)
                                        {
                                            Log("Error", "更新相应索引:" + ex.Message);
                                        }
                                        Log("Info", logMsg);
                                    

                                    break;
                                case 2:

                                    DeleteImage(RedisTool.ImgpathBase + Convert.ToString(MethodParameter[1]), picturetype);
                                    break;
                            }

                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(queueConsumer.ErrorMessage))
                            {
                                Log("Error", "ReceiveMessage0():" + queueConsumer.ErrorMessage + "  MQ：出现异常。");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log("Error", "ReceiveMessage1():" + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Log("Error", "ReceiveMessage2():" + ex.Message);
            }
        }



        protected static void DeleteImage(string path, string picturetype)
        {
            try
            {
                Log("Info", "删除图片开始:" + picturetype + path);
                var thumbnails = Thumbnail.GetGoodsPictureThumbnail(picturetype);
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    file.Delete();
                    Log("Info", "删除图片:" + picturetype + file.Name);
                }
                file = new FileInfo(path + ".180_130.jpg");
                if (file.Exists)
                {
                    file.Delete();
                    Log("Info", "删除图片:" + file.Name);
                }
                if (thumbnails != null)
                {
                    for (int i = 0; i < thumbnails.Length; i++)
                    {
                        string nextpath = path + "." + thumbnails[i] + ".jpg";
                        file = new FileInfo(nextpath);
                        if (file.Exists)
                        {
                            file.Delete();
                            Log("Info", "删除图片:" + file.Name);
                        }
                    }

                }

            }
            catch (Exception ex)
            {

                Log("Info", "删除图片错误:" + ex.Message);
                throw ex;
            }
        }


        /// <summary>
        /// 生成日志
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        protected static void Log(string flag, string msg)
        {
            try
            {
                var logPath = string.Format(LogPath, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                FileStream fs = new FileStream(logPath, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter mStreamWriter = new StreamWriter(fs);
                mStreamWriter.BaseStream.Seek(0, SeekOrigin.End);
                mStreamWriter.WriteLine(flag + ":  " + msg + " :" + DateTime.Now + "\n");
                mStreamWriter.Flush();
                mStreamWriter.Close();
                fs.Close();
            }
            catch { }

        }


    }
}
