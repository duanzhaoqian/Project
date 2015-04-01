
namespace TXCommons.NewHouseWeb
{
    public class AlbumType
    {
        /// <summary>
        /// 取相册名称
        /// </summary>
        /// <param name="type">//xgt效果图|ght规划图|wzt位置图|ldt楼栋平面图|sjt实景图|jtt交通图</param>
        /// <returns></returns>
        public static string GetAlbumType(string type) {
            string typeStr = "";
            switch(type){
                case "xgt":
                    typeStr = "效果图";
                    break;
                case "ght":
                    typeStr = "规划图";
                    break;
                case "wzt":
                    typeStr = "位置图";
                    break;
                case "ldt":
                    typeStr = "楼栋平面图";
                    break;
                case "sjt":
                    typeStr = "实景图";
                    break;
                case "jtt":
                    typeStr = "交通图";
                    break;
            }
            return typeStr;
        }

        /// <summary>
        /// 取相册对应参数
        /// </summary>
        /// <param name="type">//1.户型图2.规划图3.位置图4.楼栋平面图5.效果图6.实景图7.交通图</param>
        /// <returns></returns>
        public static int GetAlbumIntType(string type)
        {
            int typeStr = 0;
            switch (type)
            {
                case "xgt":
                    typeStr = 5;
                    break;
                case "ght":
                    typeStr = 2;
                    break;
                case "wzt":
                    typeStr = 3;
                    break;
                case "ldt":
                    typeStr = 4;
                    break;
                case "sjt":
                    typeStr = 6;
                    break;
                case "jtt":
                    typeStr = 7;
                    break;
            }
            return typeStr;
        }

    }
}
