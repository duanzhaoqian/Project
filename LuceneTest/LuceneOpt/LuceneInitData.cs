using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DAL;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using TXOrm;
using Version = Lucene.Net.Util.Version;

namespace LuceneOpt
{
    public class LuceneInitData
    {
        DAL.SHouseDal sHouse = new SHouseDal();

        public int InitData()
        {
            List<S_LongHouseBase> list = sHouse.GetAllData<S_LongHouseBase>();

            IndexWriter indexWriter = new IndexWriter(FSDirectory.Open(new DirectoryInfo(LuceneBaseData.DirPath)), new StandardAnalyzer(Version.LUCENE_29), false, IndexWriter.MaxFieldLength.UNLIMITED);
            PropertyInfo[] propertyInfos = typeof(S_LongHouseBase).GetProperties();
            indexWriter.SetUseCompoundFile(true);
            IndexReader indexReader = IndexReader.Open(FSDirectory.Open(new DirectoryInfo(LuceneBaseData.DirPath)), true);
            int a = indexReader.NumDocs();
            foreach (S_LongHouseBase houseBase in list)
            {
                Document document = new Document();
                foreach (PropertyInfo info in propertyInfos)
                {
                    string s = houseBase.GetType().GetProperty(info.Name).GetValue(houseBase, null) == null ? "" : houseBase.GetType().GetProperty(info.Name).GetValue(houseBase, null).ToString();
                    document.Add(new Field(info.Name, s, Field.Store.YES, Field.Index.NOT_ANALYZED));
                }
                indexWriter.AddDocument(document);
            }
            indexWriter.Optimize();
            indexWriter.Close();
            return list.Count;
        }
    }
}
