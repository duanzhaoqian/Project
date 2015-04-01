using System.Text;
using System.IO;

namespace TXCommons.PictureModular
{
    public class Tool
    {
        #region 将文本写成任意扩展名的文件
        public static void WriteTextToFile(string Text, string FileFullPathAndName)
        {
            WriteTextToFile(Text, FileFullPathAndName, false, "gb2312");
        }
        public static void WriteTextToFile(string Text, string FileFullPathAndName, bool IsAppend)
        {
            WriteTextToFile(Text, FileFullPathAndName, IsAppend, "gb2312");
        }
        public static void WriteTextToFile(string Text, string FileFullPathAndName, bool IsAppend, string encoding)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return;
            }
            string DirName = GetDirByFileName(FileFullPathAndName);
            if (!Directory.Exists(DirName))
            {
                Directory.CreateDirectory(DirName);
            }
            if (!File.Exists(FileFullPathAndName))
            {
                using (File.Create(FileFullPathAndName)) { };
            }
            Encoding gbEncode = Encoding.GetEncoding(encoding);
            using (StreamWriter swFromFile = new StreamWriter(FileFullPathAndName, IsAppend, gbEncode))
            {
                swFromFile.Write(Text);
                swFromFile.Close();
            }
        }
        #endregion

        #region 根据文件名,取得目录名
        public static string GetDirByFileName(string FileName)
        {
            int position = FileName.LastIndexOf(@"\");
            if (position > 0)
            {
                return FileName.Substring(0, position);
            }
            else
            {
                return "";
            }
        }

        #endregion
    }
}
