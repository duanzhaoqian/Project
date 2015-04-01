using System.Web.Mvc;
using TXManagerWeb.Common;
using TXModel.Admins;

namespace TXManagerWeb.Utility
{
    public class WebRentViewPage<TModel> : ViewPage<TModel>
    {
        #region 私有成员

        private Z_Admin _currentUser;

        #endregion

        public Z_Admin CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    _currentUser = ViewData["_CurrentUser"] as Z_Admin;
                }
                return _currentUser;
            }
        }

        public string Current
        {
            get
            {
                if (ViewData["_Current"] != null)
                {
                    return ViewData["_Current"].ToString();
                }
                return string.Empty;
            }
        }

        public AdminPageInfoModel AdminPageInfo
        {
            get
            {
                if (ViewData["_AdminPageInfo"] != null)
                {
                    return ViewData["_AdminPageInfo"] as AdminPageInfoModel;
                }
                return new AdminPageInfoModel();
            }
        }

    }

    public class WebRentViewPage : WebRentViewPage<object>
    {
        
    }

    public class WebViewMasterPage<TModel> : ViewMasterPage<TModel>
    {
        #region 私有成员

        private Z_Admin _currentUser;

        #endregion

        public Z_Admin CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    _currentUser = ViewData["_CurrentUser"] as Z_Admin;
                }

                return _currentUser;
            }
        }

        public string Current
        {
            get
            {
                if (ViewData["_Current"] != null)
                {
                    return ViewData["_Current"].ToString();
                }
                return string.Empty;
            }
        }

        public AdminPageInfoModel AdminPageInfo
        {
            get
            {
                if (ViewData["_AdminPageInfo"] != null)
                {
                    return ViewData["_AdminPageInfo"] as AdminPageInfoModel;
                }
                return new AdminPageInfoModel();
            }
        }
    }

    public class WebMasterViewPage : WebViewMasterPage<object>
    {

    }

    public class WebViewUserControl<TModel> : ViewUserControl<TModel>
    {
        #region 私有成员

        private Z_Admin _currentUser;

        #endregion

        public Z_Admin CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    _currentUser = ViewData["_CurrentUser"] as Z_Admin;
                }

                return _currentUser;
            }
        }
    }

    public class WebViewUserControl : WebViewUserControl<object>
    {

    }
}