using System;
using System.Drawing;
using ServiceStack;

namespace TXCommons.Injection
{
    public class VerificationCode
    {
        /// <summary>
        /// douhaichao
        /// 2013年11月12日 11:47:41
        /// 创建5位数验证码,并保存在redis
        /// </summary>
        /// <returns></returns>
        public string CreateCode()
        {
            string cookieName = GetCookieName();
            string val = PubConstant.CreateValidateNumber(5);
            TXCommons.Redis.SetValue<string>(cookieName, val, DateTime.Now.AddMinutes(15),FunctionTypeEnum.VerificationCode,0);
            return val;
        }

        /// <summary>
        /// douhaichao
        /// 2013年11月12日 11:47:46
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public string GetCode()
        {
            string cookieName = GetCookieName();
            string value = TXCommons.Redis.GetValue<string>(cookieName,FunctionTypeEnum.VerificationCode,0);
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
            TXCommons.Redis.RemoveAllFromList(cookieName,FunctionTypeEnum.VerificationCode,0);
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
            string cookieName = "VerificationCode";
            string verificationcode = CookieHelper.GetCookie(cookieName);
            if (string.IsNullOrEmpty(verificationcode))
            {
                verificationcode = Guid.NewGuid().ToString();
                CookieHelper.SetCookie(cookieName, verificationcode);
            }
            return verificationcode;
        }
        /// <summary>
        /// douhaichao
        /// 2013年11月13日 09:14:58
        /// 创建图片验证码
        /// </summary>
        public byte[] CreateCodeImage()
        {
            string str_ValidateCode = CreateCode();
            if (string.IsNullOrEmpty(str_ValidateCode))
                return null;
            char[] checkCode = str_ValidateCode.ToCharArray();
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
                Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
                //定义字体
                string[] f = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "黑体" };

                for (int k = 0; k <= checkCode.Length - 1; k++)
                {
                    int cindex = random.Next(7);
                    int findex = random.Next(5);

                    Font drawFont = new Font(f[findex], 14, (System.Drawing.FontStyle.Bold));



                    SolidBrush drawBrush = new SolidBrush(c[cindex]);

                    float x = 5.0F;
                    float y = 0.0F;
                    float width = 12.0F;
                    float height = 20.0F;
                    int sjx = random.Next(10);
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
