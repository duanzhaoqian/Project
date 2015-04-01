using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestTask
{
    using System.ServiceProcess;
    using System.Threading.Tasks;
    using System.Timers;

    using KYJ.ZS.Services;
    using KYJ.ZS.Task;
    using KYJ.ZS.Log4net;
    using KYJ.ZS.Task.Task;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            RecordLog.ServiceLogs("cesi", "TaskService Start");

            var ServicesToRun = new ServiceBase[] 
			{ 
				new ZSService() 
			};
            ServiceBase.Run(ServicesToRun);

        }
        [TestMethod]
        public void TestTaskRuntime()
        {
            var rumtime = "* * * * *";
            var targetTime = DateTime.Now;

            Assert.IsTrue(TaskConfig.CheckTaskRuntime(rumtime, targetTime));

            rumtime = "1 * * * *";
            targetTime = new DateTime(2011, 1, 1, 1, 1, 0, 0);

            Assert.IsTrue(TaskConfig.CheckTaskRuntime(rumtime, targetTime));

            rumtime = "1 * * * *";
            targetTime = new DateTime(2011, 1, 1, 1, 2, 0, 0);

            Assert.IsFalse(TaskConfig.CheckTaskRuntime(rumtime, targetTime));

            rumtime = "1,2 * * * *";
            targetTime = new DateTime(2011, 1, 1, 1, 2, 0, 0);

            Assert.IsTrue(TaskConfig.CheckTaskRuntime(rumtime, targetTime));

            rumtime = "59 23 * * 5";
            var baseTime = DateTime.Now;

            Console.WriteLine(rumtime);
            var count = 0;
            for (var i = 1; i < 60 * 24 * 365; i++)
            {
                targetTime = baseTime.AddMinutes(i);
                if (TaskConfig.CheckTaskRuntime(rumtime, targetTime))
                {
                    Console.WriteLine("Run at: {0}", targetTime);

                    if (++count > 10)
                    {
                        break;
                    }
                }
            }
        }

        [TestMethod]
        public void Test()
        {
            //var _timer = new Timer(5 * 1000);

            //_timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

            //_timer.Start();

            RecordLog.ServiceLogs("cesi", "TaskService Start");
            var tick = DateTime.Now.Ticks;
            
            var tasks = TaskConfig.GetTasks();
            var _runningTasks = new List<string>();

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
                        RecordLog.ServiceLogs("cesi", string.Format("Task Error：{0}，StackTrace：{1}", ex.Message, ex.StackTrace));
                    }
                    finally
                    {
                        if (tce != null)
                            _runningTasks.Remove(tce.Name);
                    }
                }, task);

                _runningTasks.Add(task.Name);
                
            }
            RecordLog.ServiceLogs("cesi", string.Format("Checke Tasks end,{0}", DateTime.Now));
            //LogHelper.Info(string.Format("Checke Tasks end,{0}", DateTime.Now));

        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var tick = DateTime.Now.Ticks;
            List<string> ig = new List<string>
            {

            };
            var tasks = TaskConfig.GetTasks();
            var _runningTasks = new List<string>();

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
                    }
                    finally
                    {
                        if (tce != null)
                            _runningTasks.Remove(tce.Name);
                    }
                }, task);

                if (!ig.Contains(task.Name))
                {
                    _runningTasks.Add(task.Name);
                }
            }

            //LogHelper.Info(string.Format("Checke Tasks end,{0}", tick));
        }

        [TestMethod]
        public void TestRuntime()
        {
            var d = DateTime.Now.ToShortDateString();
            var d2 = DateTime.Now.ToShortTimeString();
            var d3 = string.Format("{0:g}", DateTime.Now);
        }
        [TestMethod]
        public void TestOrderPendingPaymentTask()
        {
            var orderStart = new OrderStartRentTask();
            orderStart.Run();
        }
    }
}
