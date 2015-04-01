using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;

namespace KYJ.ZS.Models.View
{
    /// <summary>
    /// 作者：maq
    /// 时间；2014年5月19日10:20:36
    /// 描述：租期模板更新viewmodel
    /// </summary>
    public class TenancyTemplateUpDateView : TenancyTemplate
    {
        public int[] Months;

        public TenancyTemplateUpDateView()
        {
            string[] arrStr =base.TtempMonths.Split(',');
            Months=new int[arrStr.Length];
            for (int i = 0; i < arrStr.Length; i++)
            {
                Months[i] = Convert.ToInt32(arrStr[i]);
            }
        }

        public int Max
        {
            get { return Months.Max(); }
        }

        public int Min
        {
            get { return Months.Min(); }
        }
    }
}
