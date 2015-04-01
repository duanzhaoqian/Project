using System;
using System.Collections.Generic;
using System.Text;
using TXDal.HouseData;
using TXOrm;

namespace TXBll.HouseData
{
    public partial class PremisesFeatureBll
    {
        private readonly PremisesFeatureDal _premisesFeatureDal = new PremisesFeatureDal();

        /// <summary>
        /// 添加
        /// Author:tyh
        /// Time:2014-4-15
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int Add(string text)
        {
            return _premisesFeatureDal.Add(text);
        }
        /// <summary>
        /// 修改
        /// Author:tyh
        /// Time:2014-4-15
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public int Update(int id, string text)
        {
            return _premisesFeatureDal.Update(id, text);
        }
        /// <summary>
        /// 删除
        /// Author:tyh
        /// Time:2014-4-15
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            return _premisesFeatureDal.Delete(id);
        }

        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/3 14:44:46
        /// 功能描述：获取楼盘特色列表
        /// </summary>
        /// <returns></returns>
        public List<PremisesFeature> GetList()
        {
            return _premisesFeatureDal.GetList();
        }

        /// <summary>
        /// 根据编号集获取信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<PremisesFeature> GetListByIds(string ids)
        {
            return _premisesFeatureDal.GetListByIds(ids);
        }

        /// <summary>
        /// 根据编号集获取信息(json格式)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public string GetListJsonStringByIds(string ids)
        {
            var arrList = ids.Split(',');

            var list = _premisesFeatureDal.GetListByIds(ids);

            if (null == list || 0 == list.Count)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            for (int i = 0; i < arrList.Length; i++)
            {
                var entity = GetEntityByIdFromList(list, arrList[i]);
                if (null == entity)
                {
                    continue;
                }

                builder.Append("{\"id\":\"" + entity.Id + "\"");
                builder.Append(",\"name\":\"" + entity.Name + "\"}");
                if (i < list.Count - 1)
                {
                    builder.Append(",");
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// 根据编号从基础list中找出相应的实体
        /// </summary>
        /// <param name="list"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private PremisesFeature GetEntityByIdFromList(List<PremisesFeature> list, object id)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == Convert.ToInt32(id))
                {
                    return list[i];
                }
            }

            return null;
        }
    }
}