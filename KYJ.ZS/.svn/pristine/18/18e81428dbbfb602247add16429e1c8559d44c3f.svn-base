using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Search.Api.Action
{
    using KYJ.ZS.Commons;
    using KYJ.ZS.Commons.Index;
    using KYJ.ZS.Models.Categories;
    using KYJ.ZS.Search.Api.Common;

    using Lucene.Net.Analysis;
    using Lucene.Net.Search;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/6/23 11:42:24
    /// 描述：
    /// </summary>
    public class Categories : Builder
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
            #region 查询条件
            string condition = ini.condition as string;
            string[] conditions = null;
            string pid = "";
            string id = "";
            if (string.IsNullOrEmpty(condition))
            {

            }
            else
            {
                conditions = condition.Split("-".ToCharArray());
                pid = conditions[0];
                id = conditions[1];
            }
            #endregion
            
            var relusts = new Relusts<CatResult>
                              {
                                  FatherCategories = this.Search(pid),
                                  SonCategories = this.Search(id)
                              };

            return relusts;
        }

        private BooleanQuery GetQuery( string id)
        {
            #region 固定值字段

            BooleanQuery bq = new BooleanQuery();
            if (string.IsNullOrEmpty(id))
            {
                bq.Add(new MatchAllDocsQuery(), BooleanClause.Occur.MUST);
            }
            else
            {
                bq.Add(new TermQuery(new Lucene.Net.Index.Term("category_pid", id)), BooleanClause.Occur.MUST);
            }
            return bq;
            #endregion
        }

        private Sort GetSort()
        {
            Sort sort = null;
            string Name = "";
            bool Order = false;
            Name = "category_id";
            Order = false;

            SortField[] sf = { new SortField(Name, SortField.LONG, Order) };
            sort = new Sort(sf);
            return sort;
        }

        private List<CatResult> GetList(IndexSearcher indexSearcher, BooleanQuery bq, Sort sort, out int count)
        {
            TopFieldDocs topdocs = null;

            topdocs = indexSearcher.Search(bq, null, 50, sort);
            ScoreDoc[] hits = topdocs.scoreDocs;

            count = topdocs.totalHits;
            int bi = 1;
            int ei = 50;
            List<CatResult> list = new List<CatResult>();
            if (hits.Length > 0 && bi < count)
            {
                for (int i = bi; i < ei; i++)
                {
                    if (i >= count)
                        break;
                    CatResult result = new CatResult();
                    ScoreDoc doc = hits[i];

                    result.Id = Convert.ToInt32(indexSearcher.Doc(doc.doc).Get("category_id"));
                    result.Name = indexSearcher.Doc(doc.doc).Get("category_name");
                    result.Pid = Convert.ToInt32(indexSearcher.Doc(doc.doc).Get("category_pid"));

                    result.IsShow = true;
                    if (list.Count(r => r.Id == result.Id) == 0)
                        list.Add(result);
                }
            }
            return list;
        }

        private List<CatResult> Search(string id)
        {
            
            #region 固定值字段

            BooleanQuery bq = GetQuery(id);

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

            #region 结果
            int hitscount = 0;

            return GetList(ini.IndexSearcher, bq, sort, out hitscount);
            #endregion
        }
    }
}
