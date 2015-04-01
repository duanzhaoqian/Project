using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using Directory = Lucene.Net.Store.Directory;
using Analyzer = Lucene.Net.Analysis.Analyzer;
using StandardAnalyzer = Lucene.Net.Analysis.Standard.StandardAnalyzer;
using FSDirectory = Lucene.Net.Store.FSDirectory;
using IndexWriter = Lucene.Net.Index.IndexWriter;
using Field = Lucene.Net.Documents.Field;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using System.Threading;

namespace HouseDataInit
{
     
    public  class Program
    {
      
        static void Main(string[] args)
        {
            Console.WriteLine("开始_1");
            HouseInit h = new HouseInit();
            h.Init();
            Console.WriteLine("OK");
            Console.Read();
        }

        
    }
}
