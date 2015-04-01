using System.Collections.Generic;
using System.Text;
using TXCommons.Index;
using System.Data;
using TXCommons;
using TXCommons.NewHouseWeb;

namespace TXNewHouseWeb.Common
{
    public class SEO
    {
        #region 搜索列表页

        /// <summary>
        /// 搜索列表页（新房默认首页）
        /// </summary>
        /// <param name="seoModel"></param>
        /// <param name="realUrl"></param>
        /// <returns></returns>
        public SEOModel SeoSearchList(SEOModel seoModel, string cityName, string citypy, IndexPremisesConditionInfo info)
        {
            string value = string.Empty;
            StringBuilder sb = new StringBuilder();
            List<string> List = new List<string>();
            if (cityName == null)
            {
                cityName = "北京";
            }
            try
            {
                //Title：<城市> +新房_<城市>+新楼盘_<城市>+新开楼盘_快有家房产网
                //Keywords：<城市>+新房,<城市>+新楼盘,<城市>+新开楼盘,<城市>+新房开盘,<城市>+新房信息网
                //Description：<城市>+新房网为您提供<城市>每天最新的新房、新楼盘、新开楼盘信息。<城市>商圈房、<城市>学区房、<城市>医院附近房等,快有家房产网让您方便快捷找到<城市>新房新楼盘信息.

                #region
                if (!string.IsNullOrEmpty(cityName))//城市
                {
                    sb.Append(cityName);
                    //List.Add(cityName);
                }
                if (!string.IsNullOrEmpty(info.DistrictPY))//区域
                {
                    value = SEOConditon.GetTrafficData(citypy, info.DistrictPY, info.BusinessPY, info.Type);
                    sb.Append(value);
                    List.Add(cityName + value + "新房");
                }
                if (!string.IsNullOrEmpty(info.PropertyType))//物业类型
                {
                    sb.Append(info.PropertyType);
                    List.Add(cityName + info.PropertyType + "新房");
                }
                if (!string.IsNullOrEmpty(info.Ring))//环线
                {
                    sb.Append(info.Ring);
                    List.Add(cityName + info.Ring + "新房");
                }
                if (!string.IsNullOrEmpty(info.Room))//居室
                {
                    sb.Append(info.Room);
                    List.Add(cityName + info.Room + "新房");
                }
                if (!string.IsNullOrEmpty(info.SalesStatus))//销售状态
                {
                    sb.Append(info.SalesStatus);
                    List.Add(cityName + info.SalesStatus + "新房");
                }
                //if (!string.IsNullOrEmpty(info.TrafficLine))//地铁线
                //{
                //    sb.Append(info.TrafficLine);
                //    List.Add(cityName + info.TrafficLine + "新房");
                //}
                //if (!string.IsNullOrEmpty(info.TrafficStation))//地铁站
                //{
                //    sb.Append(info.TrafficStation);
                //    List.Add(cityName + info.TrafficStation + "新房");
                //}
                if (!string.IsNullOrEmpty(info.PriceBegin) && !string.IsNullOrEmpty(info.PriceEnd))//价格
                {
                    if (info.PriceBegin == "0")
                    {
                        value = info.PriceEnd + "元以下";
                        sb.Append(value);
                        List.Add(cityName + value + "新房");
                    }
                    else if (info.PriceEnd == "0")
                    {
                        value = info.PriceBegin + "元以上";
                        sb.Append(value);
                        List.Add(cityName + value + "新房");
                    }
                    else
                    {
                        value = info.PriceBegin + "-" + info.PriceEnd + "元";
                        sb.Append(value);
                        List.Add(cityName + value + "新房");
                    }
                }
                if (!string.IsNullOrEmpty(info.Renovation))//装修程度
                {
                    sb.Append(info.Renovation);
                    List.Add(cityName + info.Renovation + "新房");
                }
                if (!string.IsNullOrEmpty(info.BuildingType))//建筑类别
                {
                    sb.Append(info.BuildingType);
                    List.Add(cityName + info.BuildingType + "新房");
                }
                if (!string.IsNullOrEmpty(info.AreaBegin) && !string.IsNullOrEmpty(info.AreaEnd))//面积
                {
                    if (info.AreaBegin == "0")
                    {
                        value = info.AreaEnd + "平米以下";
                        sb.Append(value);
                        List.Add(cityName + value + "新房");
                    }
                    else if (info.AreaEnd == "0")
                    {
                        value = info.AreaBegin + "平米以上";
                        sb.Append(value);
                        List.Add(cityName + value + "新房");
                    }
                    else
                    {
                        value = info.AreaBegin + "-" + info.AreaEnd + "平米";
                        sb.Append(value);
                        List.Add(cityName + value + "新房");
                    }
                }
                if (!string.IsNullOrEmpty(info.Characteristic))
                {
                    sb.Append(info.Characteristic);
                    List.Add(cityName + info.Characteristic + "新房");
                }
                if (!string.IsNullOrEmpty(info.OpeningTime))
                {
                    sb.Append(info.OpeningTime);
                    List.Add(cityName + info.OpeningTime + "新房");
                }
                #endregion
                seoModel.Title = string.Format("{0}新房_{1}新房新楼盘_快有家房产网", sb.ToString(), cityName);
                if (List.Count > 0)
                    seoModel.Keywords = string.Join(",", List.ToArray());
                else
                    seoModel.Keywords = string.Format("{0}新房,{0}新楼盘,{0}新开楼盘,{0}新房开盘,{0}新房信息网", cityName);
                seoModel.Description = string.Format("{1}新房网为您提供{0}新房新楼盘信息,快有家新房网为您提供新房新楼盘信息.", sb.ToString(), cityName);
                return seoModel;
            }
            catch
            {
                seoModel.Title = string.Format("新房新楼盘_快有家房产网");
                seoModel.Keywords = string.Format("新房,新楼盘,新开楼盘,新房开盘,新房信息网");
                seoModel.Description = string.Format("新房网为您提供每天最新的新房、新楼盘、新开楼盘信息。商圈房、学区房、医院附近房等,快有家房产网让您方便快捷找到新房新楼盘信息.");
                return seoModel;
            }
        }

        #endregion

        #region 房源详情页

        /// <summary>
        /// 房源详情页
        /// </summary>
        /// <param name="seoModel"></param>
        /// <param name="cityName"></param>
        /// <param name="title"></param>
        /// <param name="village"></param>
        /// <returns></returns>
        public SEOModel SeoDetail(SEOModel seoModel, string cityName, string title, string village)
        {
            //Title：<业主上传标题信息>_ <城市>+新房新楼盘_快有家房产网
            //Keywords：<小区名>+小区新房，<城市>+新房，<城市>+新楼盘
            //Description：<业主上传标题信息>，是快有家<城市>新房网为您提供的实时房源，<小区名>小区房源来自北京快有家房产网, 快有家新房网为您提供新房新楼盘信息.
            string value = string.Empty;
            StringBuilder sb = new StringBuilder();
            List<string> List = new List<string>();
            if (cityName == null)
            {
                cityName = "北京";
            }
            try
            {
                seoModel.Title = string.Format("{0}新房新楼盘_快有家房产网", cityName);
                seoModel.Keywords = string.Format("{2}小区新房，{0}新房，{0}新楼盘", cityName, village);
                seoModel.Description = string.Format("{0}，是快有家{2}新房网为您提供的实时房源，{1}小区房源来自北京快有家房产网, 快有家新房网为您提供新房新楼盘信息.", title, village, cityName);
                return seoModel;
            }
            catch
            {
                seoModel.Title = string.Format("新房新楼盘_快有家房产网");
                seoModel.Keywords = string.Format("新房信息_新房网_快有家");
                seoModel.Description = string.Format("新房网为您提供新房新楼盘信息,快有家新房网为您提供新房新楼盘信息.");
                return seoModel;
            }
        }

        #endregion

        #region 楼盘—首页

        /// <summary>
        /// 楼盘—首页
        /// </summary>
        /// <param name="seoModel"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public SEOModel SeoPremisesIndex(SEOModel seoModel, string cityName,string premisesName)
        {
            //Title：<城市>+新房_新楼盘_新开楼盘_快有家房产网
            //Keywords：新楼盘、新楼盘开盘、新开楼盘、新楼盘销售、新楼盘信息、2014最新楼盘
            //Description：快有家房产网提供<城市>楼盘,<城市>新楼盘信息,新开楼盘,了解更多新房新楼盘信息就到快有家房产网.
            if (cityName == null)
            {
                cityName = "北京";
            }
            try
            {
                seoModel.Title = string.Format("{1}_{0}新房_新楼盘_新开楼盘_快有家房产网", cityName, premisesName);
                seoModel.Keywords = string.Format("{0}、新楼盘、新楼盘开盘、新开楼盘、新楼盘销售、新楼盘信息、2014最新楼盘",premisesName);
                seoModel.Description = string.Format("快有家房产网提供{0}楼盘,{0}新楼盘信息,新开楼盘,了解更多新房新楼盘信息就到快有家房产网.", cityName);
                return seoModel;
            }
            catch
            {
                seoModel.Title = string.Format("新房_新楼盘_新开楼盘_快有家房产网");
                seoModel.Keywords = string.Format("新楼盘、新楼盘开盘、新开楼盘、新楼盘销售、新楼盘信息、2014最新楼盘");
                seoModel.Description = string.Format("快有家房产网提供楼盘,新楼盘信息,新开楼盘,了解更多新房新楼盘信息就到快有家房产网.");
                return seoModel;
            }
        }

        #endregion

        #region 楼盘—详细信息

        /// <summary>
        /// 楼盘—首页
        /// </summary>
        /// <param name="seoModel"></param>
        /// <param name="cityName"></param>
        /// <param name="premises"></param>
        /// <returns></returns>
        public SEOModel SeoPremisesDetail(SEOModel seoModel, string cityName, TXOrm.Premis premises)
        {
            //Title：<楼盘名称>详细信息+<城市>+新房_新开楼盘_快有家房产网
            //Keywords：<楼盘名称>+详细信息,物业类型,建筑类别,楼盘介绍,楼盘均价
            //Description：快有家房产网提供楼盘详细信息, 物业类型,建筑信息,楼盘介绍,楼盘均价等，更多关于<楼盘名称>楼盘详细信息请到快有家房产网.

            string value = string.Empty;
            StringBuilder sb = new StringBuilder();
            List<string> List = new List<string>();
            if (cityName == null)
            {
                cityName = "北京";
            }
            try
            {
                string[] p = premises.PropertyType.Split(',');
                string str = "";
                for (int i = 0; i < p.Length;i++ )
                {
                    str += SEOConditon.GetPropertyType(p[i])+",";
                }
                sb.Append("物业类型:" + str.TrimEnd(',') + ",建筑类别:" + SEOConditon.GetJZJG(premises.BuildingType.ToString()) + ",楼盘介绍:" + premises.PremisesIntroduction + ",楼盘均价:" + premises.ReferencePrice + "");
                seoModel.Title = string.Format("{1}详细信息_{0}新房_新开楼盘_快有家房产网", cityName, premises.Name);
                seoModel.Keywords = string.Format("{0}详细信息,{1}", premises.Name, sb);
                seoModel.Description = string.Format("快有家房产网提供楼盘详细信息,{1}，更多关于{0}楼盘详细信息请到快有家房产网.", premises.Name,sb);
                return seoModel;
            }
            catch
            {
                seoModel.Title = string.Format("详细信息_新房_新开楼盘_快有家房产网");
                seoModel.Keywords = string.Format("详细信息,物业类型,建筑信息,楼盘介绍,楼栋开盘信息,楼盘均价");
                seoModel.Description = string.Format("快有家房产网提供楼盘详细信息, 物业类型,建筑信息,楼盘介绍,楼盘均价等，更多关于楼盘详细信息请到快有家房产网.");
                return seoModel;
            }
        }

        #endregion

        #region 房源列表页

        /// <summary>
        /// 房源列表页
        /// </summary>
        /// <param name="seoModel"></param>
        /// <param name="cityName"></param>
        /// <param name="premises"></param>
        /// <returns></returns>
        public SEOModel SeoHouseList(SEOModel seoModel, string cityName, string premises)
        {
            //Title：<城市>+新房源_最新房源_<新开楼盘>_快有家房产网
            //Keywords：新楼盘、最新楼盘、<城市>+新楼盘大全
            //Description：快有家房产网提供<城市>+新楼盘、<城市>+最新楼盘、<城市>+新楼盘大全，更多关于<楼盘名称>楼盘详细信息请到快有家房产网.
            string value = string.Empty;
            StringBuilder sb = new StringBuilder();
            List<string> List = new List<string>();
            if (cityName == null)
            {
                cityName = "北京";
            }
            try
            {
                seoModel.Title = string.Format("{0}新房源_最新房源_{1}_快有家房产网", cityName, premises);
                seoModel.Keywords = string.Format("新楼盘、最新楼盘、{0}新楼盘大全", premises);
                seoModel.Description = string.Format("快有家房产网提供{0}新楼盘、{0}最新楼盘、{0}新楼盘大全，更多关于{1}楼盘详细信息请到快有家房产网.", cityName, premises);
                return seoModel;
            }
            catch
            {
                seoModel.Title = string.Format("新房源_最新房源_快有家房产网");
                seoModel.Keywords = string.Format("新楼盘、最新楼盘、新楼盘大全");
                seoModel.Description = string.Format("快有家房产网提供新楼盘、最新楼盘、新楼盘大全，更多楼盘详细信息请到快有家房产网.");
                return seoModel;
            }
        }

        #endregion

        #region 房间信息页

        /// <summary>
        /// 房间信息页
        /// </summary>
        /// <param name="seoModel"></param>
        /// <param name="cityName"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public SEOModel SeoHouseIndex(SEOModel seoModel, string cityName, string title)
        {
            //Title：<房间标题信息>_ <城市>+新房_新开楼盘_快有家房产网
            //Keywords：<房间标题信息>+新房，新楼盘，新开楼盘
            //Description：<房间标题信息>，快有家新房网为您提供新房新楼盘信息.

            string value = string.Empty;
            StringBuilder sb = new StringBuilder();
            List<string> List = new List<string>();
            if (cityName == null)
            {
                cityName = "北京";
            }
            try
            {
                seoModel.Title = string.Format("{1}_{0}新房_新开楼盘_快有家房产网", cityName, title);
                seoModel.Keywords = string.Format("{0}新房，新楼盘，新开楼盘", title);
                seoModel.Description = string.Format("{0}，快有家新房网为您提供新房新楼盘信息.", title);
                return seoModel;
            }
            catch
            {
                seoModel.Title = string.Format("新房_新开楼盘_快有家房产网");
                seoModel.Keywords = string.Format("新房，新楼盘，新开楼盘");
                seoModel.Description = string.Format("快有家新房网为您提供新房新楼盘信息");
                return seoModel;
            }
        }

        #endregion

        #region 户型图—首页

        /// <summary>
        /// 户型图—首页
        /// </summary>
        /// <param name="seoModel"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public SEOModel SeoSizeChartIndex(SEOModel seoModel, string cityName)
        {
            //Title：户型图_<城市>+新房_新开楼盘_快有家房产网
            //Keywords：户型图、户型、户型图大全
            //Description：快有家房产网提供户型图大全.

            string value = string.Empty;
            StringBuilder sb = new StringBuilder();
            List<string> List = new List<string>();
            if (cityName == null)
            {
                cityName = "北京";
            }
            try
            {
                seoModel.Title = string.Format("户型图_{0}新房_新开楼盘_快有家房产网", cityName);
                seoModel.Keywords = string.Format("户型图、户型、户型图大全");
                seoModel.Description = string.Format("快有家房产网提供户型图大全.");
                return seoModel;
            }
            catch
            {
                seoModel.Title = string.Format("户型图_新房_新开楼盘_快有家房产网");
                seoModel.Keywords = string.Format("户型图、户型、户型图大全");
                seoModel.Description = string.Format("快有家房产网提供户型图大全.");
                return seoModel;
            }
        }

        #endregion

        #region 户型图—图片详情页

        /// <summary>
        /// 户型图—图片详情页
        /// </summary>
        /// <param name="seoModel"></param>
        /// <param name="cityName"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        public SEOModel SeoSizeChartDetail(SEOModel seoModel, string cityName, string room, string type)
        {
            //Title：<对应厅室>户型图_<城市>+新房_新开楼盘_快有家房产网
            //Keywords：<对应厅室>户型图、户型图、户型、户型图大全
            //Description：快有家房产网提供户型图大全，<对应厅室>户型图

            string value = string.Empty;
            StringBuilder sb = new StringBuilder();
            List<string> List = new List<string>();
            if (cityName == null)
            {
                cityName = "北京";
            }
            try
            {
                if (type == "hxt")
                {
                    seoModel.Title = string.Format("{1}户型图_{0}新房_新开楼盘_快有家房产网", cityName,room);
                    seoModel.Keywords = string.Format("{0}户型图、户型图、户型、户型图大全", room);
                    seoModel.Description = string.Format("快有家房产网提供户型图大全，{0}户型图",room);
                    return seoModel;
                }
                else
                {
                    seoModel.Title = string.Format("{1}_{0}新房_新开楼盘_快有家房产网", cityName, AlbumType.GetAlbumType(type));
                    seoModel.Keywords = string.Format("{0}、户型图、户型、户型图大全", AlbumType.GetAlbumType(type));
                    seoModel.Description = string.Format("快有家房产网提供{0}大全，{0}", AlbumType.GetAlbumType(type));
                    return seoModel;
                }
            }
            catch
            {
                seoModel.Title = string.Format("户型图_新房_新开楼盘_快有家房产网");
                seoModel.Keywords = string.Format("户型图、户型、户型图大全");
                seoModel.Description = string.Format("快有家房产网提供户型图大全.");
                return seoModel;
            }
        }

        #endregion

        #region 楼盘相册

        /// <summary>
        /// 楼盘相册
        /// </summary>
        /// <param name="seoModel"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public SEOModel SeoAlbum(SEOModel seoModel, string cityName)
        {
            //Title：楼盘相册_楼盘照片_ <城市>+新房_新开楼盘_快有家房产网
            //Keywords：楼盘相册,新楼盘照片,楼盘实景图,户型图,规划图,位置图,楼栋平面图,效果图
            //Description：快有家房产网提供楼盘相册大全，新楼盘照片,楼盘实景图,户型图,规划图,位置图,楼栋平面图,效果图.

            string value = string.Empty;
            StringBuilder sb = new StringBuilder();
            List<string> List = new List<string>();
            if (cityName == null)
            {
                cityName = "北京";
            }
            try
            {
                seoModel.Title = string.Format("楼盘相册_楼盘照片_{0}新房_新开楼盘_快有家房产网", cityName);
                seoModel.Keywords = string.Format("楼盘相册,新楼盘照片,楼盘实景图,户型图,规划图,位置图,楼栋平面图,效果图");
                seoModel.Description = string.Format("快有家房产网提供楼盘相册大全，新楼盘照片,楼盘实景图,户型图,规划图,位置图,楼栋平面图,效果图.");
                return seoModel;
            }
            catch
            {
                seoModel.Title = string.Format("楼盘相册_楼盘照片_新房_新开楼盘_快有家房产网");
                seoModel.Keywords = string.Format("楼盘相册,新楼盘照片,楼盘实景图,户型图,规划图,位置图,楼栋平面图,效果图");
                seoModel.Description = string.Format("快有家房产网提供楼盘相册大全，新楼盘照片,楼盘实景图,户型图,规划图,位置图,楼栋平面图,效果图.");
                return seoModel;
            }
        }

        #endregion

        #region 交通配套

        /// <summary>
        /// 交通配套
        /// </summary>
        /// <param name="seoModel"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public SEOModel SeoTrafficFacilities(SEOModel seoModel, string cityName)
        {
            //Title：交通配套_ <城市>+新房_新开楼盘_快有家房产网
            //Keywords：交通配套,交通配套设施, 周边交通,周边配套, 配套设施
            //Description：北京快有家房产网提供新房新楼盘,周边交通、周围配套设置齐全.

            string value = string.Empty;
            StringBuilder sb = new StringBuilder();
            List<string> List = new List<string>();
            if (cityName == null)
            {
                cityName = "北京";
            }
            try
            {
                seoModel.Title = string.Format("交通配套_{0}新房_新开楼盘_快有家房产网", cityName);
                seoModel.Keywords = string.Format("交通配套,交通配套设施, 周边交通,周边配套, 配套设施");
                seoModel.Description = string.Format("北京快有家房产网提供新房新楼盘,周边交通、周围配套设置齐全.");
                return seoModel;
            }
            catch
            {
                seoModel.Title = string.Format("交通配套_新房_新开楼盘_快有家房产网");
                seoModel.Keywords = string.Format("交通配套,交通配套设施, 周边交通,周边配套, 配套设施");
                seoModel.Description = string.Format("北京快有家房产网提供新房新楼盘,周边交通、周围配套设置齐全.");
                return seoModel;
            }
        }

        #endregion

        #region 工具

        /// <summary>
        /// 楼盘PK
        /// </summary>
        /// <param name="seoModel"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public SEOModel SeoPremisesPK(SEOModel seoModel, string cityName)
        {
            //Title：新房楼盘PK_ <城市>+新房_新楼盘_快有家房产网
            //Keywords：楼盘PK、快有家楼盘PK、<城市>+楼盘PK
            //Description：快有家房产网提供楼盘PK，快有家楼盘PK.

            string value = string.Empty;
            StringBuilder sb = new StringBuilder();
            List<string> List = new List<string>();
            if (cityName == null)
            {
                cityName = "北京";
            }
            try
            {
                seoModel.Title = string.Format("新房楼盘PK_{0}新房_新楼盘_快有家房产网", cityName);
                seoModel.Keywords = string.Format("楼盘PK、快有家楼盘PK、{0}楼盘PK", cityName);
                seoModel.Description = string.Format("快有家房产网提供楼盘PK，快有家楼盘PK.");
                return seoModel;
            }
            catch
            {
                seoModel.Title = string.Format("新房楼盘PK_新房_新楼盘_快有家房产网");
                seoModel.Keywords = string.Format("楼盘PK、快有家楼盘PK、楼盘PK");
                seoModel.Description = string.Format("快有家房产网提供楼盘PK，快有家楼盘PK.");
                return seoModel;
            }
        }

        /// <summary>
        /// 贷款计算工具
        /// </summary>
        /// <param name="seoModel"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public SEOModel SeoLoanComputational(SEOModel seoModel, string cityName,string typeName)
        {
            //Title：贷款计算工具_ <城市>+新房_新楼盘_快有家房产网
            //Keywords：房贷计算器、贷款计算器在线计算、房贷计算器最新2014、贷款计算器、购房能力评估计算器、公积金贷款计算器、提前还款计算器、税费计算器
            //Description：快有家房产网提供, 房贷计算器、贷款计算器在线计算、房贷计算器最新2014
            //注:选择计时器类型时，标题自动切换（参考产品原型）,
            //例: 贷款计算器_快有家房产网
            //购房能力评估计算器_快有家房产网

            string value = string.Empty;
            StringBuilder sb = new StringBuilder();
            List<string> List = new List<string>();
            if (cityName == null)
            {
                cityName = "北京";
            }
            try
            {
                seoModel.Title = string.Format("{1}_{0}新房_新楼盘_快有家房产网", cityName, typeName);
                seoModel.Keywords = string.Format("房贷计算器、贷款计算器在线计算、房贷计算器最新2014、贷款计算器、购房能力评估计算器、公积金贷款计算器、提前还款计算器、税费计算器");
                seoModel.Description = string.Format("快有家房产网提供, 房贷计算器、贷款计算器在线计算、房贷计算器最新2014");
                return seoModel;
            }
            catch
            {
                seoModel.Title = string.Format("贷款计算工具_新房_新楼盘_快有家房产网");
                seoModel.Keywords = string.Format("房贷计算器、贷款计算器在线计算、房贷计算器最新2014、贷款计算器、购房能力评估计算器、公积金贷款计算器、提前还款计算器、税费计算器");
                seoModel.Description = string.Format("快有家房产网提供, 房贷计算器、贷款计算器在线计算、房贷计算器最新2014");
                return seoModel;
            }
        }

        /// <summary>
        /// 购房手册
        /// </summary>
        /// <param name="seoModel"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public SEOModel SeoManuals(SEOModel seoModel, string cityName)
        {
            //Title：购房手册_ <城市>+新房_新楼盘_快有家房产网
            //Keywords：购房手册、最新购房手册、购房须知、买房手册、购房知识、购房流程、<城市>+购房手册
            //Description：快有家房产网提供，最新购房手册、购房须知、买房手册.
            string value = string.Empty;
            StringBuilder sb = new StringBuilder();
            List<string> List = new List<string>();
            if (cityName == null)
            {
                cityName = "北京";
            }
            try
            {
                seoModel.Title = string.Format("购房手册_{0}新房_新楼盘_快有家房产网", cityName);
                seoModel.Keywords = string.Format("购房手册、最新购房手册、购房须知、买房手册、购房知识、购房流程、{0}购房手册", cityName);
                seoModel.Description = string.Format("快有家房产网提供，最新购房手册、购房须知、买房手册.");
                return seoModel;
            }
            catch
            {
                seoModel.Title = string.Format("购房手册_新房_新楼盘_快有家房产网");
                seoModel.Keywords = string.Format("购房手册、最新购房手册、购房须知、买房手册、购房知识、购房流程、购房手册");
                seoModel.Description = string.Format("快有家房产网提供，最新购房手册、购房须知、买房手册.");
                return seoModel;
            }
        }

        /// <summary>
        /// 购房资格自查系统
        /// </summary>
        /// <param name="seoModel"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public SEOModel SeoEntitledToOwn(SEOModel seoModel, string cityName)
        {
            //Title：购房资格自查系统_ <城市>+新房_新楼盘_快有家房产网
            //Keywords：购房资格核验查询、购房资格自查系统、购房所需材料、<城市>+ 购房资格、<城市>购房资格查询
            // Description：快有家房产网提供,购房资格核验查询,购房资格自查系统.

            string value = string.Empty;
            StringBuilder sb = new StringBuilder();
            List<string> List = new List<string>();
            if (cityName == null)
            {
                cityName = "北京";
            }
            try
            {
                seoModel.Title = string.Format("购房资格自查系统_{0}新房_新楼盘_快有家房产网", cityName);
                seoModel.Keywords = string.Format("购房资格核验查询、购房资格自查系统、购房所需材料、{0}购房资格、{0}购房资格查询", cityName);
                seoModel.Description = string.Format("快有家房产网提供,购房资格核验查询,购房资格自查系统.");
                return seoModel;
            }
            catch
            {
                seoModel.Title = string.Format("购房资格自查系统_新房_新楼盘_快有家房产网");
                seoModel.Keywords = string.Format("购房资格核验查询、购房资格自查系统、购房所需材料、购房资格、{0}购房资格查询");
                seoModel.Description = string.Format("快有家房产网提供,购房资格核验查询,购房资格自查系统.");
                return seoModel;
            }
        }

        #endregion

    }

    public class SEOModel
    {
        public string Title { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
    }

    public static class SEOConditon
    {
        private static Dictionary<string, string> dic;
        /// <summary>
        /// 获取物业类型
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string GetPropertyType(string key)
        {
            dic = new Dictionary<string, string>();
            dic.Add("1", "住宅");
            dic.Add("2", "写字楼");
            dic.Add("3", "别墅");
            dic.Add("4", "商铺");
            return dic[key];
        }
        /// <summary>
        /// 户型
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetRoomType(string key)
        {
            dic = new Dictionary<string, string>();
            dic.Add("1", "1居");
            dic.Add("2", "2居");
            dic.Add("3", "3居");
            dic.Add("4", "4居");
            dic.Add("5", "5居");
            dic.Add("0,5", "五居以下");
            dic.Add("5,8", "5-8居");
            dic.Add("5,", "5居以上");
            return dic[key];
        }
        /// <summary>
        /// 获取配套设施
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetPPST(string key)
        {
            string[] str = { "拎包入住", "家电齐全", "可上网", "可做饭", "可洗澡", "空调房", "可看电视", "有暖气", "有车位", "有车库", "有露台", "有阁楼", "游泳池", "阳光房", "有电梯", "地下室", "可上网" };
            dic = new Dictionary<string, string>();
            for (int i = 0; i < str.Length; i++)
            {
                dic.Add((i + 1).ToString(), str[i]);
            }
            return dic[key];
        }
        /// <summary>
        /// 获取装修程度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetZXCD(string key)
        {
            dic = new Dictionary<string, string>();
            dic.Add("1", "毛坯");
            dic.Add("2", "简装修");
            dic.Add("3", "中等修");
            dic.Add("4", "精装修");
            dic.Add("5", "豪华装修");
            return dic[key];
        }
        /// <summary>
        /// 获取租房方式
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetRentType(string key)
        {
            dic = new Dictionary<string, string>();
            dic.Add("1", "整租");
            dic.Add("2", "合租");
            return dic[key];
        }
        /// <summary>
        /// 获取住宅类别
        /// </summary>
        /// <param name="type">物业类型</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetZZLB(string type, string key)
        {
            dic = new Dictionary<string, string>();
            if (type == "villa")
            {
                dic.Add("1", "独栋");
                dic.Add("2", "双拼");
                dic.Add("3", "联排");
                dic.Add("4", "叠加");
            }
            else if (type == "shop")
            {
                dic.Add("1", "住宅底商");
                dic.Add("2", "商业街商铺");
                dic.Add("3", "临街门面");
                dic.Add("4", "写字楼配套底商");
                dic.Add("5", "购物中心/百货");
                dic.Add("6", "其他");
            }
            else if (type == "office")
            {
                dic.Add("1", "纯写字楼");
                dic.Add("2", "商住楼");
                dic.Add("3", "商业综合楼");
                dic.Add("4", "酒店写字楼");
            }
            else if (type == "all")
            {
                dic.Add("1", "纯写字楼");
                dic.Add("2", "商住楼");
                dic.Add("3", "商业综合楼");
                dic.Add("4", "酒店写字楼");
            }
            else if (type == "flats")
            {
                dic.Add("1", "纯写字楼");
                dic.Add("2", "商住楼");
                dic.Add("3", "商业综合楼");
                dic.Add("4", "酒店写字楼");
            }
            else
            {
                return string.Empty;
            }
            return dic[key];
        }
        /// <summary>
        /// 获取建筑结构
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetJZJG(string key)
        {
            dic = new Dictionary<string, string>();
            dic.Add("1", "平层");
            dic.Add("2", "复式");
            dic.Add("3", "跃层");
            dic.Add("4", "开间");
            dic.Add("5", "错层");
            return dic[key];
        }
        /// <summary>
        /// 获取房龄
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetFangLing(string key)
        {
            dic = new Dictionary<string, string>();
            dic.Add("g1", "2年以下");
            dic.Add("g2", "2-5年");
            dic.Add("g3", "5-10年");
            dic.Add("g4", "10年以上");
            return dic[key];
        }
        /// <summary>
        /// 返回区域、和地铁相关数据
        /// </summary>
        /// <param name="searchtype"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetTrafficData(string citypy, string quyu, string shangquan, string searchtype)
        {
            try
            {
                string returnValue = string.Empty;
                if (searchtype == "quyu")
                {
                    if (!string.IsNullOrEmpty(quyu))
                    {
                        string xmlUrl = GetConfig.SearchUrl + "/Area/GetAreaIndex.ashx?CityPY=" + citypy + "&DPY=" + quyu + "&type=xml";
                        DataSet ds = new DataSet();
                        ds.ReadXml(xmlUrl);
                        DataTable dt = ds.Tables[1];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(shangquan))
                            {
                                if (dt.Rows[i]["BNamePY"].ToString() == shangquan)
                                {
                                    returnValue = dt.Rows[i]["DName"] + dt.Rows[i]["BName"].ToString();
                                    break;
                                }
                            }
                            else
                            {
                                if (dt.Rows[i]["DNamePY"].ToString() == quyu)
                                {
                                    returnValue = dt.Rows[i]["DName"].ToString();
                                    break;
                                }
                            }
                        }
                    }
                }
                if (searchtype == "sub")
                {
                    if (!string.IsNullOrEmpty(quyu))
                    {
                        string xmlUrl = GetConfig.SearchUrl + "/Traffic/GetTrafficIndex.ashx?CityPY=" + citypy + "&PId=0&type=xml";
                        DataSet ds = new DataSet();
                        ds.ReadXml(xmlUrl);
                        DataTable dt = ds.Tables[1];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["TId"].ToString() == quyu)
                            {
                                returnValue = dt.Rows[i]["TName"].ToString();
                                break;
                            }
                        }
                        if (!string.IsNullOrEmpty(shangquan))
                        {
                            string xmlUrl1 = GetConfig.SearchUrl + "/Traffic/GetTrafficIndex.ashx?CityPY=" + citypy + "&PId=" + quyu + "&type=xml";
                            DataSet ds1 = new DataSet();
                            ds1.ReadXml(xmlUrl1);
                            DataTable dt1 = ds1.Tables[1];
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                if (dt1.Rows[i]["TId"].ToString() == shangquan)
                                {
                                    returnValue += dt1.Rows[i]["TName"].ToString();
                                    break;
                                }
                            }
                        }

                    }
                }
                return returnValue;
            }
            catch
            {
                return "";
            }
        }
    }
}