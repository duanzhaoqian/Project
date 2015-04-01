using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Directory = System.IO.Directory;

namespace LuceneOpt
{
    public static class LuceneBaseData
    {
        private static string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LuceneData");
       
        private static IndexSearcher indexSearcher;

        public static string DirPath
        {
            get
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                return dirPath;
            }
        }

        public static IndexSearcher IndexSearcher
        {
            get
            {
                //if (indexSearcher == null)
                //{
                //    indexSearcher = new IndexSearcher(DirPath, true);
                //}
                return indexSearcher;
            }
        }

    }
}
