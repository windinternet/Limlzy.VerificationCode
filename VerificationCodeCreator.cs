using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace Limlzy.VerificationCode
{
    //=========================================================================
    // *  作者：   杨泉耀
    // *  时间：   2017/3/1 11:57:26
    // *  文件名： VerificationCodeCreator      
    // *  版本：   1.0
    // *  说明：    
    //=========================================================================
    public class VerificationCodeCreator
    {

        public Confusionlevel Level { get; set; }
        public CharSet Charset { get; set; }
        public bool AutoSize { get; set; }
        public SizeF ImageSize { get; set; }
        public string Content { get; set; }
        public string CodeString { get; private set; }
        public Bitmap Image { get; private set; }
        public float FontSize { get; set; }
        public Color BackColor { get; set; }
        private static Color[] _Colors = new Color[] { 
            Color.FromArgb(21,163,133),
            Color.FromArgb(200,29,43,83),
            Color.FromArgb(252,101,101),
            Color.FromArgb(153,202,186),
            Color.FromArgb(243,108,104),
            Color.FromArgb(33,44,55),
            Color.FromArgb(21,165,133)
        };
        public string FontFamily { get;set; }
        public static Color[] Colors { get { return _Colors; } }
        public VerificationCodeCreator()
        {
            AutoSize = true;
            FontSize = 20;
            FontFamily="Microsoft YaHei";
            BackColor = Color.Transparent;
        }
       
        public int Length
        {
            get
            {
                if (CodeString == null)
                    return 0;
                else
                    return CodeString.Length;
            }
        }
        public void CreateImage(int len=4,string chars = null)
        {
            if (chars == null)
            {
                CodeString = CreateRandString(Charset, len);
            }
            else
            {
                CodeString = chars;
            }
            if (AutoSize)
            {
                ImageSize = new SizeF((float)FontSize*len+len*10,FontSize*(float)1.4);
            }
            Image = new Bitmap((int)ImageSize.Width,(int)ImageSize.Height);
            Graphics g = Graphics.FromImage(Image);
            if (BackColor != Color.Transparent)
            {
                g.Clear(BackColor);
            }
            //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Random rand=new Random((int )DateTime.Now.Ticks);
            for (int  i=0;i<CodeString.Length;i++)
            { 
                Pen p=new Pen(Colors[rand.Next(0,Colors.Length)]);
                g.DrawString(CodeString[i].ToString(), new Font(new FontFamily(FontFamily), (float)FontSize, FontStyle.Regular, GraphicsUnit.Pixel), p.Brush, new PointF(i*FontSize+(i+1)*5,5));
                for (int k = 0; k < (int)Level; k++)
                {
                    g.DrawLine(p, new Point(rand.Next(0, (int)ImageSize.Width), rand.Next(0, (int)ImageSize.Height)), new Point(rand.Next(0, (int)ImageSize.Width), rand.Next(0, (int)ImageSize.Height)));

                }
            }
            g.Dispose();

        }


        
        public static string CreateRandString(CharSet set, int length)
        {
            if (length == 0)
                throw new ArgumentException("错误的参数值，长度不能为0");
            StringBuilder sb = new StringBuilder(length);
            int chinaStart = 0x4e00;
            int chinaEnd = 0x9fa5;
            int numberStart = 48;
            int numberEnd = numberStart + 10;
            int englishStart = 65;
            int englishEnd = 65 + 26;
            Random rand = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < length; i++)
            {
                switch (set)
                {
                    case CharSet.China:
                        sb.Append((Char)rand.Next(chinaStart, chinaEnd));
                        break;
                    case CharSet.Number:

                        sb.Append((Char)rand.Next(numberStart, numberEnd));
                        break;
                    case CharSet.English:
                        var char_ = (Char)rand.Next(englishStart, englishEnd);
                        if (rand.Next(1, 10) % 2 == 0)
                            char_ += ' ';
                        sb.Append(char_);
                        break;
                    default:
                        if (set == (CharSet.China | CharSet.English))
                        {
                            if (rand.Next(1, 10) % 2 == 0)
                            {
                                sb.Append((Char)rand.Next(chinaStart, chinaEnd));
                            }
                            else
                            {
                                var charLower = (Char)rand.Next(englishStart, englishEnd);
                                if (rand.Next(1, 10) % 2 == 0)
                                    charLower += ' ';
                                sb.Append(charLower);
                            }
                        }
                        else if (set == (CharSet.China | CharSet.Number))
                        {
                            if (rand.Next(1, 10) % 2 == 0)
                            {
                                sb.Append((Char)rand.Next(chinaStart, chinaEnd));
                            }
                            else
                            {
                                sb.Append((Char)rand.Next(numberStart, numberEnd));
                            }
                        }
                        else if (set == (CharSet.Number | CharSet.English))
                        {
                            if (rand.Next(1, 10) % 2 == 0)
                            {
                                var charLower = (Char)rand.Next(englishStart, englishEnd);
                                if (rand.Next(1, 10) % 2 == 0)
                                    charLower += ' ';
                                sb.Append(charLower);
                            }
                            else
                            {
                                sb.Append((Char)rand.Next(numberStart, numberEnd));
                            }
                        }
                        else if (set == (CharSet.Number | CharSet.English | CharSet.China))
                        {
                            var arg = rand.Next(1, 15) % 3;
                            if (arg == 0)
                            {
                                sb.Append((Char)rand.Next(chinaStart, chinaEnd));
                            }
                            else if (arg == 1)
                            {
                                var charLower = (Char)rand.Next(englishStart, englishEnd);
                                if (rand.Next(1, 10) % 2 == 0)
                                    charLower += ' ';
                                sb.Append(charLower);
                            }
                            else if (arg == 2)
                            {
                                sb.Append((Char)rand.Next(chinaStart, chinaEnd));
                            }
                        }
                        else
                        {
                            throw new ArgumentException("无效的Charset组合");
                        }
                        break;
                }
            }
            return sb.ToString();
        }

    }

}
