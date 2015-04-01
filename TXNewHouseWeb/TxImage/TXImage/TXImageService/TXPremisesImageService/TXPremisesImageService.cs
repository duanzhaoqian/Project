using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.IO;
using System.Threading;
using System.Collections;
using TXCommons.PictureModular;
using ServiceStack;

namespace TXPremisesImageService
{
    partial class TXPremisesImageService : ServiceBase
    {
        protected static string MQIPAddress = string.Empty;
        protected static string MQQueueName = string.Empty;
        protected static string MQPremisesSearchQueueName = string.Empty;
        protected static string MQConnectionTimeOut = string.Empty;
        protected static string MQRetryCount = string.Empty;
        protected static string LogPath = string.Empty;
        protected static int ThreadCount = 0;
        protected static string WWWDomain = string.Empty;

        //静态词典
        protected static Dictionary<string, int> dictionary = new Dictionary<string, int>();
        public TXPremisesImageService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            //读取MQ的相关配置
            MQIPAddress = Profile.IniReadValue("TXImageService", "MQIPAddress");
            MQQueueName = Profile.IniReadValue("TXImageService", "MQQueueName");
            MQConnectionTimeOut = Profile.IniReadValue("TXImageService", "MQConnectionTimeOut");
            MQRetryCount = Profile.IniReadValue("TXImageService", "MQRetryCount");

            LogPath = Profile.IniReadValue("TXImageService", "LogPath");
            ThreadCount = int.Parse(Profile.IniReadValue("TXImageService", "ThreadCount"));

            WWWDomain = Profile.IniReadValue("TXImageService", "WWWDomain");


            //设置MQ的相关配置
            MQ.ConsumerConfiguration.IPAddress = MQIPAddress;
            MQ.ConsumerConfiguration.ConnectionTimeOut = new TimeSpan(0, 0, Convert.ToInt32(Profile.IniReadValue("TXImageService", "MQConnectionTimeOut")));
            MQ.ConsumerConfiguration.RetryCount = Convert.ToInt32(Profile.IniReadValue("TXImageService", "MQRetryCount"));
            MQPremisesSearchQueueName = Profile.IniReadValue("TXImageService", "MQLongHouseSearchQueueName");
            MQ.ProducerConfiguration.IPAddress = Profile.IniReadValue("TXImageService", "MQIPAddress");
            MQ.ProducerConfiguration.ConnectionTimeOut = new TimeSpan(0, 0, Convert.ToInt32(Profile.IniReadValue("TXImageService", "MQConnectionTimeOut")));
            MQ.ProducerConfiguration.RetryCount = Convert.ToInt32(Profile.IniReadValue("TXImageService", "MQRetryCount"));
            Log("Info", "图片服务启动");

            CreateThread();
            Console.Read();
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
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
                            string renttype = string.Empty;
                            string houseid = string.Empty;
                            string picturetype = string.Empty;
                            int cityId = 0;
                            switch (MethodType)
                            {
                                //租房图片尺寸处理
                                case 1:
                                    //房源GUID
                                    guid = Convert.ToString(MethodParameter[0]);
                                    //租房类型
                                    renttype = Convert.ToString(MethodParameter[1]);
                                    //楼盘ID
                                    houseid = Convert.ToString(MethodParameter[2]);
                                    //图片类型
                                    picturetype = Convert.ToString(MethodParameter[3]);
                                    //城市ID
                                    cityId = Convert.ToInt32(MethodParameter[4]);
                                    //fullpath
                                    string fullpath = Convert.ToString(MethodParameter[7]);
                                    bool isindex = Convert.ToBoolean(MethodParameter[5]);


                                    if (renttype == "Premises")
                                    {
                                        var message = "update" + "," + houseid + "," + cityId;
                                        Log("Info", "接收消息:房源ID：" + houseid + "　城市ID：" + cityId + "　房源类型:" + renttype + "　　GUID:" + guid);
                                        try
                                        {
                                            //string key = KeyManager.GetPremisesPictureKey(guid, picturetype.ToUpper());

                                            //List<PremisesPictureInfo> list = GetPicture.GetPremisesPictureList(guid, false, picturetype, cityId);
                                            PremisesPictureInfo info = null;
                                            List<PremisesPictureInfo> list = null;
                                            string path = string.Empty;
                                            if (!string.IsNullOrEmpty(fullpath))
                                            {
                                                path = fullpath;
                                            }
                                            else
                                            {
                                                list = GetPicture.GetPremisesPictureList(guid, false, picturetype, cityId);
                                                if (list.Count > 1)
                                                {
                                                    info = list[0];
                                                    path = Redis.IMGPATH_BASE + info.Path;
                                                }
                                            }
                                            if (null != info || !string.IsNullOrEmpty(path))
                                            {

                                                UploadPicture.DeleteRentHousePicture(guid, PremisesPictureType.List.ToString(), cityId);
                                                /////////////////////////////////////////////////
                                                UploadThumbnail.ThumbnailPremisesPicture(path, picturetype);
                                                /////////////////////////////////////////////////
                                                if (null != info)
                                                {
                                                    string key2 = KeyManager.GetPremisesPictureKey(guid,
                                                        PremisesPictureType.List.ToString());
                                                    info.IsNew = 0;
                                                    Redis.AddItemToSortedSet<PremisesPictureInfo>(info, key2, 1, null,
                                                        FunctionTypeEnum.NewHouseBasicDataPicture, cityId);
                                                    logMsg = "列表图已生成";
                                                }
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
                                            if (!string.IsNullOrEmpty(houseid))
                                            {
                                                if (!dictionary.ContainsKey(houseid))
                                                {
                                                    MQ.IQueueProducer queueProducer =
                                                        MQ.ProducerFactory.CreateQueueProducer(MQQueueName);
                                                    queueProducer.Send(messageBody, Apache.NMS.MsgPriority.Normal);
                                                    dictionary.Add(houseid, 1);
                                                }
                                                else
                                                {
                                                    dictionary.Remove(houseid);
                                                }
                                            }
                                        }
                                        try
                                        {
                                            //更新相应索引
                                            if (isindex && !string.IsNullOrWhiteSpace(houseid))
                                            {
                                                TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", int.Parse(houseid), cityId);
                                                logMsg += message + "_索引消息已发送";
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Log("Error", "更新相应索引:" + ex.Message);
                                        }
                                        Log("Info", logMsg);
                                    }

                                    break;
                                case 2:

                                    DeleteImage(Redis.IMGPATH_BASE + Convert.ToString(MethodParameter[2]), picturetype);
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
                var thumbnails = Thumbnail.GetRentHousePictureThumbnail(picturetype);
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
                StreamWriter m_streamWriter = new StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                m_streamWriter.WriteLine(flag + ":  " + msg + " :" + DateTime.Now.ToString() + "\n");
                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs.Close();
            }
            catch { }

        }
    }
}
