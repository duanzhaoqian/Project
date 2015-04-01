using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using TXBll.WebSite;
using TXModel.Admins;

namespace TXManagerWeb.Common
{
    public class ServiceContext
    {
        private AdminBll _adminBll;

        private string _Name;

        public string Name
        {
            get
            {
                if (_Name == null)
                {
                    var cookie = HttpContext.Current.Request.Cookies["manager_kuaiyoujia"];
                    if (cookie != null)
                    {
                        var data = cookie.Value;
                        if (!string.IsNullOrWhiteSpace(data))
                        {
                            return data;
                        }
                        return null;
                    }

                    return null;
                }

                return _Name;
            }
        }

        public Z_Admin CurrentUser
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    var btName = Convert.FromBase64String(Name);
                    var admin = Deserialize(btName) as Z_Admin;
                    return admin;
                }
                return null;
            }
        }

        public void Refresh()
        {
        }

        public Z_Admin GetCurrentUser(string username)
        {
            Z_Admin currentUser = null;
            if (!string.IsNullOrWhiteSpace(username))
            {
                currentUser = GetAdminByLoginName(username);

                if (currentUser == null)
                {
                    AuthenticationService.SignOut();
                }
            }

            return currentUser;
        }

        private Z_Admin GetAdminByLoginName(string loginName)
        {
            _adminBll = new AdminBll();
            return _adminBll.Z_GetAdminByLoginName(loginName);
        }

        /// <summary>
        /// 序列化Z_Admin对象返回二进制数据
        /// </summary>
        /// <param name="admin">Z_Admin对象</param>
        /// <returns></returns>
        public static byte[] Serialize(Z_Admin admin)
        {
            var formatter = new BinaryFormatter();
            var ms = new MemoryStream();
            formatter.Serialize(ms, admin);
            ms.Position = 0;
            var extraData = new byte[ms.Length];
            ms.Read(extraData, 0, extraData.Length);
            ms.Close();
            return extraData;
        }

        /// <summary>
        /// 反序列化成Z_Admin对象
        /// </summary>
        /// <param name="extraData">ExtraData的二进制数据</param>
        /// <returns>Z_Admin实体数据</returns>
        public static Z_Admin Deserialize(byte[] extraData)
        {
            var formatter = new BinaryFormatter();
            var ms = new MemoryStream();
            ms.Write(extraData, 0, extraData.Length);
            ms.Position = 0;
            var zAdmin = formatter.Deserialize(ms) as Z_Admin;
            return zAdmin;
        }
    }
}