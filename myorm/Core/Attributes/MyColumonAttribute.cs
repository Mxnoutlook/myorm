using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myorm.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MyColumonAttribute:MyBaseMappingAttribute
    {
        /// <summary>
        /// base 调用基类构造函数
        /// </summary>
        /// <param name="mappingName"></param>
        public MyColumonAttribute(string mappingName) : base(mappingName)
        {
        
        }
    }
}
