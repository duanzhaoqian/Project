namespace TXSearchService
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
            this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.TxVillageService = new System.ServiceProcess.ServiceInstaller();
            this.TxHouseBidStatusService = new System.ServiceProcess.ServiceInstaller();
            this.TxLongHouseService = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstaller1
            // 
            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;
            this.serviceProcessInstaller1.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceProcessInstaller1_AfterInstall);
            // 
            // TxVillageService
            // 
            this.TxVillageService.Description = "小区索引服务 ";
            this.TxVillageService.DisplayName = "TxVillageService";
            this.TxVillageService.ServiceName = "TxVillageService";
            this.TxVillageService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.TxVillageService.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.TxVillageService_AfterInstall);
            // 
            // TxHouseBidStatusService
            // 
            this.TxHouseBidStatusService.Description = "房源出价状态索引服务";
            this.TxHouseBidStatusService.DisplayName = "TxHouseBidStatusService";
            this.TxHouseBidStatusService.ServiceName = "TxHouseBidStatusService";
            this.TxHouseBidStatusService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.TxHouseBidStatusService.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.TxHouseBidStatusService_AfterInstall);
            // 
            // TxLongHouseService
            // 
            this.TxLongHouseService.Description = "房源索引服务";
            this.TxLongHouseService.DisplayName = "TxLongHouseService";
            this.TxLongHouseService.ServiceName = "TXLongHouseService";
            this.TxLongHouseService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller1,
            this.TxVillageService,
            this.TxHouseBidStatusService,
            this.TxLongHouseService});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller TxVillageService;
        private System.ServiceProcess.ServiceInstaller TxHouseBidStatusService;
        private System.ServiceProcess.ServiceInstaller TxLongHouseService;
    }
}