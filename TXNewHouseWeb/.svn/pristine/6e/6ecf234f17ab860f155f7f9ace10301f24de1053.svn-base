using System.Text;
using System.Collections;
using System.Data;
using System.Web;
using System.Xml;
using System.Reflection;
namespace TXCommons
{
    /// <summary>
    /// 导出Excel(汪敏)
    /// </summary>
    public class ExportExcel
    {
        private StringBuilder s = new StringBuilder();
        public void GenerateByHtmlString(string Typename, string TempHtml)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "utf-8";
            string Filename = Typename + ".xls";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "online;filename=" + Filename);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(TempHtml);
            HttpContext.Current.Response.End();
        }
        public void CreateExcelWithMode(int TableRows, int TableColumns, string FileName)
        {
            string TableString = "";
            TableString += TableStart(TableRows, TableColumns);
            TableString += s.ToString();
            TableString += TableEnd();
            string ModePath = HttpContext.Current.Server.MapPath("~/xml/ExcelMode.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ModePath);
            string ExcelXmlStr = xmlDoc.InnerXml;
            ExcelXmlStr = ExcelXmlStr.Insert(ExcelXmlStr.IndexOf("</Worksheet>"), TableString);
            GenerateByHtmlString(FileName, ExcelXmlStr);
        }
        public DataTable ToDataTable(IList list)//集合转换DataTable
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        #region Draw Excel Table Methods
        private string TableStart(int rows, int columns)
        {
            string TableString = "";
            TableString += "<Table ss:ExpandedRowCount=\"" + rows + "\" ss:ExpandedColumnCount=\"" + columns + "\" x:FullColumns=\"1\"\n";
            TableString += "x:FullRows=\"1\" ss:DefaultColumnWidth=\"100\" ss:DefaultRowHeight=\"14.25\">\n";
            return TableString;
        }
        private string TableEnd()
        {
            string TableString = "";
            TableString += "</Table>\n";
            return TableString;
        }
        public void RowStart()
        {
            s.Append("<Row ss:AutoFitHeight=\"0\">\n");
        }
        public void RowEnd()
        {
            s.Append("</Row>\n");
        }
        public void CellWithoutFormula(string DataType, string Data)
        {
            s.Append("<Cell><Data ss:Type=\"" + DataType + "\">" + Data + "</Data></Cell>\n");
        }
        public void CellWithFormula(string DataType, string Formula)
        {
            s.Append("<Cell ss:Formula=\"=" + Formula + "\"><Data ss:Type=\"" + DataType + "\"></Data></Cell>\n");
        }
        #endregion
    }
}
