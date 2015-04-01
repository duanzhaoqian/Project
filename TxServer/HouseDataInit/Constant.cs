using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HouseDataInit
{
  public   class Constant
  {
      

      /// <summary>
      /// Unix时间戳转成时间
      /// </summary>
      /// <param name="timeStamp"></param>
      /// <returns></returns>
      public static DateTime FROM_UNIXTIME(long timeStamp)
      {
          return DateTime.Parse("2010-01-01 00:00:00.135").AddSeconds(timeStamp);
      }

      /// <summary>
      /// 时间转成Unix时间戳
      /// </summary>
      /// <param name="dateTime"></param>
      /// <returns></returns>
      public static long UNIX_TIMESTAMP(DateTime dateTime)
      {
          try
          {
              long i = (dateTime.Ticks - DateTime.Parse("2010-01-01 00:00:00.135").Ticks) / 10000000;//自己改的时间
              if (i == 0 || i == null || i < 1000)
                  i = 1000;
              return i;
          }
          catch{return 1000;}
          
      }

      
     
    }
}
