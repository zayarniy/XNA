using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MVVM_TestMvvmLightTools.ViewModel
{
    public class LoginValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string login = value.ToString();
            if (login.Length < 2 || login.Length > 20) return new ValidationResult(false, "ограничение 2-20 символов, которыми могут быть буквы и цифры, первый символ обязательно буква");
            return new ValidationResult(true, null);
        }
    }

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
            if (!regex.IsMatch(password)) return new ValidationResult(false, "Строчные и прописные латинские буквы, цифры, спецсимволы. Минимум 8 символов");
            return new ValidationResult(true, null);
        }
    }

}

