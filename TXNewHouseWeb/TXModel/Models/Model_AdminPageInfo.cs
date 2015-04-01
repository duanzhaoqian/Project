
namespace TXModel.Models
{
    public class Model_AdminPageInfo
    {
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
    }
}