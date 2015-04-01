using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorClient.Model
{
    [Serializable]
    public class ServerInfo
    {
        private string _name;
        private string _ip;
        private int _port;
        private string _wanip;
        private int _wanport;

        public ServerInfo()
        { }
        public string wanip
        {
            get { return this._wanip; }
            set
            {
                this._wanip = value;
            }
        }
        public int wanport
        {
            get { return this._wanport; }
            set
            {
                this._wanport = value;
            }
        }


        public string name
        {
            get { return this._name; }
            set
            {
                this._name = value;
            }
        }
        public string ip
        {
            get { return this._ip; }
            set
            {
                this._ip = value;
            }
        }
        public int port
        {
            get { return this._port; }
            set
            {
                this._port = value;
            }
        }
    }
}
