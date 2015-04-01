
namespace TXModel.Commons
{
    /// <summary>
    /// 数据库执行结果返回值(Code： 执行代码（如：'0' 执行失败 '1' 执行成功 '-1' 不存在） Msg：错误信息或提示信息)
    /// </summary>
    public class ESqlResult
    {
        public string Code { set; get; }

        public string Msg { set; get; }
    }
}