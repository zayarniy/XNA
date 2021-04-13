using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertTypeValidationExample
{
    public class Man: IDataErrorInfo
    {
       
        public string Name { get; set; }
        public int Age { get; set; }

        public string this[string columnName]
        {
            get
            {
                string Error = "";
                switch(columnName)
                {
                    case "Age":
                        if ((Age<0) || (Age>100))
                        {
                            Error= "Возраст должен быть больше 0 и меньше 100";
                        }
                        break;
                    case "Name":
                        //Validation for Name
                        break;
                }
                return Error;
            }
        }

        public string Error => throw new NotImplementedException();
    }
}
