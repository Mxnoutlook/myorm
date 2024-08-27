using myorm.Core;
using myorm.Model;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace myorm
{
    public class Program
    {
        static void Main(string[] args)
        {
            SqlHelper sqlHelper = new SqlHelper();

            Expression<Func<Company, bool>> expression = c => c.Id > 10;
            sqlHelper.DeleteCondition<Company>(expression);

        }
    }
}