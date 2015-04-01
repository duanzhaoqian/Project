using TXModel.AdminLogs;

namespace TXBll.WebSite
{
    public class AdminService<T> where T : Z_OperAdminLog
    {
        private readonly AdminLogBll _adminLogBll = new AdminLogBll();

        public T Entity { get; set; }

        public AdminService(T entity)
        {
            Entity = entity;
        }

        public void ExecuteResult()
        {
            if (Entity != null)
                //添加管理员操作日志
                _adminLogBll.Create(Entity);
            // new EmptyResult().ExecuteResult(context);
        }

    }
}