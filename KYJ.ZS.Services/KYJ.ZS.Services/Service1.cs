
namespace KYJ.ZS.Services
{
    using System;
    using System.Collections.Generic;
    using System.ServiceProcess;
    using System.Threading.Tasks;
    using System.Timers;
    using System.Configuration;

    using KYJ.ZS.Log4net;
    using KYJ.ZS.Task;

    public partial class ZSService : ServiceBase
    {
        int interval = 30;
        private static Timer _timer;

        private IList<string> _runningTasks;

        public ZSService()
        {
            InitializeComponent();

            _runningTasks = new List<string>();

            try
            {
                var section = ConfigurationManager.GetSection("taskconfig") as TaskConfigSection;

                if (section != null)
                {
                    interval = section.Interval;
                }
                else
                {
                    //LogHelper.Info("section is null");
                    RecordLog.ServiceLogs("ZSService", "section is null");
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex.Message, ex);
                RecordLog.RecordException("ZSService", string.Format("Task Error：{0}，ex：{1}", ex.Message, ex));
            }
        }
        

        protected override void OnStart(string[] args)
        {
            _timer = new Timer(interval * 1000);

            this.EventLog.WriteEntry("ZSService started");

            _timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

            _timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var tick = DateTime.Now.Ticks;

            var tasks = TaskConfig.GetTasks();

            foreach (var task in tasks)
            {
                if (task == null || task.Instance == null || !task.Enable)
                    continue;

                if (_runningTasks.Contains(task.Name))
                {
                    continue;
                }

                Task.Factory.StartNew((t) =>
                {
                    TaskConfigElement tce = null;
                    try
                    {
                        tce = t as TaskConfigElement;
                        tce.Instance.Run();
                    }
                    catch (Exception ex)
                    {
                        //LogHelper.Error(string.Format("Task Error：{0}，StackTrace：{1}", ex.Message, ex.StackTrace), ex);
                        RecordLog.RecordException(string.Format("taskName:{0}", task.Name), string.Format("Task Error：{0}，StackTrace：{1}", ex.Message, ex.StackTrace));
                    }
                    finally
                    {
                        if (tce != null)
                            _runningTasks.Remove(tce.Name);
                    }
                }, task);

                
                _runningTasks.Add(task.Name);
                
            }

            //LogHelper.Info(string.Format("Checke Tasks end,{0}", DateTime.Now));
        }

        protected override void OnStop()
        {
            _timer.Stop();
            this.EventLog.WriteEntry("ZSService Stopped");
            RecordLog.ServiceLogs("ZSService", "ZSService Stopped");
            //LogHelper.Info("ZSService Stopped");
            _timer.Close();
        }
    }
}
