using System;
using System.ServiceProcess;
using System.Timers;

namespace TXNewHouseServices
{
    public partial class ServiceNewHouse : ServiceBase
    {
        private Timer _timer;

        private string _log_filenamelast_rank = "rank";

        private string _log_filenamelast_characteristic = "characteristic";

        public ServiceNewHouse()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _timer = new Timer(1000 * 60);
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();

            ServiceCom.Instance.WriteLog(_log_filenamelast_rank, "服务已开启");
        }

        protected override void OnStop()
        {
            _timer.Stop();
            _timer.Dispose();

            ServiceCom.Instance.WriteLog(_log_filenamelast_rank, "服务已结束");
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                // 读取setting.xml文件
                var settings = ServiceCom.Instance.GetSettings();
                if (null == settings)
                {
                    ServiceCom.Instance.WriteLog(_log_filenamelast_rank, "配置文件读取失败");
                    return; // 配置文件读取出错
                }

                if ((!settings.IsRunBody) && (settings.BeginTime <= DateTime.Now))
                {
                    settings.IsRunBody = true;
                    ServiceCom.Instance.SaveSettingsForXml(settings);

                    ServiceCom.Instance.WriteLog(_log_filenamelast_rank, "服务主体开始执行……");
                    var body = new RankBody();
                    body.RunBody();
                    ServiceCom.Instance.WriteLog(_log_filenamelast_rank, "服务主体执行结束……\r\n---------- ***** ---------- ***** ----------");

                    while (settings.BeginTime < DateTime.Now)
                    {
                        settings.BeginTime = Convert.ToDateTime(string.Format("{0:yyyy-MM-dd HH:00:00}", settings.BeginTime)).AddHours(settings.TimeInterval);
                    }
                    settings.IsRunBody = false;
                    ServiceCom.Instance.SaveSettingsForXml(settings);
                }

                if ((!settings.IsRunBody_Characteristic) && (settings.BeginTime_Characteristic <= DateTime.Now))
                {
                    settings.IsRunBody_Characteristic = true;
                    ServiceCom.Instance.SaveSettingsForXml(settings);

                    ServiceCom.Instance.WriteLog(_log_filenamelast_characteristic, "服务主体开始执行……");
                    var body = new CharacteristicBody();
                    body.RunBody();
                    ServiceCom.Instance.WriteLog(_log_filenamelast_characteristic, "服务主体执行结束……\r\n---------- ***** ---------- ***** ----------");

                    while (settings.BeginTime_Characteristic < DateTime.Now)
                    {
                        settings.BeginTime_Characteristic = Convert.ToDateTime(string.Format("{0:yyyy-MM-dd HH:00:00}", settings.BeginTime_Characteristic)).AddHours(settings.TimeInterval_Characteristic);
                    }
                    settings.IsRunBody_Characteristic = false;
                    ServiceCom.Instance.SaveSettingsForXml(settings);
                }
            }
            catch (Exception ex)
            {
                ServiceCom.Instance.WriteLog(_log_filenamelast_rank, ex.StackTrace);
            }
        }
    }
}