using myorm.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myorm.Model
{
    public abstract class BaseModel
    {
        /// <summary>
        /// 数据库主键
        /// </summary>
        [MyKey]
        public int Id { get; set; }
    }
}
