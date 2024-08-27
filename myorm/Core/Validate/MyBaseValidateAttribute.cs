using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myorm.Core.Validate
{
    public abstract class MyBaseValidateAttribute:Attribute
    {
        public abstract bool ValidateCore(object oValue);
    }
}
