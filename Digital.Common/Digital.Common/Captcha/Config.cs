using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Common.Captcha
{
    public class Config
    {
        const string chars = "1234567890qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM";
        public static string SessionKey
        {
            get { return "Captcha"; }
        }

        public static string InputName
        {
            get { return "Captcha"; }
        }

        private static string _CaptchaCode = string.Empty;
        public static string CaptchaCode
        {
            get 
            {
                var ramdon = new Random();
                _CaptchaCode = string.Empty;
                for (var i = 0; i < 4; i++)
                {
                    _CaptchaCode += chars.Substring(ramdon.Next(0, chars.Length - 1), 1);
                }
                return _CaptchaCode;
            }
        }
    }
}
