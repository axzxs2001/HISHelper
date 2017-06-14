using System;
using System.Collections.Generic;
using System.Linq;

using System.Data.SqlClient;

namespace ProductReleaseSystem
{
    /// <summary>
    /// 数据处理类
    /// </summary>
    public class DBHelper
    {

        public DBHelper(string hisconnection)
        {
            ConnectionString = hisconnection;
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        { get; private set; }


        /// <summary>
        /// 查询数据，返回List
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="pars">参数</param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> GetList(string sql, params SqlParameter[] pars)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(pars);
                con.Open();
                var dr = cmd.ExecuteReader();
                var list = new List<Dictionary<string, dynamic>>();
                //读取list数据
                while (dr.Read())
                {
                    var newdic = new Dictionary<string, dynamic>();
                    foreach (var dicitem in Enumerable.Range(0, dr.FieldCount).ToDictionary(dr.GetName, dr.GetValue))
                    {
                        newdic.Add(dicitem.Key, dicitem.Value == DBNull.Value ? "" : dicitem.Value);
                    }
                    list.Add(newdic);
                }
                return list;
            }
        }


        /// <summary>
        /// 查询数据，返回单个值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="pars">参数</param>
        /// <returns></returns>
        public object GetValue(string sql, params SqlParameter[] pars)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(pars);
                con.Open();
                var value = cmd.ExecuteScalar();
                return value;
            }
        }

        /// <summary>
        /// 执行存储过程,返回list
        /// </summary>
        /// <param name="procName">存储过程名字</param>
        /// <param name="pars">参数列表</param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> ExecProcedureBackList(string procName, params SqlParameter[] pars)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = procName;
                cmd.Parameters.AddRange(pars);
                con.Open();
                var dr = cmd.ExecuteReader();
                var list = new List<Dictionary<string, dynamic>>();
                //读取list数据
                while (dr.Read())
                {
                    list.Add(Enumerable.Range(0, dr.FieldCount).ToDictionary(dr.GetName, dr.GetValue));
                }
                return list;
            }
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名字</param>
        /// <param name="pars">参数列表</param>
        /// <returns></returns>
        public int ExecProcedure(string procName, params SqlParameter[] pars)
        {

            using (var con = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = procName;
                cmd.Parameters.AddRange(pars);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// 增删改数据
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="pars">参数</param>
        /// <returns></returns>
        public int SavaData(string sql, params SqlParameter[] pars)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(pars);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 事务中增删改数据
        /// </summary>
        /// <param name="sqls">sql语句列表</param>
        /// <param name="parsList">参数列表</param>
        /// <returns></returns>
        public bool SavaDataTransaction(List<string> sqls, List<List<SqlParameter>> parsList)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    var cmd = new SqlCommand();
                    cmd.Transaction = tran;
                    cmd.Connection = con;
                    for (int i = 0; i < sqls.Count; i++)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = sqls[i];
                        cmd.Parameters.AddRange(parsList[i].ToArray());
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception exc)
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    con.Close();
                }
            }

        }
        /// <summary>
        /// 在事务中执行批量SQL
        /// </summary>
        /// <param name="sqlOperations">多个SQL操作对象</param>
        /// <returns></returns>
        public bool ExecuteTransactionSql(List<SqlOperation> sqlOperations)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    var command = new SqlCommand();
                    command.Connection = connection;
                    command.Transaction = transaction;
                    foreach (var sqlOperation in sqlOperations)
                    {
                        command.CommandText = sqlOperation.Sql;
                        command.Parameters.Clear();
                        command.Parameters.AddRange(sqlOperation.parmeters);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception exc)
                {
                    transaction.Rollback();
                    throw exc;
                }
            }
        }

    }
}
