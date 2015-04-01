namespace TXNewServices
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
            this.TXPremisesService = new System.ServiceProcess.ServiceInstaller();
            this.TXRoomService = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstaller1
            // 
            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;
            // 
            // TXPremisesService
            // 
            this.TXPremisesService.Description = "新房楼盘索引服务";
            this.TXPremisesService.DisplayName = "TXPremisesService";
            this.TXPremisesService.ServiceName = "TXPremisesService";
            this.TXPremisesService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.TXPremisesService.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            // 
            // TXRoomService
            // 
            this.TXRoomService.Description = "新房房源户型图服务";
            this.TXRoomService.DisplayName = "TXRoomService";
            this.TXRoomService.ServiceName = "TXRoomService";
            this.TXRoomService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller1,
            this.TXPremisesService,
            this.TXRoomService});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller TXPremisesService;
        private System.ServiceProcess.ServiceInstaller TXRoomService;
    }
}