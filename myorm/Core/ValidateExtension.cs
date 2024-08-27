using myorm.Core.Attributes;
using myorm.Core.Validate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myorm.Core
{
    public static class ValidateExtension
    {
        public static bool Vaildate<T>(this T t)
        {

            Type type = typeof(T);
            foreach (var property in type.GetProperties())
            {
           
                if (property.IsDefined(typeof(MyBaseValidateAttribute), true))
                {
                    var attributes = property.GetCustomAttributes(typeof(MyBaseValidateAttribute), true);
                    object oValue = property.GetValue(t);
                    foreach (MyBaseValidateAttribute attribute in attributes)
                    {
                        if (!attribute.ValidateCore(oValue))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
