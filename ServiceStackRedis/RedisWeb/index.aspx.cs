using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KYJ.ServiceStack;

namespace RedisWeb
{
    public partial class index : System.Web.UI.Page
    {
        protected string[] PostUrl
        {
            get
            {
                string offerPrice = Tool.ConfigKey("offerPrice");
                return offerPrice.Split(',');
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}