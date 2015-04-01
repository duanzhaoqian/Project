using System;
using System.Collections.Generic;
using ServiceStack;
using TXNewHouseServices.dal;
using TXNewHouseServices.models;

namespace TXNewHouseServices
{
    public class CharacteristicBody
    {
        private const string KeyprefixCharacteristic = "Characteristic_";

        private string _log_filenamelast_characteristic = "characteristic";

        /// <summary>
        /// 运行主体
        /// </summary>
        public void RunBody()
        {
            // 从数据库中获取特色列表
            var list = GetCharacteristicList();

            // 更新Redis中的楼盘特色
            UpdateCharacteristicForRedis(list);
        }

        // 从数据库中获取特色列表
        private IList<PremisesFeatureEntity> GetCharacteristicList()
        {
            var list = new PremisesFeatureDal().GetPremisesList();
            ServiceCom.Instance.WriteLog(_log_filenamelast_characteristic, "\t数据库中读取楼盘特色完毕");
            return list;
        }

        // 更新Redis中的楼盘特色
        private void UpdateCharacteristicForRedis(IList<PremisesFeatureEntity> list)
        {
            if (null == list || 0 == list.Count)
            {
                ServiceCom.Instance.WriteLog(_log_filenamelast_characteristic, "\t数据库中没有读取到楼盘特色数据");
                return;
            }

            var delList = new List<string>();
            var updateErrList = new List<string>();
            foreach (var t in list)
            {
                if (t.IsDel)
                {
                    Redis.RemoveAllFromList(KeyprefixCharacteristic + Convert.ToString(t.Id), FunctionTypeEnum.NewHousePropertyFeatures, 0);
                    delList.Add(string.Format("项目特色编号：{0} 名称：{1} 被删除\r\n", t.Id, t.Name));
                }
                else
                {
                    var vc = Redis.SetValue(KeyprefixCharacteristic + Convert.ToString(t.Id), t.Name, null, FunctionTypeEnum.NewHousePropertyFeatures, 0);
                    if (!vc)
                    {
                        // 更新出错，写入日志
                        updateErrList.Add(string.Format("\t\t{0:yyyy-MM-dd HH:mm:ss} 在更新楼盘特色编号:{1} 时出现错误。", DateTime.Now, t.Id));
                    }
                }
            }

            if (0 < delList.Count)
            {
                // 写入日志
                ServiceCom.Instance.WriteLog(_log_filenamelast_characteristic, string.Join("\r\n", delList));
            }

            if (0 < updateErrList.Count)
            {
                // 写入日志
                ServiceCom.Instance.WriteLog(_log_filenamelast_characteristic, string.Join("\r\n", updateErrList));
            }

            ServiceCom.Instance.WriteLog(_log_filenamelast_characteristic, "\tRedis中更新楼盘特色完毕");
        }
    }
}