using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TXCommons.Admins
{

    //注：该类中的方法为管理平台专用，如需修改(添加、修改、删除方法)要通知管理平台相关开发人员

    /// <summary>
    /// 管理平台 使用到的类型
    /// Author: liyuzhao
    /// CreatTime: 2013-11-26
    /// </summary>
    /// <remarks>
    /// 注意：方法中返回List类型的对象均已做了克隆处理，可以随意修改获取到的对象
    /// 
    /// 前缀含义
    /// Db_:(DataBase)数据库
    /// Tb_:(Table)表
    /// Fc_:(Function)方法
    /// </remarks>
    public static class AdminTypes
    {
        /// <summary>
        /// 错误提示
        /// </summary>
        private static class W_Error
        {
            /// <summary>
            /// 没有找到
            /// </summary>
            public static string NotFound
            {
                get { return "--"; } // ERROR:没有找到}
            }
        }

        /// <summary>
        /// 新房数据库
        /// </summary>
        public static class Db_NewHouse
        {
            #region 数据表

            /// <summary>
            /// Premises表
            /// </summary>
            public static class Tb_Premises
            {
                #region 方法集

                /// <summary>
                /// KeyValuePairs
                /// </summary>
                public static class Fc_Pairs
                {
                    #region 物业类型

                    /// <summary>
                    /// 物业类型(1住宅、2公寓、3别墅、4 商铺、5写字楼)
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_PropertyType()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("0", "住宅"),
                            new KeyValuePair<string, string>("1", "住宅"),
                            new KeyValuePair<string, string>("2", "写字楼"),
                            new KeyValuePair<string, string>("3", "别墅"),
                            new KeyValuePair<string, string>("4", "商业"),
                            // new KeyValuePair<string, string>("5", "写字楼")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }

                    #endregion

                    #region 楼盘状态

                    /// <summary>
                    /// 楼盘状态(0 待售 1在售  2售罄)
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_SalesStatus()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("0", "待售"),
                            new KeyValuePair<string, string>("1", "在售"),
                            new KeyValuePair<string, string>("2", "售罄")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }


                    /// <summary>
                    /// 根据当前状态获取可选择的下拉列表
                    /// </summary>
                    /// <param name="currentState">当前状态值</param>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_StateType_ByCurrentState(object currentState)
                    {
                        var state = Convert.ToInt32(currentState);
                        List<KeyValuePair<string, string>> pairs = null;
                        switch (state)
                        {
                            case 0:
                                pairs = new List<KeyValuePair<string, string>>
                                {
                                    new KeyValuePair<string, string>("0", "待售"),
                                    new KeyValuePair<string, string>("1", "在售")
                                };
                                break;
                            case 1:
                                pairs = new List<KeyValuePair<string, string>>
                                {
                                    new KeyValuePair<string, string>("1", "在售"),
                                    new KeyValuePair<string, string>("2", "售罄")
                                };
                                break;
                            case 2:
                                pairs = new List<KeyValuePair<string, string>>
                                {
                                    new KeyValuePair<string, string>("2", "售罄")
                                };
                                break;
                        }

                        return Clone_KeyValuePairs(pairs);
                    }
                    #endregion

                    #region 环线位置

                    /// <summary>
                    /// 环线位置(1 一环 2二环 3 三环 4四环 5五环 6六环 )
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_Ring()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("1", "一环"),
                            new KeyValuePair<string, string>("2", "二环"),
                            new KeyValuePair<string, string>("3", "三环"),
                            new KeyValuePair<string, string>("4", "四环"),
                            new KeyValuePair<string, string>("5", "五环"),
                            new KeyValuePair<string, string>("6", "六环")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }

                    #endregion

                    #region 建筑类别

                    /// <summary>
                    /// 建筑类别(1板楼 2塔楼 3砖楼 4砖混 5平房 6钢混 )
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_BuildingType()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("1", "板楼"),
                            new KeyValuePair<string, string>("2", "塔楼"),
                            new KeyValuePair<string, string>("3", "砖楼"),
                            new KeyValuePair<string, string>("4", "砖混"),
                            new KeyValuePair<string, string>("5", "平房"),
                            new KeyValuePair<string, string>("6", "钢混")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }

                    #endregion
                }

                /// <summary>
                /// 下拉框
                /// </summary>
                public static class Fc_SelectListItems
                {
                    #region 销售状态

                    /// <summary>
                    /// 新房管理 → 新房数据管理 → 楼盘管理 → 发布楼盘
                    /// </summary>
                    /// <returns></returns>
                    public static List<SelectListItem> Get_SalesStatus()
                    {
                        var list = new List<SelectListItem>();
                        var pairs = Fc_Pairs.Get_SalesStatus();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            list.Add(new SelectListItem { Text = pairs[i].Value, Value = pairs[i].Key });
                        }
                        list.Insert(0, new SelectListItem { Text = "请选择", Value = "-1" });

                        return Clone_SelectListItem(list);
                    }

                    /// <summary>
                    /// 新房管理 → 新房数据管理 → 楼盘管理 → 编辑楼盘
                    /// </summary>
                    /// <param name="currentState">当前状态</param>
                    /// <returns></returns>
                    public static List<SelectListItem> Get_SalesStateByCurrentState(object currentState)
                    {
                        var list = new List<SelectListItem>();
                        var pairs = Fc_Pairs.Get_StateType_ByCurrentState(currentState);

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            list.Add(new SelectListItem { Text = pairs[i].Value, Value = pairs[i].Key });
                        }
                        list.Insert(0, new SelectListItem { Text = "请选择", Value = "-1" });

                        return Clone_SelectListItem(list);
                    }

                    #endregion

                    #region 环线位置

                    /// <summary>
                    /// 新房管理 → 新房数据管理 → 楼盘管理 → 发布楼盘
                    /// </summary>
                    /// <returns></returns>
                    public static List<SelectListItem> Get_Ring()
                    {
                        var list = new List<SelectListItem>();
                        var pairs = Fc_Pairs.Get_Ring();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            list.Add(new SelectListItem { Text = pairs[i].Value, Value = pairs[i].Key });
                        }
                        list.Insert(0, new SelectListItem { Text = "环线", Value = "-1" });

                        return Clone_SelectListItem(list);
                    }

                    #endregion

                    #region 建筑类别

                    /// <summary>
                    /// 新房管理 → 新房数据管理 → 楼盘管理 → 发布楼盘
                    /// </summary>
                    /// <returns></returns>
                    public static List<SelectListItem> Get_BuildingType()
                    {
                        var list = new List<SelectListItem>();
                        var pairs = Fc_Pairs.Get_BuildingType();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            list.Add(new SelectListItem { Text = pairs[i].Value, Value = pairs[i].Key });
                        }
                        list.Insert(0, new SelectListItem { Text = "请选择", Value = "-1" });

                        return Clone_SelectListItem(list);
                    }

                    #endregion
                }

                /// <summary>
                /// 根据编号取值
                /// </summary>
                public static class Fc_ById
                {
                    #region 楼盘状态

                    /// <summary>
                    /// 楼盘状态
                    /// </summary>
                    /// <param name="id">编号</param>
                    /// <returns></returns>
                    public static string For_SalesStatus(object id)
                    {
                        var pairs = Fc_Pairs.Get_SalesStatus();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(id).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }

                    #endregion

                    #region 物业类型

                    /// <summary>
                    /// 物业类型(1住宅、2公寓、3别墅、4 商铺、5写字楼)
                    /// </summary>
                    /// <param name="id">编号</param>
                    /// <returns></returns>
                    public static string For_PropertyType(object id)
                    {
                        var pairs = Fc_Pairs.Get_PropertyType();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(id).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }

                    /// <summary>
                    ///  物业类型(1住宅、2公寓、3别墅、4 商铺、5写字楼)
                    /// </summary>
                    /// <param name="ids">多个物业类型编号</param>
                    /// <returns></returns>
                    public static string For_PropertyTypes(string ids)
                    {
                        if (string.IsNullOrEmpty(ids))
                        {
                            return string.Empty;
                        }

                        var idArr = ids.Split(',');

                        var ppStr = string.Empty;
                        for (int i = 0; i < idArr.Length; i++)
                        {
                            ppStr += For_PropertyType(idArr[i]) + ",";
                        }

                        return ppStr.TrimEnd(',');
                    }

                    #endregion

                    #region 建筑类别

                    /// <summary>
                    /// 建筑类别(1板楼 2塔楼 3砖楼 4砖混 5平房 6钢混 )
                    /// </summary>
                    /// <param name="id">编号</param>
                    /// <returns></returns>
                    public static string For_BuildingType(object id)
                    {
                        var pairs = Fc_Pairs.Get_BuildingType();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(id).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }

                    #endregion

                    #region 楼盘状态变更-->楼栋需要变更的编号

                    /// <summary>
                    /// 楼盘状态变更-->楼栋需要变更的编号
                    /// </summary>
                    /// <param name="state"></param>
                    /// <returns></returns>
                    public static string For_BuildingState(object state)
                    {
                        var t = Convert.ToString(state);
                        switch (t)
                        {
                            case "0":
                                return "1";
                            case "1":
                                return "2";
                            case "2":
                                return "3";
                        }

                        return W_Error.NotFound;
                    }

                    #endregion

                    #region 楼盘状态变更-->房源需要变更的编号

                    /// <summary>
                    /// 楼盘状态变更-->房源需要变更的编号
                    /// </summary>
                    /// <param name="state"></param>
                    /// <returns></returns>
                    public static string For_HouseState(object state)
                    {
                        var t = Convert.ToString(state);
                        switch (t)
                        {
                            case "0":
                                return "0";
                            case "1":
                                return "2";
                            case "2":
                                return "9";
                        }

                        return W_Error.NotFound;
                    }

                    #endregion
                }

                #endregion
            }

            /// <summary>
            /// Developer表
            /// </summary>
            public static class Tb_Developer
            {
                #region 方法集

                /// <summary>
                /// KeyValuePairs
                /// </summary>
                public static class Fc_Pairs
                {
                    #region 开发商审核状态

                    /// <summary>
                    /// 0 未认证 1 已通过 2 未通过
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_SalesStatus()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("0", "未认证"),
                            new KeyValuePair<string, string>("1", "已通过"),
                            new KeyValuePair<string, string>("2", "未通过"),
                            new KeyValuePair<string, string>("3", "审核中"),
                        };

                        return Clone_KeyValuePairs(pairs);
                    }

                    #endregion

                    #region 公司类型

                    /// <summary>
                    /// 1 开发商
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_TypeStatus()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("1", "开发商"),
                            new KeyValuePair<string, string>("2", "代理商")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }

                    #endregion

                    #region 锁定状态

                    /// <summary>
                    /// 锁定状态 0正常 1锁定
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_LockState()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("0", "正常"),
                            new KeyValuePair<string, string>("1", "锁定")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }

                    #endregion

                    /// <summary>
                    /// 委托状态 0前台注册 1后台添加
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_EntrustState()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("0", "否"),
                            new KeyValuePair<string, string>("1", "是")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }
                }

                /// <summary>
                /// 下拉框
                /// </summary>
                public static class Fc_SelectListItems
                {
                    #region 账号状态

                    /// <summary>
                    /// 开发商管理 → 开发商账号管理 
                    /// </summary>
                    /// <returns></returns>
                    public static List<SelectListItem> Get_LockState()
                    {
                        var list = new List<SelectListItem>();
                        var pairs = Fc_Pairs.Get_LockState();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            list.Add(new SelectListItem { Text = pairs[i].Value, Value = pairs[i].Key });
                        }
                        list.Insert(0, new SelectListItem { Text = "账号状态", Value = "-1" });

                        return Clone_SelectListItem(list);
                    }

                    /// <summary>
                    /// 开发商管理 → 委托状态
                    /// </summary>
                    /// <returns></returns>
                    public static List<SelectListItem> Get_EntrustState()
                    {
                        var list = new List<SelectListItem>();
                        var pairs = Fc_Pairs.Get_EntrustState();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            list.Add(new SelectListItem { Text = pairs[i].Value, Value = pairs[i].Key });
                        }
                        list.Insert(0, new SelectListItem { Text = "委托状态", Value = "-1" });

                        return Clone_SelectListItem(list);
                    }

                    #endregion

                    #region 公司类型

                    /// <summary>
                    /// 开发商管理 → 开发商账号管理 → 修改开发商资料→ 身份认证
                    /// </summary>
                    /// <returns></returns>
                    public static List<SelectListItem> Get_CompanyType()
                    {
                        var list = new List<SelectListItem>();
                        var pairs = Fc_Pairs.Get_TypeStatus();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            list.Add(new SelectListItem { Text = pairs[i].Value, Value = pairs[i].Key });
                        }
                        list.Insert(0, new SelectListItem { Text = "全部", Value = "0" });

                        return Clone_SelectListItem(list);
                    }

                    #endregion

                }


                /// <summary>
                /// 根据编号取值
                /// </summary>
                public static class Fc_ById
                {
                    #region 开发商审核状态

                    /// <summary>
                    /// 开发商审核状态
                    /// </summary>
                    /// <param name="id"></param>
                    /// <returns></returns>
                    public static string For_State(object id)
                    {
                        var pairs = Fc_Pairs.Get_SalesStatus();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(id).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }

                    #endregion

                    #region 公司类型

                    /// <summary>
                    /// 公司类型
                    /// </summary>
                    /// <param name="id"></param>
                    /// <returns></returns>
                    public static string For_TypeState(object id)
                    {
                        var pairs = Fc_Pairs.Get_TypeStatus();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(id).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }

                    #endregion

                    #region 锁定状态

                    /// <summary>
                    /// 锁定状态
                    /// </summary>
                    /// <param name="id"></param>
                    /// <returns></returns>
                    public static string For_LockState(object id)
                    {
                        var pairs = Fc_Pairs.Get_LockState();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(id).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }
                    /// <summary>
                    /// 委托状态
                    /// </summary>
                    /// <param name="id"></param>
                    /// <returns></returns>
                    public static string For_EntrustState(object id)
                    {
                        var pairs = Fc_Pairs.Get_EntrustState();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(id).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }
                    #endregion
                }

                #endregion
            }

            /// <summary>
            /// 楼栋
            /// </summary>
            public static class Tb_Building
            {
                #region 方法集

                /// <summary>
                /// KeyValuePairs
                /// </summary>
                public static class Fc_Pairs
                {
                    #region 装修程度

                    /// <summary>
                    /// 装修程度(1毛坯、2简装修、3精装修、4菜单式装修、5公共部分精装修、6全装修)
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_RenovationType()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("1", "毛坯"),
                            new KeyValuePair<string, string>("2", "简装修"),
                            new KeyValuePair<string, string>("3", "精装修"),
                            new KeyValuePair<string, string>("4", "菜单式装修"),
                            new KeyValuePair<string, string>("5", "公共部分精装修"),
                            new KeyValuePair<string, string>("6", "全装修")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }

                    #endregion

                    #region 销售状态

                    /// <summary>
                    /// 销售状态(1在售 2售罄 3建设中 4规划中)
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_StateType()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("1", "待售"),
                            new KeyValuePair<string, string>("2", "在售"),
                            new KeyValuePair<string, string>("3", "售罄"),
                            new KeyValuePair<string, string>("4", "建设中"),
                            new KeyValuePair<string, string>("5", "规划中")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }

                    /// <summary>
                    /// 根据当前状态获取可选择的下拉列表
                    /// </summary>
                    /// <param name="currentState">当前状态值</param>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_StateType_ByCurrentState(object currentState)
                    {
                        var state = Convert.ToInt32(currentState);
                        List<KeyValuePair<string, string>> pairs = null;
                        switch (state)
                        {
                            case 1:
                                pairs = new List<KeyValuePair<string, string>>
                                {
                                    new KeyValuePair<string, string>("1", "待售"),
                                    new KeyValuePair<string, string>("2", "在售")
                                };
                                break;
                            case 2:
                                pairs = new List<KeyValuePair<string, string>>
                                {
                                    new KeyValuePair<string, string>("2", "在售"),
                                    new KeyValuePair<string, string>("3", "售罄")
                                };
                                break;
                            case 3:
                                pairs = new List<KeyValuePair<string, string>>
                                {
                                    new KeyValuePair<string, string>("3", "售罄")
                                };
                                break;
                            case 4:
                                pairs = new List<KeyValuePair<string, string>>
                                {
                                    new KeyValuePair<string, string>("4", "建设中"),
                                    new KeyValuePair<string, string>("2", "在售")
                                };
                                break;
                            case 5:
                                pairs = new List<KeyValuePair<string, string>>
                                {
                                    new KeyValuePair<string, string>("5", "规划中"),
                                    new KeyValuePair<string, string>("2", "在售")
                                };
                                break;
                        }

                        return Clone_KeyValuePairs(pairs);
                    }

                    #endregion

                    #region 楼栋名称后缀

                    /// <summary>
                    /// 楼栋名称后缀(1号楼、2栋、3座、4幢)
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_NameTypes()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("号楼", "号楼"),
                            new KeyValuePair<string, string>("栋", "栋"),
                            new KeyValuePair<string, string>("座", "座"),
                            new KeyValuePair<string, string>("幢", "幢")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }

                    #endregion

                    #region 梯户配比

                    /// <summary>
                    /// 梯户配比(1.1梯2户)
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_Ladder()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("1", "1梯1户"),
                            new KeyValuePair<string, string>("2", "1梯2户"),
                            new KeyValuePair<string, string>("3", "1梯3户"),
                            new KeyValuePair<string, string>("4", "1梯4户"),
                            new KeyValuePair<string, string>("5", "1梯5户"),
                            new KeyValuePair<string, string>("6", "1梯6户"),
                            new KeyValuePair<string, string>("7", "2梯1户"),
                            new KeyValuePair<string, string>("8", "2梯2户"),
                            new KeyValuePair<string, string>("9", "2梯3户"),
                            new KeyValuePair<string, string>("10", "2梯4户"),
                            new KeyValuePair<string, string>("11", "2梯5户"),
                            new KeyValuePair<string, string>("12", "2梯6户")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }

                    #endregion

                    #region 楼栋所处楼盘位置

                    /// <summary>
                    /// 楼栋所处楼盘位置(1临街、2中部、3东部、4西部、5南部、6北部)
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_BuildingPosition()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("1", "临街"),
                            new KeyValuePair<string, string>("2", "中部"),
                            new KeyValuePair<string, string>("3", "东部"),
                            new KeyValuePair<string, string>("4", "西部"),
                            new KeyValuePair<string, string>("5", "南部"),
                            new KeyValuePair<string, string>("6", "北部")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }

                    #endregion

                    #region 物业类型

                    /// <summary>
                    /// 物业类型(1住宅、2公寓、3别墅、4 商铺、5写字楼)
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_PropertyType()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("1", "住宅"),
                            new KeyValuePair<string, string>("2", "写字楼"),
                            new KeyValuePair<string, string>("3", "别墅"),
                            new KeyValuePair<string, string>("4", "商业")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }

                    #endregion
                }

                /// <summary>
                /// 下拉框
                /// </summary>
                public static class Fc_SelectListItems
                {
                    #region 楼栋名称后缀

                    /// <summary>
                    /// 新房管理 → 新房数据管理 → 楼栋管理 → 发布楼盘、编辑楼盘
                    /// </summary>
                    /// <returns></returns>
                    public static List<SelectListItem> Get_NameTypes()
                    {
                        var list = new List<SelectListItem>();
                        var pairs = Fc_Pairs.Get_NameTypes();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            list.Add(new SelectListItem { Text = pairs[i].Value, Value = pairs[i].Key });
                        }
                        list.Insert(0, new SelectListItem { Text = "请选择", Value = "" });

                        return Clone_SelectListItem(list);
                    }

                    #endregion

                    #region 梯户配比

                    /// <summary>
                    /// 新房管理 → 新房数据管理 → 楼栋管理 → 发布楼盘、编辑楼盘
                    /// </summary>
                    /// <returns></returns>
                    public static List<SelectListItem> Get_Ladder()
                    {
                        var list = new List<SelectListItem>();
                        var pairs = Fc_Pairs.Get_Ladder();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            list.Add(new SelectListItem { Text = pairs[i].Value, Value = pairs[i].Key });
                        }
                        list.Insert(0, new SelectListItem { Text = "请选择", Value = "0" });

                        return Clone_SelectListItem(list);
                    }

                    #endregion

                    #region 装修程度

                    /// <summary>
                    /// 新房管理 → 新房数据管理 → 楼栋管理 → 发布楼盘、编辑楼盘
                    /// </summary>
                    /// <returns></returns>
                    public static List<SelectListItem> Get_Renovation()
                    {
                        var list = new List<SelectListItem>();
                        var pairs = Fc_Pairs.Get_RenovationType();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            list.Add(new SelectListItem { Text = pairs[i].Value, Value = pairs[i].Key });
                        }
                        list.Insert(0, new SelectListItem { Text = "请选择", Value = "0" });

                        return Clone_SelectListItem(list);
                    }

                    #endregion

                    #region 楼栋所处楼盘位置

                    /// <summary>
                    /// 新房管理 → 新房数据管理 → 楼栋管理 → 发布楼盘、编辑楼盘
                    /// </summary>
                    /// <returns></returns>
                    public static List<SelectListItem> Get_BuildingPosition()
                    {
                        var list = new List<SelectListItem>();
                        var pairs = Fc_Pairs.Get_BuildingPosition();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            list.Add(new SelectListItem { Text = pairs[i].Value, Value = pairs[i].Key });
                        }
                        list.Insert(0, new SelectListItem { Text = "请选择", Value = "0" });

                        return Clone_SelectListItem(list);
                    }

                    #endregion

                    #region 销售状态

                    /// <summary>
                    /// 新房管理 → 新房数据管理 → 楼栋管理 → 发布楼盘、编辑楼盘
                    /// </summary>
                    /// <returns></returns>
                    public static List<SelectListItem> Get_State()
                    {
                        var list = new List<SelectListItem>();
                        var pairs = Fc_Pairs.Get_StateType();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            list.Add(new SelectListItem { Text = pairs[i].Value, Value = pairs[i].Key });
                        }
                        list.Insert(0, new SelectListItem { Text = "请选择", Value = "0" });

                        return Clone_SelectListItem(list);
                    }

                    /// <summary>
                    /// 根据当前状态获取可选择的下拉列表
                    /// </summary>
                    /// <param name="currentState">当前状态值</param>
                    /// <returns></returns>
                    public static List<SelectListItem> Get_State_ByCurrentState(object currentState)
                    {
                        var list = new List<SelectListItem>();
                        var pairs = Fc_Pairs.Get_StateType_ByCurrentState(currentState);

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            list.Add(new SelectListItem { Text = pairs[i].Value, Value = pairs[i].Key });
                        }
                        list.Insert(0, new SelectListItem { Text = "请选择", Value = "0" });

                        return Clone_SelectListItem(list);
                    }

                    #endregion
                }

                /// <summary>
                /// 根据编号取值
                /// </summary>
                public static class Fc_ById
                {
                    #region 装修程度

                    /// <summary>
                    /// 装修程度
                    /// </summary>
                    /// <param name="id"></param>
                    /// <returns></returns>
                    public static string For_Renovation(object id)
                    {
                        var pairs = Fc_Pairs.Get_RenovationType();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(id).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }

                    #endregion

                    #region 销售状态

                    /// <summary>
                    /// 销售状态
                    /// </summary>
                    /// <param name="id"></param>
                    /// <returns></returns>
                    public static string For_State(object id)
                    {
                        var pairs = Fc_Pairs.Get_StateType();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(id).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }

                    #endregion

                    #region 楼栋名称后缀

                    /// <summary>
                    /// 楼栋名称后缀
                    /// </summary>
                    /// <param name="name"></param>
                    /// <returns></returns>
                    public static string For_NameType(object name)
                    {
                        var pairs = Fc_Pairs.Get_NameTypes();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(name).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }

                    #endregion

                    #region 梯户配比

                    /// <summary>
                    /// 梯户配比
                    /// </summary>
                    /// <param name="id"></param>
                    /// <returns></returns>
                    public static string For_Ladder(object id)
                    {
                        var pairs = Fc_Pairs.Get_Ladder();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(id).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }

                    #endregion

                    #region 物业类型

                    /// <summary>
                    /// 梯户配比
                    /// </summary>
                    /// <param name="id"></param>
                    /// <returns></returns>
                    public static string For_PropertyType(object id)
                    {
                        var pairs = Fc_Pairs.Get_PropertyType();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(id).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }

                    /// <summary>
                    /// 梯户配比
                    /// </summary>
                    /// <param name="ids">逗号分隔</param>
                    /// <returns></returns>
                    public static string For_PropertyTypes(string ids)
                    {
                        if (string.IsNullOrEmpty(ids))
                        {
                            return string.Empty;
                        }

                        var idArr = ids.Split(',');

                        var ppStr = string.Empty;
                        for (int i = 0; i < idArr.Length; i++)
                        {
                            ppStr += For_PropertyType(idArr[i]) + ",";
                        }

                        return ppStr.TrimEnd(',');
                    }

                    #endregion

                    #region 楼栋状态与房源状态对应关系

                    /// <summary>
                    /// 楼栋状态与房源状态对应关系
                    /// </summary>
                    /// <param name="state">楼栋状态</param>
                    /// <returns></returns>
                    public static int For_MatchHouseStateByState(object state)
                    {
                        switch (Convert.ToString(state))
                        {
                            case "2":
                                return 2;
                            case "3":
                                return 9;
                            default:
                                return 0;
                        }
                    }

                    #endregion
                }

                #endregion
            }

            /// <summary>
            /// House表
            /// </summary>
            public static class Tb_House
            {
                #region 方法集

                /// <summary>
                /// KeyValuePairs
                /// </summary>
                public static class Fc_Pairs
                {
                    #region 房源销售状态

                    /// <summary>
                    /// 销售状态0 待售1开发商保留2 可售3 已认购4 已签约5 已备案6 已办产权7 被限制8 拆迁安置
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_SalesStatus()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("0", "待售"),
                            new KeyValuePair<string, string>("1", "开发商保留"),
                            new KeyValuePair<string, string>("2", "在售"),
                            new KeyValuePair<string, string>("3", "已认购"),
                            new KeyValuePair<string, string>("4", "已签约"),
                            new KeyValuePair<string, string>("5", "已备案"),
                            new KeyValuePair<string, string>("6", "已办产权"),
                            new KeyValuePair<string, string>("7", "被限制"),
                            new KeyValuePair<string, string>("8", "拆迁安置"),
                            new KeyValuePair<string, string>("9", "售罄"),
                        };

                        return Clone_KeyValuePairs(pairs);
                    }

                    #endregion

                    #region 房源价格类型

                    /// <summary>
                    /// 0 市场价格 1 参考价格
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_PriceType()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("0", "市场价格"),
                            new KeyValuePair<string, string>("1", "参考价格")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }

                    #endregion

                    #region 建筑形式

                    /// <summary>
                    /// 建筑形式1 开间2 错层3 跃层4 复式5 平层
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_BuildingType()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("1", "开间"),
                            new KeyValuePair<string, string>("2", "错层"),
                            new KeyValuePair<string, string>("3", "跃层"),
                            new KeyValuePair<string, string>("4", "复式"),
                            new KeyValuePair<string, string>("5", "平层")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }

                    #endregion

                    #region 采光情况

                    /// <summary>
                    /// 采光情况1东、2南、3西、4北、5东南、6东北、7西南、8西北、9南北、10东西
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_Orientation()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("1", "东"),
                            new KeyValuePair<string, string>("2", "南"),
                            new KeyValuePair<string, string>("3", "西"),
                            new KeyValuePair<string, string>("4", "北"),
                            new KeyValuePair<string, string>("5", "东南"),
                            new KeyValuePair<string, string>("6", "东北"),
                            new KeyValuePair<string, string>("7", "西南"),
                            new KeyValuePair<string, string>("8", "西北"),
                            new KeyValuePair<string, string>("9", "南北"),
                            new KeyValuePair<string, string>("10", "东西")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }

                    #endregion
                }

                /// <summary>
                /// 根据编号取值
                /// </summary>
                public static class Fc_ById
                {
                    #region 房源销售状态

                    /// <summary>
                    /// 房源销售状态
                    /// </summary>
                    /// <param name="id"></param>
                    /// <returns></returns>
                    public static string For_SalesStatus(object id)
                    {
                        var pairs = Fc_Pairs.Get_SalesStatus();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(id).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }

                    #endregion

                    #region 房源价格类型

                    /// <summary>
                    /// 房源价格类型
                    /// </summary>
                    /// <param name="id"></param>
                    /// <returns></returns>
                    public static string For_PriceType(object id)
                    {
                        var pairs = Fc_Pairs.Get_PriceType();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(id).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }

                    #endregion

                    #region 建筑形式

                    /// <summary>
                    /// 建筑形式1 开间2 错层3 跃层4 复式5 平层
                    /// </summary>
                    /// <returns></returns>
                    public static string For_BuildingType(object id)
                    {
                        var pairs = Fc_Pairs.Get_BuildingType();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(id).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }

                    #endregion

                    #region 采光情况

                    /// <summary>
                    /// 采光情况1东、2南、3西、4北、5东南、6东北、7西南、8西北、9南北、10东西
                    /// </summary>
                    /// <returns></returns>
                    public static string For_Orientation(object id)
                    {
                        var pairs = Fc_Pairs.Get_Orientation();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(id).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }

                    #endregion
                }

                /// <summary>
                /// 下拉框
                /// </summary>
                public static class Fc_SelectListItems
                {
                    #region 房源销售状态

                    /// <summary>
                    /// 房源销售状态
                    /// </summary>
                    /// <returns></returns>
                    public static List<SelectListItem> Get_SalesStatus()
                    {
                        var list = new List<SelectListItem>();
                        var pairs = Fc_Pairs.Get_SalesStatus();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            list.Add(new SelectListItem { Text = pairs[i].Value, Value = pairs[i].Key });
                        }
                        list.Insert(0, new SelectListItem { Text = "销售状态", Value = "-1" });

                        return Clone_SelectListItem(list);
                    }

                    #endregion

                    #region 建筑形式

                    /// <summary>
                    /// 建筑形式
                    /// </summary>
                    /// <returns></returns>
                    public static List<SelectListItem> Get_BuildingType()
                    {
                        var list = new List<SelectListItem>();
                        var pairs = Fc_Pairs.Get_BuildingType();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            list.Add(new SelectListItem { Text = pairs[i].Value, Value = pairs[i].Key });
                        }
                        list.Insert(0, new SelectListItem { Text = "请选择", Value = "0" });

                        return Clone_SelectListItem(list);
                    }

                    #endregion

                    #region 采光情况

                    /// <summary>
                    /// 采光情况1东、2南、3西、4北、5东南、6东北、7西南、8西北、9南北、10东西
                    /// </summary>
                    /// <returns></returns>
                    public static List<SelectListItem> Get_Orientation()
                    {
                        var list = new List<SelectListItem>();
                        var pairs = Fc_Pairs.Get_Orientation();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            list.Add(new SelectListItem { Text = pairs[i].Value, Value = pairs[i].Key });
                        }
                        list.Insert(0, new SelectListItem { Text = "请选择", Value = "0" });

                        return Clone_SelectListItem(list);
                    }

                    #endregion
                }

                #endregion
            }

            /// <summary>
            /// 活动表
            /// </summary>
            public static class Tb_Activities
            {
                #region 方法集

                /// <summary>
                /// KeyValuePairs
                /// </summary>
                public static class Fc_Pairs
                {
                    #region 活动状态

                    /// <summary>
                    /// 活动状态 0未开始 1已开始 2已结束
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_ActivitieStates()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("0", "未开始"),
                            new KeyValuePair<string, string>("1", "已开始"),
                            new KeyValuePair<string, string>("2", "已结束")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }

                    #endregion

                    #region 摇号标记状态

                    /// <summary>
                    /// 摇号标记状态0 待处理 1 已通过 2 未通过 3未联系 4 已联系 5 活动已发布 6已结束
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_MarkStates()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("0", "待处理"),
                            new KeyValuePair<string, string>("1", "已通过"),
                            new KeyValuePair<string, string>("2", "未通过"),
                            new KeyValuePair<string, string>("3", "未联系"),
                            new KeyValuePair<string, string>("4", "已联系"),
                            new KeyValuePair<string, string>("5", "活动已发布"),
                            new KeyValuePair<string, string>("6", "已结束")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }

                    #endregion

                    #region 摇号公证处

                    /// <summary>
                    /// 摇号公证处
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_NotarialOffice()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("公证处1", "公证处1"),
                            new KeyValuePair<string, string>("公证处2", "公证处2"),
                            new KeyValuePair<string, string>("公证处3", "公证处3"),
                            new KeyValuePair<string, string>("公证处4", "公证处4")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }

                    #endregion
                }

                /// <summary>
                /// 根据编号取值
                /// </summary>
                public static class Fc_ById
                {
                    #region 活动状态

                    /// <summary>
                    /// 活动状态
                    /// </summary>
                    /// <param name="id">活动状态</param>
                    /// <returns></returns>
                    public static string For_ActivitieStates(object id)
                    {
                        var pairs = Fc_Pairs.Get_ActivitieStates();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(id).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }

                    /// <summary>
                    /// 活动状态
                    /// </summary>
                    /// <param name="beginTime">活动开始时间</param>
                    /// <param name="endTime">活动结束时间</param>
                    /// <param name="now">当前时间(DateTime.Now)</param>
                    /// <returns></returns>
                    public static string For_ActivitieStates(DateTime beginTime, DateTime endTime, DateTime now)
                    {
                        if (now < beginTime)
                        {
                            return "未开始";
                        }
                        if (beginTime < now && now < endTime)
                        {
                            return "已开始";
                        }
                        if (endTime < now)
                        {
                            return "已结束";
                        }

                        return W_Error.NotFound;
                    }

                    #endregion

                    #region 摇号标记状态

                    /// <summary>
                    /// 摇号标记状态
                    /// </summary>
                    /// <param name="id"></param>
                    /// <returns></returns>
                    public static string For_MarkStates(object id)
                    {
                        var pairs = Fc_Pairs.Get_MarkStates();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(id).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }

                    #endregion

                    #region 摇号公证处

                    /// <summary>
                    /// 摇号公证处
                    /// </summary>
                    /// <param name="id"></param>
                    /// <returns></returns>
                    public static string For_NotarialOffice(object id)
                    {
                        var pairs = Fc_Pairs.Get_NotarialOffice();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(id).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }

                    #endregion
                }

                /// <summary>
                /// 下拉框
                /// </summary>
                public static class Fc_SelectListItems
                {
                    #region 活动状态

                    /// <summary>
                    /// 活动状态
                    /// </summary>
                    /// <returns></returns>
                    public static List<SelectListItem> Get_ActivitieStates()
                    {
                        var list = new List<SelectListItem>();
                        var pairs = Fc_Pairs.Get_ActivitieStates();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            list.Add(new SelectListItem { Text = pairs[i].Value, Value = pairs[i].Key });
                        }
                        list.Insert(0, new SelectListItem { Text = "活动状态", Value = "-1" });

                        return Clone_SelectListItem(list);
                    }

                    #endregion

                    #region 摇号标记状态

                    /// <summary>
                    /// 摇号标记状态
                    /// </summary>
                    /// <returns></returns>
                    public static List<SelectListItem> Get_MarkStates()
                    {
                        var list = new List<SelectListItem>();
                        var pairs = Fc_Pairs.Get_MarkStates();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            list.Add(new SelectListItem { Text = pairs[i].Value, Value = pairs[i].Key });
                        }
                        list.Insert(0, new SelectListItem { Text = "全部", Value = "-1" });

                        return Clone_SelectListItem(list);
                    }

                    #endregion

                    #region 摇号公证处

                    /// <summary>
                    /// 摇号公证处
                    /// </summary>
                    /// <returns></returns>
                    public static List<SelectListItem> Get_NotarialOffice()
                    {
                        var list = new List<SelectListItem>();
                        var pairs = Fc_Pairs.Get_NotarialOffice();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            list.Add(new SelectListItem { Text = pairs[i].Value, Value = pairs[i].Key });
                        }
                        list.Insert(0, new SelectListItem { Text = "选择", Value = "-1" });

                        return Clone_SelectListItem(list);
                    }

                    #endregion
                }

                #endregion
            }

            public static class Tb_Order
            {
                #region 方法集

                /// <summary>
                /// KeyValuePairs
                /// </summary>
                public static class Fc_Pairs
                {
                    /// <summary>
                    /// 活动状态 0未支付 1已支付 2已退还
                    /// </summary>
                    /// <returns></returns>
                    public static List<KeyValuePair<string, string>> Get_BondStatus()
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("0", "未支付"),
                            new KeyValuePair<string, string>("1", "已支付"),
                            new KeyValuePair<string, string>("2", "已退还")
                        };

                        return Clone_KeyValuePairs(pairs);
                    }
                }

                /// <summary>
                /// 下拉框
                /// </summary>
                public static class Fc_SelectListItems
                {

                }

                /// <summary>
                /// 根据编号取值
                /// </summary>
                public static class Fc_ById
                {
                    /// <summary>
                    /// 活动状态
                    /// </summary>
                    /// <param name="id">活动状态</param>
                    /// <returns></returns>
                    public static string For_BondStatus(object id)
                    {
                        var pairs = Fc_Pairs.Get_BondStatus();

                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (Convert.ToString(id).Trim().Equals(pairs[i].Key))
                            {
                                return pairs[i].Value;
                            }
                        }

                        return W_Error.NotFound;
                    }
                }

                #endregion
            }

            #endregion
        }

        #region 本类专用方法

        /// <summary>
        /// 类内专用 克隆 KeyValuePairs_string,string_ 
        /// </summary>
        /// <param name="pairs"></param>
        /// <returns></returns>
        private static List<KeyValuePair<string, string>> Clone_KeyValuePairs(List<KeyValuePair<string, string>> pairs)
        {
            if (null == pairs)
            {
                return null;
            }

            var clonePairs = new List<KeyValuePair<string, string>>();

            for (int i = 0; i < pairs.Count; i++)
            {
                clonePairs.Add(new KeyValuePair<string, string>(pairs[i].Key, pairs[i].Value));
            }

            return clonePairs;
        }

        /// <summary>
        /// 类内专用 克隆 SelectListItems
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static List<SelectListItem> Clone_SelectListItem(List<SelectListItem> list)
        {
            if (null == list)
            {
                return null;
            }

            var cloneList = new List<SelectListItem>();

            for (int i = 0; i < list.Count; i++)
            {
                cloneList.Add(new SelectListItem
                {
                    Text = list[i].Text,
                    Value = list[i].Value
                });
            }

            return cloneList;
        }

        #endregion

    }
}