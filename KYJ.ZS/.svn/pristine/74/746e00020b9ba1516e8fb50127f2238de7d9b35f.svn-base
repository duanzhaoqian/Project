using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KYJ.ZS.Commons.Common
{
    /// <summary>
    /// Author:zhuzh
    /// Date:2014-05-04
    /// Desc:验证码
    /// </summary>
    public class VerificationCode
    {
        #region 手机验证码

        /// <summary>
        /// 获取对某一手机一天内发送验证码的次数
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <returns></returns>
        public static int GetMobileVerificationCodeCount(string mobile)
        {
            var key = mobile.Trim() + "count";

            var counts = RedisTool.GetAllItemsFromList<List<string>>(key, FunctionType.ZuShouVerificationCode, 0);

            if (counts == null || counts.Count == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(counts.Last());
            }
        }

        /// <summary>
        ///修改验证码的次数
        /// </summary>
        /// <param name="mobile"></param>
        public static void ChangeMobileVerificationCodeCount(string mobile)
        {
            var key = mobile.Trim() + "count";

            var count = GetMobileVerificationCodeCount(mobile);

            if (count > 0)
            {
                RedisTool.RemoveAllFromList(key, FunctionType.ZuShouVerificationCode, 0);
            }

            count++;

            RedisTool.AddItemToList<string>(count.ToString(), key, DateTime.Now.Date.AddDays(1), FunctionType.ZuShouVerificationCode, 0); //存储次数        
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static bool SendMobileVerificationCode(string mobile, string msg = "")
        {
            var verificationCode = CreateValidateNumber(4);

            SMSTool smsTool = new SMSTool();

            var key = KeyManager.GetVerificationCodeKey(mobile);

            bool flag = RedisTool.AddItemToList<string>(verificationCode, key, DateTime.Now.AddMinutes(30), FunctionType.ZuShouVerificationCode, 0); //存储验证码

            if (flag) //说明短信发送成功
            {
                string str = "您的验证码是：" + verificationCode;
                if (msg != "")
                {
                    str = string.Format(msg, verificationCode);
                }
                smsTool.sendSms(SMSOptionType.SEND_SMS, mobile.Trim(), str);
                ChangeMobileVerificationCodeCount(mobile);
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 图片验证码

        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <param name="length">验证码的长度</param>
        /// <returns>验证码</returns>
        public static string CreateValidateNumber(int length)
        {
            int[] randMembers = new int[length];
            int[] validateNums = new int[length];
            StringBuilder validateNumberStr = new StringBuilder();
            //生成起始序列值
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            Random seekRand = new Random(seekSeek);
            int beginSeek = (int)seekRand.Next(0, Int32.MaxValue - length * 10000);
            int[] seeks = new int[length];
            for (int i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }
            //生成随机数字
            for (int i = 0; i < length; i++)
            {
                Random rand = new Random(seeks[i]);
                int pownum = 1 * (int)Math.Pow(10, length);
                randMembers[i] = rand.Next(pownum, Int32.MaxValue);
            }
            //抽取随机数字
            for (int i = 0; i < length; i++)
            {
                string numStr = randMembers[i].ToString();
                int numLength = numStr.Length;
                Random rand = new Random();
                int numPosition = rand.Next(0, numLength - 1);
                validateNums[i] = Int32.Parse(numStr.Substring(numPosition, 1));
            }
            //生成验证码
            for (int i = 0; i < length; i++)
            {
                validateNumberStr.Append(validateNums[i].ToString());
            }
            return validateNumberStr.ToString();
        }


        /// <summary>
        /// douhaichao
        /// 2013年11月13日 09:14:58
        /// 创建图片验证码
        /// </summary>
        public byte[] CreateCodeImage()
        {
            string cookieName = GetCookieName();
            string str_ValidateCode = CreateValidateNumber(4);
            RedisHelper.SetValue<string>(cookieName, str_ValidateCode, DateTime.Now.AddMinutes(15), FunctionType.ZuShouVerificationCode, 0);
            return CommonCode(str_ValidateCode);
        }

        #endregion

        /// <summary>
        /// douhaichao
        /// 2013年11月12日 11:47:46
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public string GetCode()
        {
            string cookieName = GetCookieName();
            string value = RedisHelper.GetValue<string>(cookieName, FunctionType.ZuShouVerificationCode, 0);
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            return value;
        }

     
        /// <summary>
        /// douhaichao
        /// 2013年11月13日 09:14:44
        /// 从redis里删除当前用户的验证码
        /// </summary>
        public void DelCode()
        {
            string cookieName = GetCookieName();
            RedisHelper.RemoveAllFromList(cookieName, FunctionType.ZuShouVerificationCode, 0);
        }

        /// <summary>
        /// douhaichao
        /// 2013年11月13日 09:14:33
        /// 检测当前验证码是否正确
        /// </summary>
        /// <param name="code">输入的验证码</param>
        /// <param name="delcode">是否删除该验证码，默认不删除。注意：点击发房源时该参数必须为true</param>
        /// <returns></returns>
        public bool CheckCode(string code, bool delcode = false)
        {
            if (GetCode() == code.Trim())
            {
                if (delcode)
                {
                    DelCode();
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// douhaichao
        /// 2013年11月12日 11:47:50
        /// 设置当前用户的验证码
        /// </summary>
        /// <returns></returns>
        private string GetCookieName()
        {
            string cookieName = "kyjzsverificationcode";
            string verificationcode = CookieHelper.GetCookie(cookieName);
            if (string.IsNullOrEmpty(verificationcode))
            {
                verificationcode = Guid.NewGuid().ToString();
                CookieHelper.SetCookie(cookieName, verificationcode);
            }
            return verificationcode;
        }

        /// <summary>
        ///  创建验证码调用的公用方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public byte[] CommonCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return null;
            char[] checkCode = code.ToCharArray();
            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 18.5)), 22);
            System.Drawing.Graphics g = Graphics.FromImage(image);

            try
            {
                Random random = new Random();
                //清空图片背景色 
                g.Clear(Color.White);

                //画图片的背景噪音线 
                for (int i = 0; i < 20; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                //定义颜色
                Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Brown, Color.DarkCyan, Color.Purple };
                //定义字体
                string[] f = { "黑体" };
                //Random rLeftNum = new Random();//定义折叠字

                for (int k = 0; k <= checkCode.Length - 1; k++)
                {
                    int cindex = random.Next(6);
                    int findex = random.Next(1);

                    Font drawFont = new Font(f[findex], 14, (System.Drawing.FontStyle.Bold));

                    SolidBrush drawBrush = new SolidBrush(c[cindex]);

                    float x = 5.0F;
                    float y = 0.0F;
                    float width = 12.0F;
                    float height = 20.0F;
                    int sjx = random.Next(10);
                    //if (k != 0)
                    //{
                    //    sjx -= rLeftNum.Next(6);
                    //}
                    int sjy = random.Next(image.Height - (int)height);

                    RectangleF drawRect = new RectangleF(x + sjx + (k * 15), y + sjy, width, height);

                    StringFormat drawFormat = new StringFormat();
                    drawFormat.Alignment = StringAlignment.Center;

                    g.DrawString(checkCode[k].ToString(), drawFont, drawBrush, drawRect, drawFormat);
                }

                //画图片的前景噪音点 
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线 
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                //Response.Clear();
                //Response.ContentType = "image/Gif";
                //Response.BinaryWrite(ms.ToArray());
                return ms.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
        private Color GetRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            //  对于C#的随机数，没什么好说的
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);
            //  为了在白色背景上显示，尽量生成深色
            int int_Red = RandomNum_First.Next(256);
            int int_Green = RandomNum_Sencond.Next(256);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;
            return Color.FromArgb(int_Red, int_Green, int_Blue);
        }
    }
}
