using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mailer.ViewModels
{
    public class PasswordValidationRule : ValidationRule
    {

        //(Строчные и прописные латинские буквы, цифры):
        //Regex regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$");

        // (Строчные и прописные латинские буквы, цифры, спецсимволы. Минимум 8 символов):
        Regex regex = new Regex(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$");

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string password = value.ToString();
            //if (password.Length < 6) return new ValidationResult(false, "Пароль меньше 6 символов");
            if (!regex.IsMatch(password)) return new ValidationResult(false, "от 6 символов с использованием цифр, спец. символов, латиницы, наличием строчных и прописных символов");
            return new ValidationResult(true, null);
        }
    }

    //Статья про то как проверять почтовые адреса
    //https://habr.com/ru/post/175375/
    public class EMailValidationRule : ValidationRule
    {

        
        //Regex regex = new Regex(@"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$");

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string adress = value.ToString();
            if (!adress.Contains('@')) return new ValidationResult(false, "Не верный адрес почты");
            return new ValidationResult(true, null);
        }
    }
}
