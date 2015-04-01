using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Search.Api.Common
{
    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/6/23 9:18:57
    /// 描述：
    /// </summary>
    public abstract class Builder
    {
        public abstract  Ini Ini { get; set; }
        public abstract object GetResult();
        public virtual void Close()
        {
            if (Ini.Directory != null)
                Ini.Directory.Close();
            if (Ini.IndexWriter == null && Ini.IndexSearcher != null)
                Ini.IndexSearcher.Close();
            if (Ini.IndexSearcher == null && Ini.IndexWriter != null)
                Ini.IndexWriter.Close();
            if (Ini.Analyzer != null)
                Ini.Analyzer.Close();
        }
    }
}
