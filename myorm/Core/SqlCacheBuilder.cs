using myorm.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace myorm.Core
{
    /// <summary>
    /// 负责生成SQL，缓存重用
    /// </summary>
    public  class SqlCacheBuilder<T> where T:BaseModel
    {
        private static string _InsertSql = null;
        private static string _FindOneSql = null;
        private static string _UpdateOneSql = null;
        private static string _DeleteSql = null;
        static SqlCacheBuilder()
        {

            /// 插入的
            {
                // 1、拼装SQL语句
                Type type = typeof(T);

                // 1.1、属性名称
                string columnString = string.Join(",", type.GetProNokey().Select(p => $"`{p.GetMappingName()}`"));

                // 1.2、实体类的值
                string valueStrings = string.Join(",", type.GetProNokey().Select(p => $"@{p.GetMappingName()}"));

                string sql = $"Insert Into `{type.GetMappingName()}` ({columnString}) Values ({valueStrings})";

                _InsertSql = sql;
            }

            /// 根据Id查找
            {
                Type type = typeof(T);

                // 1.1、获得对象的属性——也就对应着数据库中的字段
                string columnString = string.Join(",", type.GetProperties().Select(p => $"`{p.GetMappingName()}`"));

                string sql = $@"SELECT {columnString} FROM `{type.GetMappingName()}` WHERE Id = @Id";
                _FindOneSql=sql;
            }

            /// 更新
            {
                Type type = typeof(T);
                string columns = string.Join(",", type.GetProNokey().Select(p => $"`{p.GetMappingName()}`=@{p.Name}"));
                string sql = $"UPDATE `{type.GetMappingName()}` SET {columns} WHERE Id= @Id";
                _UpdateOneSql = sql;
            }

            // 删除
            {
                Type type = typeof(T);
                 string   DeleteSQL = $"DELETE FROM [{type.GetMappingName()}] WHERE Id=@Id";
                _UpdateOneSql = DeleteSQL;
            }
        }


        public static string GetSql(SqlCacheBuilderType sqlCacheBuilderEnum)
        {
            switch (sqlCacheBuilderEnum)
            {
                case SqlCacheBuilderType.Insert:
                    return _InsertSql;
                case SqlCacheBuilderType.FindOne:
                    return _FindOneSql;
                case SqlCacheBuilderType.UpdateOne:
                    return _UpdateOneSql;
                case SqlCacheBuilderType.Delete:
                    return _DeleteSql;
                default:
                    throw new Exception("Unknow sqlCacheBuilder!");
            }
        }

    }


    public enum SqlCacheBuilderType
    {
        Insert,
        FindOne,
        UpdateOne,
        Delete
    }

}
