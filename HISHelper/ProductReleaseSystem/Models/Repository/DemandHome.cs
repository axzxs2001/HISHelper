using ProductReleaseSystem.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductReleaseSystem.ProductRelease;
using System.Data.SqlClient;
using ProductReleaseSystem.Data;
using Microsoft.Extensions.Options;

namespace ProductReleaseSystem.Models.Repository
{
    public class DemandHome:IDemandHome
    {
        /// <summary>
        /// 数据库操作对象
        /// </summary> 
        DBHelper _dbHelper;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="connections"></param>
        public DemandHome(IOptions<ConnectionSetting> connections)
        {
            _dbHelper = new DBHelper(connections.Value.prConnectionStrings);
        }

        #region 查询所有
        #region 查询产品
        /// <summary>
        /// 查询所有产品
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> QueryAllProducts()
        {
            var sql = @"select distinct 
a.ID AS ID,
a.ProductName AS 产品名 from Products a join RequestForm b on a.ID=b.ProductID and DeleteStatus=1 and  Status!='未通过'";
            return _dbHelper.GetList(sql);
        }
        #endregion
        #region 根据产品ID查询需求
        /// <summary>
        /// 根据产品ID查询需求
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        public List<Dictionary<string,dynamic>> QueryAllRequestForms(int id)
        {
            var sql = @"select
a.ID AS ID,
a.DemandNname AS 标题,
a.Priority AS 优先级,
b.Name AS 发布人,
a.Status AS 状态,
MakeTime AS 发布时间 from RequestForm a join Products c on a.ProductID=c.ID join Developers b on a.ImplementerID=b.ID where a.ProductID=@ID and a.Status!='未通过' and DeleteStatus=1";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetList(sql, par);
        }
        #endregion
        #region 查询条数
        /// <summary>
        /// 查询除了未通过审核的需求共有多少条需求
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        public object QueryRequestsCount(int id)
        {
            var sql = @"select count(*) AS COUNT from RequestForm where ProductID=@ID and Status!='未通过' and DeleteStatus=1";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetValue(sql, par);
        }
        #endregion
        #endregion

        #region 与我相关
        #region 查询与我相关的产品
        /// <summary>
        /// 查询与我相关的产品
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> QueryAndMeProduct(int id)
        {
            var sql = @"select distinct 
a.ID AS ID,
a.ProductName AS 产品名 from Products a join RequestForm b on a.ID=b.ProductID and DeleteStatus=1 and  Status!='未通过' AND b.ImplementerID=@ID";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetList(sql, par);
        }
        #endregion
        #region 查询与我相关的需求
        /// <summary>
        /// 查询与我相关的需求
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <param name="pid">产品ID</param>
        /// <returns></returns>
        public List<Dictionary<string,dynamic>> QueryAndMeRequest(int id,int pid)
        {
            var sql = @"SELECT a.ID AS ID,
a.DemandNname AS 标题,
a.Status AS 状态,
b.Name AS 发布人 FROM RequestForm a JOIN Products c ON a.ProductID=c.ID JOIN Developers b ON b.ID = a.ImplementerID WHERE a.ImplementerID=@ID AND a.DeleteStatus=1 AND Status!='未通过' and a.ProductID=@PID";
            var par1 = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            var par2 = new SqlParameter() { ParameterName = "@PID", SqlDbType = System.Data.SqlDbType.Int, Value = pid };
            return _dbHelper.GetList(sql, par1, par2);
        }
        #endregion
        #region 查询与我相关的条数
        /// <summary>
        /// 查询与我相关的条数
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <param name="pid">产品ID</param>
        /// <returns></returns>
        public object QueryAndMeCount(int id,int pid)
        {
            var sql = @"select count(*) AS COUNT from RequestForm where ProductID=@PID and ImplementerID=@ID and Status!='未通过' and DeleteStatus=1";
            var par1 = new SqlParameter() { ParameterName = "@PID", SqlDbType = System.Data.SqlDbType.Int, Value = pid };
            var par2 = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetValue(sql, par1, par2);
        }
        #endregion
        #endregion

        #region 在研项目
        #region 查询在研产品
        /// <summary>
        /// 查询在研产品
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string,dynamic>> QueryZyProduct()
        {
            var sql = @"select distinct 
a.ID AS ID,
a.ProductName AS 产品名 from Products a join RequestForm b on a.ID=b.ProductID and DeleteStatus=1 and  Status!='未通过' and Status!='已确认' and Status!='审核通过' and Status!='已完成'";
            return _dbHelper.GetList(sql);
        }
        #endregion
        #region 根据产品ID查询在研需求
        /// <summary>
        /// 根据产品ID查询在研需求
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        public List<Dictionary<string,dynamic>> QueryZyRequestForm(int id)
        {
            var sql = @"select 
a.ID AS ID,
a.DemandNname AS 标题,
b.StartTime AS 开始时间,
b.ExpectedTime AS 结束时间,
c.Name as 接受人 from RequestForm a join BeingStudied b on a.ID=b.DemandID join Developers c on b.DeveloperID=c.ID join Products d on a.ProductID=d.ID and Status!='未通过' and Status!='已确认' and Status!='审核通过' and Status!='已完成' and DeleteStatus=1 and a.ProductID=@ID";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetList(sql,par);
        }
        #endregion
        #region 根据产品ID查询在研项目需求条数
        /// <summary>
        /// 根据产品ID查询在研项目需求条数
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        public object QueryZyCount(int id)
        {
            var sql= @"select count(*) AS COUNT from RequestForm where ProductID=@ID and Status!='未通过' and Status!='已确认' and Status!='审核通过' and Status!='已完成' and DeleteStatus=1";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetValue(sql, par);
        }
        #endregion
        #endregion

        #region 已完成的
        #region 已完成的产品
        /// <summary>
        /// 已完成的产品
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> QueryFinishProduct()
        {
            var sql = @"select distinct 
a.ID AS ID,
a.ProductName AS 产品名 from Products a join RequestForm b on a.ID=b.ProductID and DeleteStatus=1 and Status='已完成'";
            return _dbHelper.GetList(sql);
        }
        #endregion
        #region 已完成的需求
        /// <summary>
        /// 已完成的产品需求名
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        public List<Dictionary<string,dynamic>> QueryFinishRequestForm(int id)
        {
            var sql = @"select 
a.ID AS ID,
a.DemandNname AS 标题,
b.name AS 发布人,
a.Status AS 状态,
a.MakeTime AS 发布时间,
a.VersionNumber AS 版本号 from RequestForm a join Developers b on a.ImplementerID=b.ID join Products c on a.ProductID=c.ID where a.ProductID=@ID and a.Status='已完成' and DeleteStatus=1";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetList(sql, par);
        }
        #endregion
        #region 已完成的需求条数
        /// <summary>
        /// 已完成的需求条数
        /// </summary>
        /// <param name="id">产品id</param>
        /// <returns></returns>
        public object QueryCountFinish(int id)
        {
            var sql = @"select count(*) AS COUNT from RequestForm where ProductID=@ID and Status='已完成' and DeleteStatus=1";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetValue(sql, par);
        }
        #endregion
        #endregion

        #region 未开始的
        #region 未开始的产品
        /// <summary>
        /// 未开始的产品
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string,dynamic>> QueryNoProduct()
        {
            var sql = @"select distinct 
a.ID AS ID,
a.ProductName AS 产品名 from Products a join RequestForm b on a.ID=b.ProductID and DeleteStatus=1
 and Status='已确认'";
            return _dbHelper.GetList(sql);
        }
        #endregion
        /// <summary>
        /// 未开始的需求
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        #region 未开始的需求
        public List<Dictionary<string,dynamic>> QueryNoRequestForm(int id) {
            var sql = @"select 
a.ID AS ID,
a.DemandNname AS 标题,
a.Status AS 状态,
a.MakeTime AS 通过时间,
a.Priority AS 优先级,
b.name AS 发布人 from RequestForm a join Developers b on a.ImplementerID=b.ID join Products c on a.ProductID=c.ID where DeleteStatus=1 and ProductID=@ID and Status='已确认'";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetList(sql,par);
        }
        #endregion
        #region 未开始的需求条数
        /// <summary>
        /// 未开始的需求条数
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        public object QueryCountNo(int id)
        {
            var sql = @"select count(*) AS COUNT from RequestForm where ProductID=@ID and Status='已确认' and DeleteStatus=1";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetValue(sql, par);
        }
        #endregion
        #endregion
    }
}
