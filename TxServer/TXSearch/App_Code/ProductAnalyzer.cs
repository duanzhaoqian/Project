using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Lucene.Net.Analysis;
using Analyzer = Lucene.Net.Analysis.Analyzer;
using StandardAnalyzer = Lucene.Net.Analysis.Standard.StandardAnalyzer;
using Document = Lucene.Net.Documents.Document;
using Field = Lucene.Net.Documents.Field;
using IndexWriter = Lucene.Net.Index.IndexWriter;
using ParseException = Lucene.Net.QueryParsers.ParseException;
using QueryParser = Lucene.Net.QueryParsers.QueryParser;
using Hits = Lucene.Net.Search.Hits;
using IndexSearcher = Lucene.Net.Search.IndexSearcher;
using Query = Lucene.Net.Search.Query;
using Directory = Lucene.Net.Store.Directory;
using RAMDirectory = Lucene.Net.Store.RAMDirectory;
using FSDirectory = Lucene.Net.Store.FSDirectory;
using System.Globalization;
using System.Collections;
using System.IO;
/// <summary>
    /// Title: ProductAnalyzer
    /// Description:
    ///   Subclass of org.apache.lucene.analysis.Analyzer
    ///   build from a ProductTokenizer, filtered with ProductFilter.
    /// Copyright:   Copyright (c) 2006.07.19
    /// @author try
    /// </summary>
    public class ProductAnalyzer : Analyzer 
    {
        public ProductAnalyzer() 
        {
        }
        /// <summary>
        /// Creates a TokenStream which tokenizes all the text in the provided Reader.
        /// </summary>
        /// <returns>A TokenStream build from a ProductTokenizer filtered with ProductFilter.</returns>
        public override sealed TokenStream TokenStream(String fieldName, TextReader reader) 
        {
            TokenStream result = null;
            result = new ProductTokenizer(reader);
            return  new ProductFilter(result);
        }
    }


public sealed class ProductTokenizer : Tokenizer 
    {
        public ProductTokenizer(TextReader _in) 
        {
            input = _in;
        }
        private int offset = 0, bufferIndex = 0, dataLen = 0;
        private static int MAX_WORD_LEN = 255;
        private static int IO_BUFFER_SIZE = 1024;
        private char[] buffer = new char[MAX_WORD_LEN];
        private char[] ioBuffer = new char[IO_BUFFER_SIZE];
        private int length;
        private int start;
        private void Push(char c) 
        {
            if (length == 0) start = offset-1;   // start of token
            buffer[length++] = Char.ToLower(c);  // buffer it
        }
        private Token Flush() 
        {
            if (length > 0) 
            {
                return new Token(new String(buffer, 0, length), start, start+length);
            }
            else
                return null;
        }
        public override Token Next()
        {
            length = 0;
            start = offset;
            while (true) 
            {
                char c;
                offset++;
                if (bufferIndex >= dataLen) 
                {
                    dataLen = input.Read(ioBuffer, 0, ioBuffer.Length);
                    bufferIndex = 0;
                };
                if (dataLen == 0) 
                    return Flush();
                else
                {
                    c = ioBuffer[bufferIndex++];
                }
                switch(Char.GetUnicodeCategory(c)) 
                {                     
                    case UnicodeCategory.DecimalDigitNumber:
                        Push(c);
                        return Flush();
                    case UnicodeCategory.LowercaseLetter:
                    case UnicodeCategory.UppercaseLetter:
                        Push(c);
                        return Flush();
                    case UnicodeCategory.OtherLetter:
                        if (length > 0)
                        {
                            bufferIndex--;
                            offset--;
                            return Flush();
                        }
                       Push(c);
                       return Flush();
                   default:
                       if (length > 0) return Flush();
                       break;
                }
            }
        }
    }


public sealed class ProductFilter : TokenFilter 
    {
        // Only English now, Chinese to be added later.
        public static String[] STOP_WORDS = 
                 {
                     "and", "are", "as", "at", "be", "but", "by",
                     "for", "if", "in", "into", "is", "it",
                     "no", "not", "of", "on", "or", "such",
                     "that", "the", "their", "then", "there", "these",
                     "they", "this", "to", "was", "will", "with"
                 };
        private Hashtable stopTable;
        public ProductFilter(TokenStream _in)
            : base(_in)
        {
            stopTable = new Hashtable(STOP_WORDS.Length);

            for (int i = 0; i < STOP_WORDS.Length; i++)
                stopTable[STOP_WORDS[i]] = STOP_WORDS[i];
        }
        public override Token Next()
        {
            for (Token token = input.Next(); token != null; token = input.Next()) 
            {
                String text = token.TermText();
                if (stopTable[text] == null) 
                {
                    switch (Char.GetUnicodeCategory(text[0])) 
                    {
                        case UnicodeCategory.LowercaseLetter:
                        case UnicodeCategory.UppercaseLetter:
                             return token;
                        case UnicodeCategory.OtherLetter:
                            return token;
                        case UnicodeCategory.DecimalDigitNumber:
                            return token;
                    }
                }
            }
            return null;
        }
    }
