﻿using System.ComponentModel;
using System.Configuration.Install;


namespace TXPremisesImageService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}
