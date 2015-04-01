using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TXSearch.RentHouse
{
    /// <summary>
    /// AddSearchIndexTest 的摘要说明
    /// </summary>
    public class AddSearchIndexTest : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string message = "ok";
            try
            {
                string c = context.Request.QueryString["c"];
                if (string.IsNullOrEmpty(c))
                    c = "253";
                int city = int.Parse(c);
                message= TXCommons.MsgQueue.MQHelp.GetMqNameByCityId(city);
               
                string id = context.Request.QueryString["id"];
                string t = context.Request.QueryString["t"];
                string u = context.Request.QueryString["uid"];
                string ty = context.Request.QueryString["utype"];
                string date = context.Request.QueryString["time"];
                if (!string.IsNullOrEmpty(date))
                {
                    try
                    {
                        message += TXCommons.Util.UNIX_TIMESTAMP(Convert.ToDateTime(date));
                    }
                    catch
                    {
                        message += TXCommons.Util.FROM_UNIXTIME(long.Parse (date));
                    }
                }
                if (string.IsNullOrEmpty(t))
                    t = "update";
                if (t == "User")
                {
                    TXCommons.MsgQueue.SendMessage.SendUserIdMessage("User", int.Parse(u), int.Parse(ty), city);
                }
                else
                {
                    TXCommons.MsgQueue.SendMessage.SendLongHouseIndexMessage(t, int.Parse(id), city);
                }
              //  TXCommons.MsgQueue.SendMessage.SendVillageSubWayIndexMessage("insert", 14990);

                
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }
            context.Response.Write(message);
        }
        public void SendMobileMessage(string mobile, string message)
        {
            string url = "http://smsweb.kuaiyoujia.com/AcceptSms.aspx";
            TXCommons.SMSTool smsTool = new TXCommons.SMSTool();
           string a=  smsTool.sendSms(1, url, mobile, message);
          
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }
}