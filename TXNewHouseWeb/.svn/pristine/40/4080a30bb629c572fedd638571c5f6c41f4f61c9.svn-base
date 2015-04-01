using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using KYJ.ServiceStack;
using TXCommons.PictureModular;

namespace TXImageWebService
{
    /// <summary>
    /// HouseImage 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class HouseImage : System.Web.Services.WebService
    {
        [WebMethod(Description = "通过文件路径保存图片")]
        public string SavePremisesImageToRedis(string InnerCode, int CityId, string PictureType, string FullFilePath, string FileName, int PremisesId, bool IsSubmitUpdate, string Title,string Desc)
        {
            if (string.IsNullOrEmpty(InnerCode))
            {
                TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", PremisesId, CityId);//生成Lucene
            }
            PictureType = PictureType.ToUpper();
            UploadPicture.SavePictureToRedis(InnerCode, PictureType, CityId, FormatGuid(InnerCode), FileName, Title, Desc);//保存
            TXCommons.MsgQueue.SendMessage.SendPictureMessage(InnerCode, "Premises", PictureType, PremisesId.ToString(), CityId, FullFilePath);//切图
            if (IsSubmitUpdate)
            {
                TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", PremisesId, CityId);//生成Lucene
            }
            return "True";
        }


        #region 图片基础方法
        /// <summary>
        /// 格式化guid路径
        /// </summary>
        /// <param name="guid">guid</param>
        /// <returns>路径</returns>
        private static string FormatGuid(string guid)
        {
            return guid.Substring(0, 2) + "/" + guid.Substring(2, 2) + "/" + guid.Substring(4, 2) + "/" + guid + "/";
        }
        #endregion

    }
}
