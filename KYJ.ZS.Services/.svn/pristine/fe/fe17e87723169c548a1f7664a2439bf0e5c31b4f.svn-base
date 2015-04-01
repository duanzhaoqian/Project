using System.ServiceProcess;

namespace KYJ.ZS.Services
{
    using KYJ.ZS.Task;

    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            //LogHelper.Config();
            //LogHelper.Info("TaskService Start");

            var ServicesToRun = new ServiceBase[] 
			{ 
				new ZSService() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
