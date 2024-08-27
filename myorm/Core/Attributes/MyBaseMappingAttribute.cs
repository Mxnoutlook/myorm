using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myorm.Core.Attributes
{
    public class MyBaseMappingAttribute : Attribute
    {
        private string _MappingName = null;

        public MyBaseMappingAttribute(string mappingName)
        {
            _MappingName = mappingName;
        }

        public string GetMappingName()
        {
            return _MappingName;
        }
    }
}
