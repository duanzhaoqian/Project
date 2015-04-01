using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.Commons.Common;

namespace KYJ.ZS.Commons.Web
{
    public class AuxiliaryViewPage<TModel> : WebViewPage<TModel>
    {
        #region 私有成员

        private LoginUserInfo _CurrentUser = null;

        #endregion

        public LoginUserInfo CurrentUser
        {
            get
            {
                if (_CurrentUser == null)
                    _CurrentUser = ViewData["_CurrentUser"] as LoginUserInfo;
                return _CurrentUser;
            }
        }

        public override void Execute()
        {
        }
    }

    public class AuxiliaryViewPage : AuxiliaryViewPage<object>
    {
    }
}
