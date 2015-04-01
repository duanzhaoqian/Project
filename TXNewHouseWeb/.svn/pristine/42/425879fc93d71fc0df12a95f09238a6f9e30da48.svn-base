using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Analysis;
using System.IO;

namespace Commons
{
    public class EsayAnalyzer : Analyzer
    {
        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            return new EsayTokenizer(reader);
        }
    }

    public class EsayTokenizer : CharTokenizer
    {
        private TextReader reader;

        public EsayTokenizer(TextReader reader)
            : base(reader)
        {
            this.reader = reader;
        }

        protected override bool IsTokenChar(char c)
        {
            return c != ',';
        }
    }
}
