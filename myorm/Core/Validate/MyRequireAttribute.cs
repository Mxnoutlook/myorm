using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myorm.Core.Validate
{
    public class MyRequireAttribute : MyBaseValidateAttribute
    {
        public override bool ValidateCore(object oValue)
        {
            return oValue != null;
        }
    }
}
