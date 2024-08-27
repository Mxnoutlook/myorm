using myorm.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace myorm.Core
{
    public static class DBMappingExtension
    {
        public static string GetMappingName<T>(this T t) where T : MemberInfo
        {

            if (t.IsDefined(typeof(MyBaseMappingAttribute), true))
            {
                // 返回的是特性集合
                var Tableattribute = t.GetCustomAttribute<MyBaseMappingAttribute>();
                return Tableattribute.GetMappingName();
            }
            else
            {
                return t.Name;
            }


        }


        //public static string GetMappingTableName(this Type type)
        //{

        //    if (type.IsDefined(typeof(MyTableAttribute), true))
        //    {
        //        // 返回的是多个特性集合
        //        var Tableattribute = type.GetCustomAttribute<MyTableAttribute>();
        //        return Tableattribute.GetMappingTableName();
        //    }
        //    else
        //    {
        //        return type.Name;
        //    }


        //}


        //public static string GetMappingColumonName(this PropertyInfo property)
        //{
        //    string name = null;
        //    if (property.IsDefined(typeof(MyColumonAttribute), true))
        //    {
        //        var attribute = property.GetCustomAttribute<MyColumonAttribute>();
        //        name = attribute.GetMappingColumonName();
        //    }
        //    else
        //    {
        //        name = property.Name;
        //    }
        //    return name;
        //}


    }
}
