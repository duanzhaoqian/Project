using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using TXCommons;
using org.in2bits.MyXls;

namespace TXManagerWeb.Controllers
{
    public class ExcelResult<T> : ActionResult where T : OutputExcel
    {
        public ExcelResult(IList<T> entity, string fileName, string wSheet)
        {
            Entity = entity;

            DateTime time = DateTime.Now;
            FileName = string.Format("{0}_{1}_{2}_{3}_{4}",
                fileName, time.Month, time.Day, time.Hour, time.Minute);
            FileName = FileName.IsHasChinese() ? System.Web.HttpUtility.UrlEncode(FileName) : FileName;
            WSheet = wSheet;
        }

        public ExcelResult(IList<T> entity, string fileName, string[] wSheetLs)
        {
            Entity = entity;

            DateTime time = DateTime.Now;
            FileName = string.Format("{0}_{1}_{2}_{3}_{4}",
                fileName, time.Month, time.Day, time.Hour, time.Minute);
            FileName = FileName.IsHasChinese() ? System.Web.HttpUtility.UrlEncode(FileName) : FileName;
            WSheetLs = wSheetLs;
        }

        public ExcelResult(IList<T> entity)
        {
            Entity = entity;

            DateTime time = DateTime.Now;
            FileName = string.Format("{0}_{1}_{2}_{3}_{4}",
                FileName, time.Month, time.Day, time.Hour, time.Minute);
            FileName = FileName.IsHasChinese() ? System.Web.HttpUtility.UrlEncode(FileName) : FileName;
        }

        public IList<T> Entity { get; set; }

        public string FileName { get; set; }

        /// <summary>
        /// 需要生成EXCEL模板 WorkSheet1:约看中  WorkSheet2：
        /// </summary>
        public string WSheet { get; set; }

        public string[] WSheetLs { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (Entity == null)
            {
                new EmptyResult().ExecuteResult(context);
                return;
            }

            SetResponse(context);

        }

        /// <summary>
        /// 设置并向客户端发送请求响应。
        /// </summary>
        /// <param name="context"></param>
        private void SetResponse(ControllerContext context)
        {
            var xls = new XlsDocument(); //创建空xls文档
            FileName = FileName.IsHasChinese() ? System.Web.HttpUtility.UrlEncode(FileName) : FileName;
            xls.FileName = FileName;

            Worksheet sheet = xls.Workbook.Worksheets.AddNamed(FileName);

            SetCellWidth(xls, sheet); //自适应宽度

            //设置文档列属性结束
            ConvertEntity(sheet);
            xls.Send();
        }

        /// <summary>
        /// 把泛型集合转换成组合Excel表格的字符串。
        /// </summary>
        /// <returns></returns>
        private void ConvertEntity(Worksheet sheet)
        {

            Cells cells = sheet.Cells; //获得指定工作页列集合
            if (!string.IsNullOrEmpty(WSheet))
            {
                AddCells(cells);
            }
            else if (WSheetLs != null)
            {
                AddCells_ByLs(cells);
            }

            AddTableBody(cells);

        }

        public void AddCells_ByLs(Cells cell)
        {
            int temp = 0;
            foreach (string s in WSheetLs)
            {
                temp++;
                cell.Add(1, temp, s);
            }
        }

        public void AddCells(Cells cell)
        {
            switch (WSheet)
            {
                case "WorkSheet1":
                    cell.Add(1, 1, "房源编号");
                    cell.Add(1, 2, "小区名");
                    cell.Add(1, 3, "所在城市");
                    cell.Add(1, 4, "详细地址");
                    cell.Add(1, 5, "支付方式");
                    cell.Add(1, 6, "房东报价");
                    cell.Add(1, 7, "出价时间");
                    cell.Add(1, 8, "真实姓名");
                    cell.Add(1, 9, "手机号码");
                    cell.Add(1, 10, "出价次数");
                    cell.Add(1, 11, "出价金额");
                    cell.Add(1, 12, "");
                    cell.Add(1, 13, "");
                    cell.Add(1, 14, "");
                    break;
                case "WorkSheet2":
                    cell.Add(1, 1, "");
                    cell.Add(1, 2, "");
                    cell.Add(1, 3, "");
                    cell.Add(1, 4, "");
                    cell.Add(1, 5, "");
                    cell.Add(1, 6, "");
                    cell.Add(1, 7, "");
                    cell.Add(1, 8, "");
                    cell.Add(1, 9, "");
                    cell.Add(1, 10, "");
                    cell.Add(1, 11, "");
                    cell.Add(1, 12, "");
                    cell.Add(1, 13, "");
                    cell.Add(1, 14, "");
                    break;
            }
        }

        #region 遍历数据

        /// <summary>
        /// 根据IList泛型集合中的每项的属性值来组合Excel表格。
        /// author:huhangfei
        /// </summary>
        /// <param name="cells"></param>
        private void AddTableBody(Cells cells)
        {
            if (Entity == null || Entity.Count <= 0)
            {
                return;
            }
            int tmepCell = 1;
            foreach (T s in Entity)
            {
                tmepCell++;
                int tempColu = 0;
                foreach (PropertyInfo pi in typeof (T).GetProperties())
                {
                    if (s != null)
                    {
                        tempColu++;
                        object value = pi.GetValue(s, null);
                        cells.Add(tmepCell, tempColu, value);
                    }
                }
            }
        }

        #endregion

        #region 设置列宽自适应

        /// <summary>
        /// 设置列宽自适应
        /// author:huhangfei
        /// </summary>
        /// <param name="xls"></param>
        /// <param name="sheet"></param>
        public void SetCellWidth(XlsDocument xls, Worksheet sheet)
        {
            if (Entity == null || Entity.Count <= 0)
            {
                return;
            }
            int tmepCell = 0; //列
            foreach (PropertyInfo pi in typeof (T).GetProperties())
            {
                if (tmepCell + 1 > WSheetLs.Length)
                {
                    break;
                }
                var initLenth = (ushort) System.Text.Encoding.Default.GetByteCount(WSheetLs[tmepCell]);
                foreach (T s in Entity)
                {
                    if (pi.GetValue(s, null) != null)
                    {
                        var l = (ushort) System.Text.Encoding.Default.GetByteCount(pi.GetValue(s, null).ToString());
                        initLenth = initLenth > l ? initLenth : l;
                    }
                }
                //设置文档列属性 
                var cinfo = new ColumnInfo(xls, sheet); //设置xls文档的指定工作页的列属性
                cinfo.Collapsed = true;
                //设置列的范围 如 0列-10列
                cinfo.ColumnIndexStart = (ushort) (tmepCell); //列开始
                cinfo.ColumnIndexEnd = (ushort) (tmepCell); //列结束
                cinfo.Collapsed = true;
                //cinfo.Width = 85 * 65;
                cinfo.Width = (ushort) ((initLenth*1.2)*256); //列的宽度计量单位为 1/256 字符宽  
                sheet.AddColumnInfo(cinfo);
                tmepCell++;

            }
        }

        #endregion

    }
}