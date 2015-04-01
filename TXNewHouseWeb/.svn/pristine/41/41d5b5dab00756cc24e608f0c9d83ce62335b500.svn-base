using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;
using TXNewHouseServices.dal;
using TXNewHouseServices.models;

namespace TXNewHouseServices
{
    public class RankBody
    {
        private const string KeyprefixRank = "NewHouseRank_";

        private string _log_filenamelast_rank = "rank";

        /// <summary>
        /// 运行主体
        /// </summary>
        public void RunBody()
        {
            // 获取楼盘列表
            var list = GetPremisesList();

            // 从Redis排名列表中删除已被逻辑删除的楼盘排名信息
            DeleteRankInfoFromPremisesIsDel(list);

            // 从Redis中获取对应楼盘的浏览量并填充到传入的楼盘列表中
            FillListViewCountByPremiseList(list);

            // 排序
            var ranklist = RankListForPremisesList(list);

            // 将排名信息保存到数据库中
            if (!SaveRankListToDb(ranklist))
            {
                return;
            }

            // 更新Radis中楼盘的历史排名信息
            UpdateRankListForRadis(ranklist);
        }

        /// <summary>
        /// 获取楼盘列表
        /// </summary>
        /// <returns></returns>
        private IList<PremisesViewCountEntity> GetPremisesList()
        {
            // 获取楼盘列表
            var list = new PremisesViewCountDal().GetPremisesList();

            ServiceCom.Instance.WriteLog(_log_filenamelast_rank, "\t楼盘数据获取完成，共获取" + (list == null ? 0 : list.Count) + "条数据");

            return list;
        }

        /// <summary>
        /// 从Redis排名列表中删除已被逻辑删除的楼盘排名信息
        /// </summary>
        /// <param name="list"></param>
        private void DeleteRankInfoFromPremisesIsDel(IList<PremisesViewCountEntity> list)
        {
            var idlist = new List<string>();
            // 从Redis中删除“已删除的楼盘排名”
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].IsDel)
                {
                    continue;
                }

                Redis.RemoveAllFromList(KeyprefixRank + Convert.ToString(list[i].PremisesId), FunctionTypeEnum.NewHousePropertyRank, list[i].CityId);
                idlist.Add(Convert.ToString(list[i].PremisesId));
            }

            ServiceCom.Instance.WriteLog(_log_filenamelast_rank, "\tRedis中删除无效楼盘完毕。" + (0 < idlist.Count ? "删除楼盘编号：" + string.Join(",", idlist) : ""));
        }

        /// <summary>
        /// 从Redis中获取对应楼盘的浏览量并填充到传入的楼盘列表中
        /// </summary>
        /// <param name="list"></param>
        private void FillListViewCountByPremiseList(IList<PremisesViewCountEntity> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].IsDel)
                {
                    list[i].ViewCount = -1; // 当该楼盘删除时设置浏览次数为 -1
                    continue;
                }
                var vc = Redis.GetOneViewCountValue(string.Format("NewHouseView_{0}", list[i].PremisesId), FunctionTypeEnum.NewHouseViewCount, list[i].CityId);
                list[i].ViewCount = Convert.ToInt32(vc);
            }
        }

        /// <summary>
        /// 排序楼盘信息列表并返回新列表对象
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private IList<PremisesViewCountEntity> RankListForPremisesList(IEnumerable<PremisesViewCountEntity> list)
        {
            // 浏览量排序
            var ranklist = (from a in list where !a.IsDel orderby a.ViewCount descending select a).ToList();
            for (int i = 0; i < ranklist.Count(); i++)
            {
                ranklist[i].RankNum = (i + 1);
            }

            ServiceCom.Instance.WriteLog(_log_filenamelast_rank, "\t楼盘浏览量排序完毕。");
            return ranklist;
        }

        /// <summary>
        /// 将排名信息保存到数据库中
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private bool SaveRankListToDb(IList<PremisesViewCountEntity> list)
        {
            // 写入数据库排名表
            var result = new PremisesViewCountDal().AddRankResult(list);

            if (0 == result)
            {
                // 添加出错
                return false;
            }

            ServiceCom.Instance.WriteLog(_log_filenamelast_rank, "\t楼盘排名写入数据库完毕。");
            return true;
        }

        /// <summary>
        /// 更新Radis中楼盘的历史排名信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private void UpdateRankListForRadis(IList<PremisesViewCountEntity> list)
        {
            var err = new List<string>();

            // 更新Redis中的浏览量历史排名
            for (int i = 0; i < list.Count; i++)
            {
                var vc = Redis.SetValue<int>(KeyprefixRank + Convert.ToString(list[i].PremisesId), list[i].RankNum, null, FunctionTypeEnum.NewHousePropertyRank, list[i].CityId);
                if (!vc)
                {
                    // 更新出错，写入日志
                    err.Add(string.Format("\t\t{0:yyyy-MM-dd HH:mm:ss} 在更新楼盘编号:{1}(CityId:{2}) 时出现错误。", DateTime.Now, list[i].PremisesId, list[i].CityId));
                }
            }

            if (0 < err.Count)
            {
                // 写入日志
                ServiceCom.Instance.WriteLog(_log_filenamelast_rank, string.Join("\r\n", err));
            }

            ServiceCom.Instance.WriteLog(_log_filenamelast_rank, "\tRedis中更新楼盘排名完毕");
        }
    }
}