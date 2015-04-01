
namespace TXModel.Models
{
    public class Z_Model
    {
        /// <summary>
        /// 模块Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 父Id
        /// </summary>
        public int PId { get; set; }
        /// <summary>
        /// 父类名称
        /// </summary>
        public string PName { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 添加时间   
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string UpdateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDel { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public int Version { get; set; }
    }
}