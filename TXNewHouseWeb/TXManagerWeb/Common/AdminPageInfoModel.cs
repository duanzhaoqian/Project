
namespace TXManagerWeb.Common
{
    public class AdminPageInfoModel
    {
        /// <summary>
        /// 选项卡编号
        /// </summary>
        public int CardId { set; get; }

        /// <summary>
        /// 选项卡名称
        /// </summary>
        public string CardName { set; get; }

        /// <summary>
        /// 栏目编号
        /// </summary>
        public int ItemId { set; get; }

        /// <summary>
        /// 栏目名称
        /// </summary>
        public string ItemName { set; get; }

        /// <summary>
        /// 父栏目编号
        /// </summary>
        public int FatherItemId { set; get; }

        /// <summary>
        /// 父栏目名称
        /// </summary>
        public string FatherItemName { set; get; }

        /// <summary>
        /// 页面 Title 部分
        /// </summary>
        public string PageTitle { set; get; }
    }
}