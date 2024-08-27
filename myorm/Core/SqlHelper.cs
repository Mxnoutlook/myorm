using myorm.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using myorm.Core.ExpressionExtend;


namespace myorm.Core
{
    public class SqlHelper
    {
        private readonly string ConnectionStringCustomers = "Data Source=localhost; Database=customer_order; User ID=root; Password=123456;charset=UTF8";


        public T Find<T>(int id) where T : BaseModel,new()
        {
            string sql = SqlCacheBuilder<T>.GetSql(SqlCacheBuilderType.FindOne);

            using (MySqlConnection conn = new MySqlConnection(ConnectionStringCustomers))
            {
                MySqlCommand command = new MySqlCommand(sql, conn);
                command.Parameters.Add(new MySqlParameter($"@Id",id));
                conn.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    T t=Activator.CreateInstance<T>();
                    // 2、通过反射进行数据的动态绑定

                    foreach(var property in typeof(T).GetProperties())
                    {
                        // 数据库的NULL和程序的Null
                        property.SetValue(t, reader[property.GetMappingName()] is DBNull ? null : reader[property.GetMappingName()]);
                    }

                    return t;
                }
                else
                {
                    return null;
                }
            }
        }



        public bool Insert<T>(T t) where T : BaseModel
        {

            Type type = typeof(T);

            string sql = SqlCacheBuilder<T>.GetSql(SqlCacheBuilderType.Insert);
            var parameters = type.GetProperties().Select(p => new MySqlParameter($"@{p.GetMappingName()}", p.GetValue(t) ?? DBNull.Value)).ToArray();

            using (MySqlConnection conn = new MySqlConnection(ConnectionStringCustomers))
            {
                MySqlCommand command = new MySqlCommand(sql, conn);
                command.Parameters.AddRange(parameters);
                conn.Open();
                int iResult = command.ExecuteNonQuery();

                return iResult == 1;
            }

        }
  
    
         public bool Update<T>(T t) where T : BaseModel
        {

            if (!t.Vaildate())
            {
                throw new Exception("数据验证失败");
            }
            Type type = typeof(T);
            string sql = SqlCacheBuilder<T>.GetSql(SqlCacheBuilderType.UpdateOne);
            var parameters = type.GetProperties().Select(p => new MySqlParameter($"@{p.GetMappingName()}", p.GetValue(t) ?? DBNull.Value )).ToArray();

            using (MySqlConnection conn = new MySqlConnection(ConnectionStringCustomers))
            {
                MySqlCommand command = new MySqlCommand(sql, conn);
                command.Parameters.AddRange(parameters);
                conn.Open();
                return 1== command.ExecuteNonQuery();
            }
        }


        public bool Delete<T>(T t) where T : BaseModel
        {
            string sql=  SqlCacheBuilder<T>.GetSql(SqlCacheBuilderType.Delete);
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("@Id",t.Id)};

            return this.ExecuteSql<bool>(sql, parameters, sqlCommand=>1 == sqlCommand.ExecuteNonQuery());
        }


        private T ExecuteSql<T> (string sql, MySqlParameter[] parameters, Func<MySqlCommand,T> func)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionStringCustomers))
            {
                MySqlCommand command = new MySqlCommand(sql, conn);
                command.Parameters.AddRange(parameters);
                conn.Open();
                return func.Invoke(command);
            }

        }

        public bool DeleteCondition<T>(Expression<Func<T,bool>> expression)
        {
            Type type = typeof(T);
         
            CustomExpressionVisitor visitor = new CustomExpressionVisitor();
            visitor.Visit(expression);
            string where = visitor.GetWhere();
            string sql = $"Delete From `{type.GetMappingName()}` where {where}";

            //执行ADO
            using (MySqlConnection conn = new MySqlConnection(ConnectionStringCustomers))
            {
                MySqlCommand command = new MySqlCommand(sql, conn);
                conn.Open();
                int iResult = command.ExecuteNonQuery();
                return iResult > 0;
            }
        }
    }
}
