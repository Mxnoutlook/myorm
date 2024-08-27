using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myorm.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MyTableAttribute:MyBaseMappingAttribute
    {
   
        public MyTableAttribute(string tableName) :base(tableName)
        { 
           // this._TableName = tableName;    
        }

    }
}
