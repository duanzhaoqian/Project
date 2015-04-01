namespace TXNewTimingServices
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.services = new System.ServiceProcess.ServiceProcessInstaller();
            this.TXActivitiesDayService = new System.ServiceProcess.ServiceInstaller();
            this.TXActivitiesHoursService = new System.ServiceProcess.ServiceInstaller();
            this.TXActivitiesMinuteService = new System.ServiceProcess.ServiceInstaller();
            this.TXAmountService = new System.ServiceProcess.ServiceInstaller();
            // 
            // services
            // 
            this.services.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.services.Password = null;
            this.services.Username = null;
            // 
            // TXActivitiesDayService
            // 
            this.TXActivitiesDayService.Description = "新房定时服务（每天执行一次）";
            this.TXActivitiesDayService.DisplayName = "TXActivitiesDayService";
            this.TXActivitiesDayService.ServiceName = "TXActivitiesDayService";
            this.TXActivitiesDayService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // TXActivitiesHoursService
            // 
            this.TXActivitiesHoursService.Description = "新房定时服务（每小时执行一次）";
            this.TXActivitiesHoursService.DisplayName = "TXActivitiesHoursService";
            this.TXActivitiesHoursService.ServiceName = "TXActivitiesHoursService";
            this.TXActivitiesHoursService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // TXActivitiesMinuteService
            // 
            this.TXActivitiesMinuteService.Description = "新房定时服务（每分钟执行一次）";
            this.TXActivitiesMinuteService.DisplayName = "TXActivitiesMinuteService";
            this.TXActivitiesMinuteService.ServiceName = "TXActivitiesMinuteService";
            this.TXActivitiesMinuteService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // TXAmountService
            // 
            this.TXAmountService.Description = "新房活动金额返还服务";
            this.TXAmountService.DisplayName = "TXAmountService";
            this.TXAmountService.ServiceName = "TXAmountService";
            this.TXAmountService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.services,
            this.TXActivitiesDayService,
            this.TXActivitiesHoursService,
            this.TXActivitiesMinuteService,
            this.TXAmountService});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller services;
        private System.ServiceProcess.ServiceInstaller TXActivitiesDayService;
        private System.ServiceProcess.ServiceInstaller TXActivitiesHoursService;
        private System.ServiceProcess.ServiceInstaller TXActivitiesMinuteService;
        private System.ServiceProcess.ServiceInstaller TXAmountService;
    }
}