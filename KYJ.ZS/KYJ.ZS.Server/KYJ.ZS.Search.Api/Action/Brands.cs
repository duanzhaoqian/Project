using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Search.Api.Action
{
    using KYJ.ZS.Commons.Index;
    using KYJ.ZS.Search.Api.Common;

    using Lucene.Net.Analysis;
    using Lucene.Net.Search;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/6/23 16:29:35
    /// 描述：
    /// </summary>
    public class Brands : Builder
    {
        private Ini ini;
        public override Ini Ini
        {
            get
            {
                return ini;
            }
            set
            {
                ini = value;
            }
        }

        public override object GetResult()
        {
            #region 固定值字段

            BooleanQuery bq = GetQuery();

            #endregion

            #region 范围值字段



            #endregion

            #region 排序字段
            Sort sort = null;

            sort = GetSort();


            #endregion

            #region 查询对象


            #endregion

            #region 特殊字段查询

            #endregion

            #region 查询及结果

            TopFieldDocs topdocs = null;

            topdocs = Ini.IndexSearcher.Search(bq, null, 50, sort);
            ScoreDoc[] hits = topdocs.scoreDocs;

            var count = topdocs.totalHits;
            int bi = 1;
            int ei = 50;
            List<IndexResult> list = null;
            if (hits.Length > 0 && bi < count)
            {
                list = new List<IndexResult>();
                for (int i = bi; i < ei; i++)
                {
                    if (i >= count)
                        break;
                    IndexResult result = new IndexResult();
                    ScoreDoc doc = hits[i];

                    result.Id = Ini.IndexSearcher.Doc(doc.doc).Get("brand_id");
                    result.Name = Ini.IndexSearcher.Doc(doc.doc).Get("brand_name");

                    if (list.Count(r => r.Id == result.Id) == 0)
                        list.Add(result);
                }
            }
            return list;

            #endregion
        }


        public BooleanQuery GetQuery()
        {
            #region 固定值字段

            BooleanQuery bq = new BooleanQuery();

            bq.Add(new MatchAllDocsQuery(), BooleanClause.Occur.MUST);

            return bq;
            #endregion
        }

        public Sort GetSort()
        {
            Sort sort = null;

            return sort;
        }
    }
}
