using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Model
{
    public class Password
    {
        public string Code(string str, int code=0)
        {
            StringBuilder sb = new StringBuilder(str);
            for (int i = 0; i < str.Length; i++)
                sb[i] = (char)(((int)str[i])+code);//Смещаем на один символ
            return sb.ToString();
        }

        public string Decode(string str, int code = 0)
        {
            StringBuilder sb = new StringBuilder(str);
            for (int i = 0; i < str.Length; i++)
                sb[i] = (char)(((int)str[i]) - code);//Смещаем на один символ
            return sb.ToString();
        }




    }
}
