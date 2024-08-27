using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myorm.Core.Validate
{
    public  class MyLengthAttribute:MyBaseValidateAttribute
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public MyLengthAttribute(int min = 10, int max = 100)
        {
            this.MinLength = min;
            this.MaxLength = max;
        }
        public override bool ValidateCore(object oValue)
        {
            return oValue != null
                && oValue.ToString()!.Length >= this.MinLength
                && oValue.ToString()!.Length <= this.MaxLength;
        }

    }
}
