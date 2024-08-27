using myorm.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myorm.Model
{
    [MyTable("Company1")]
    public class Company : BaseModel
    {
        public string Name { get; set; }

        [MyColumon("Cres")]
        public DateTime CreateTime { get; set; }

        public int CreatorId { get; set; }

        public int? LastModifierId { get; set; }

        public DateTime? LastModifyTime { get; set; }

     
    }


}
