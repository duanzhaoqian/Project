using System.Collections.Generic;
using KYJ.ZS.DAL.Module;
using KYJ.ZS.Models.Module;
using KYJ.ZS.Commons;

namespace KYJ.ZS.BLL.Module
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-06-04
    /// Desc：操作模块表
    /// </summary>
    public class ModuleBll
    {
        private ModuleDal dal = new ModuleDal();

        /// <summary>
        /// 获取模块下拉列表集合
        /// </summary>
        /// <returns></returns>
        public IList<ModuleSelectEntity> GetSelectModules()
        {
            return dal.GetSelectModules();
        }

        /// <summary>
        /// 获取日志查阅中的功能区
        /// </summary>
        /// <returns></returns>
        public IList<ModuleSelectEntity> GetAreaList()
        {
            IList<ModuleSelectEntity> list = new List<ModuleSelectEntity>();
            list.Add(new ModuleSelectEntity() { ModuleCode = 1, ModuleName = "商品排序管理" });
            list.Add(new ModuleSelectEntity() { ModuleCode = 2, ModuleName = "广告管理" });
            list.Add(new ModuleSelectEntity() { ModuleCode = 3, ModuleName = "商户管理" });
            list.Add(new ModuleSelectEntity() { ModuleCode = 4, ModuleName = "商品管理" });
            list.Add(new ModuleSelectEntity() { ModuleCode = 5, ModuleName = "订单管理" });
            list.Add(new ModuleSelectEntity() { ModuleCode = 6, ModuleName = "商户提现管理" });
            list.Add(new ModuleSelectEntity() { ModuleCode = 7, ModuleName = "普通用户管理" });
            list.Add(new ModuleSelectEntity() { ModuleCode = 8, ModuleName = "闲置物品管理" });
            list.Add(new ModuleSelectEntity() { ModuleCode = 9, ModuleName = "职员管理" });
            list.Add(new ModuleSelectEntity() { ModuleCode = 10, ModuleName = "基础数据" });
            list.Add(new ModuleSelectEntity() { ModuleCode = -1, ModuleName = "登录退出" });
            return list;
        }

        /// <summary>
        /// 根据功能区获得对应的模块ID集合
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public string GetModulesByArea(int areaId)
        {
            #region
            switch (areaId)
            {
                case 1:
                    {
                        return (int)ManagerNavigation.GUANLISHANGPINPAIXU + ","
                            + (int)ManagerNavigation.SHENHESHANGPINPAIXU + ","
                            + (int)ManagerNavigation.TIANJIASHANGPINPAIXU;
                    }
                case 2:
                    {
                        return (int)ManagerNavigation.GUANLIGUANGGAO + ","
                            + (int)ManagerNavigation.HESHENGUANGGAO + ","
                            + (int)ManagerNavigation.FABUGUANGGAO;
                    }
                case 3:
                    {
                        return (int)ManagerNavigation.GUANLISHANGHU + ","
                            + (int)ManagerNavigation.TIANJIASHANGHU;
                    }
                case 4:
                    {
                        return (int)ManagerNavigation.SHANGPINLIEBIAO + ","
                            + (int)ManagerNavigation.WEIGUISHANGPIN;
                    }
                case 5:
                    {
                        return ((int)ManagerNavigation.GUANLIDINGDAN).ToString();
                    }
                case 6:
                    {
                        return ((int)ManagerNavigation.GUANLISHANGHUTIXIAN).ToString();
                    }
                case 7:
                    {
                        return ((int)ManagerNavigation.GUANLIPUTONGYONGHU).ToString();
                    }
                case 8:
                    {
                        return (int)ManagerNavigation.GUANLIXINXI + ","
                            + (int)ManagerNavigation.WEIGUIXINXI;
                    }
                case 9:
                    {
                        return (int)ManagerNavigation.GUANLIZHIYUANQUANXIAN + ","
                            + (int)ManagerNavigation.TIANJIAZHIYUANZHANGHAO;
                    }
                case 10:
                    {
                        return (int)ManagerNavigation.SHANGPINFENLEIGUANLI + ","
                            + (int)ManagerNavigation.SHUXINGGUIGEGUANLI + ","
                            + (int)ManagerNavigation.XINXIBIAOQIANGUANLI + ","
                            + (int)ManagerNavigation.GUANLIQUANXIANJUESE + ","
                            + (int)ManagerNavigation.RIZHICHAYUE;
                    }
                case -1:
                    {
                        return "-1";
                    }
                default: return null;
            }
            #endregion
        }
    }
}
