using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace TXTimingService
{
    class Profile
    {
           

          #region 读写ini文件 API定义
          [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileStringA")]
          private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
          [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileStringA")]
          private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
          #endregion

          /// <summary>
          /// 写INI文件
          /// </summary>
          /// <param name="Section">配置目录</param>
          /// <param name="Key">参数</param>
          /// <param name="Value">值</param>
          /// <param name="IniPath">路径</param>
          public static bool IniWriteValue(string Section, string Key, string Value)
          {
          try
          {
              System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
              WritePrivateProfileString(Section, Key, Value, a.Location.Substring(0, a.Location.LastIndexOf(@"\") + 1) + "/config.ini");
          return true;
          }
          catch
          {
          return false;
          }

          }


          /// <summary>
          /// 读INI文件
          /// </summary>
          /// <param name="Section">配置目录</param>
          /// <param name="Key">参数</param>
          /// <param name="IniPath">路径</param>
          /// <returns>返回值</returns>
          public static string IniReadValue(string Section, string Key)
          {
          StringBuilder temp = new StringBuilder(255);
          System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
              string path=a.Location.Substring(0,a.Location.LastIndexOf(@"\")+1) + "config.ini";
          int i = GetPrivateProfileString(Section, Key, "", temp, 255, path);
          return temp.ToString();
          }
    }
}
