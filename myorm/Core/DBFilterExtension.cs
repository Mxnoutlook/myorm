using myorm.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace myorm.Core
{
    public static class DBFilterExtension
    {
        public static IEnumerable<PropertyInfo> GetProNokey(this Type type)
        {

            return type.GetProperties().Where(p => !p.IsDefined(typeof(MyKeyAttribute), true));
        }
    }
}
