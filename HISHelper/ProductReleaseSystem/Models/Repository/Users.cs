using Microsoft.Extensions.Options;
using ProductReleaseSystem.Data;
using ProductReleaseSystem.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.Models.Repository
{
    public class Users : IUsers
    {
        /// <summary>
        /// 数据库操作方法
        /// </summary>
        DBHelper _dbhelper;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="connections"></param>
        public Users(IOptions<ConnectionSetting> connections)
        {
            _dbhelper = new DBHelper(connections.Value.prConnectionStrings);
        }
        #region 查询用户信息
        /// <summary>
        /// 查询用户所有信息
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> SelectUsers()
        {
            var sql = @"select ID,UserName,PassWord,Character from Users";
            return _dbhelper.GetList(sql);
        }
        #endregion
        #region  添加用户信息
        /// <summary>
        /// 添加用户所有信息
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <param name="Character">角色</param>
        /// <returns></returns>
        public bool InsertUsers(string UserName,string PassWord,string Character)
        {
            var sql = @"insert into Users(UserName,PassWord,Character) value(@UserName,@PassWord,@Character)";
            var pars = new List<SqlParameter>();

            pars.Add(new SqlParameter() { ParameterName = "@UserName", SqlDbType = System.Data.SqlDbType.Int, Value = UserName });
            pars.Add(new SqlParameter() { ParameterName = "@PassWord", SqlDbType = System.Data.SqlDbType.VarChar, Value = PassWord });
            pars.Add(new SqlParameter() { ParameterName = "@Character", SqlDbType = System.Data.SqlDbType.VarChar, Value = Character });

            return _dbhelper.SavaData(sql,pars.ToArray())>0?true:false;
        }
        #endregion
        #region 修改用户信息
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWord"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool UpdateUsers(string UserName, string PassWord,string Character, int ID)
        {
            var sql = @"update Users set UserName='@UserName',PassWord='@PassWord',Character='@Character' where ID='@ID'";
            var pars = new List<SqlParameter>();

            pars.Add(new SqlParameter() { ParameterName = "@UserName", SqlDbType = System.Data.SqlDbType.Int, Value = UserName });
            pars.Add(new SqlParameter() { ParameterName = "@PassWord", SqlDbType = System.Data.SqlDbType.VarChar, Value = PassWord });
            pars.Add(new SqlParameter() { ParameterName = "@Character", SqlDbType = System.Data.SqlDbType.VarChar, Value = Character });
            pars.Add(new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = ID });

            return _dbhelper.SavaData(sql, pars.ToArray()) > 0 ? true : false;
        }
        #endregion
        #region 修改用户信息
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="ID">用户ID</param>
        /// <returns></returns>
        public bool DeleteUsers(int ID)
        {
            var sql = @"delete from Users where ID=@ID";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = ID };
            return _dbhelper.SavaData(sql, par)>0?true:false;
        }
        #endregion


        #region 查询部门信息
        /// <summary>
        /// 查询部门信息
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> SelectDepartments()
        {
            var sql = @"select DepartmentName from Departments";
            return _dbhelper.GetList(sql);
        }
        #endregion
        #region 添加部门信息
        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="DepartmentName">部门</param>
        /// <returns></returns>
        public bool InsertDepartments(string DepartmentName)
        {
            var sql = @"insert into Departments(DepartmentName) value(@DepartmentName)";
            var par = new SqlParameter() { ParameterName = "@DepartmentName", SqlDbType = System.Data.SqlDbType.Int, Value = DepartmentName };
            return _dbhelper.SavaData(sql, par) > 0 ? true : false;
        }
        #endregion
        #region 修改部门信息
        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="DepartmentName">部门</param>
        /// <returns></returns>
        public bool UpdateDepartments(string DepartmentName,int ID)
        {
            var sql = @"Update Departments set DepartmentName=@'DepartmentName'where ID =@ID";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter() { ParameterName = "@DepartmentName", SqlDbType = System.Data.SqlDbType.VarChar, Value = DepartmentName });
            pars.Add(new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = ID });

            return _dbhelper.SavaData(sql, pars.ToArray()) > 0 ? true : false;
        }
        #endregion
        #region 删除部门信息
        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="ID">部门ID</param>
        /// <returns></returns>
        public bool DeleteDepartments(int ID)
        {
            var sql = @"delete from Departments where ID=@ID";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = ID };
            return _dbhelper.SavaData(sql, par) > 0 ? true : false;
        }
        #endregion



        #region 添加开发人员信息
        /// <summary>
        /// 添加开发人员信息
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> SelectDevelopers()
        {
            var sql = @" select ID, Name, Sex, QQ, Email, Phone, DepartmentID from Developers";
            return _dbhelper.GetList(sql);
        }
        #endregion
        #region 添加开发人员信息
        /// <summary>
        /// 添加开发人员信息
        /// </summary>
        /// <param name="Name">姓名</param>
        /// <param name="Sex">性别</param>
        /// <param name="QQ">QQ</param>
        /// <param name="Email">邮箱</param>
        /// <param name="Phone">电话</param>
        /// <param name="DepartmentID">部门ID</param>
        /// <returns></returns>
        public bool InsertDevelopers(string Name, string Sex, string QQ, string Email, string Phone, int DepartmentID)
        {
            var sql = @"insert into Developers(Name,Sex,QQ,Email,Phone,DepartmentID) value(@Name,@Sex,@QQ,@Email,@Phone,@DepartmentID)";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter() { ParameterName = "@Name", SqlDbType = System.Data.SqlDbType.Int, Value = Name });
            pars.Add(new SqlParameter() { ParameterName = "@Sex", SqlDbType = System.Data.SqlDbType.VarChar, Value = Sex });
            pars.Add(new SqlParameter() { ParameterName = "@QQ", SqlDbType = System.Data.SqlDbType.VarChar, Value = QQ });
            pars.Add(new SqlParameter() { ParameterName = "@Email", SqlDbType = System.Data.SqlDbType.Int, Value = Email });
            pars.Add(new SqlParameter() { ParameterName = "@Phone", SqlDbType = System.Data.SqlDbType.VarChar, Value = Phone });
            pars.Add(new SqlParameter() { ParameterName = "@DepartmentID", SqlDbType = System.Data.SqlDbType.VarChar, Value = DepartmentID });
            return _dbhelper.SavaData(sql, pars.ToArray()) > 0 ? true : false;
        }
        #endregion
        #region 修改开发人员
        /// <summary>
        /// 修改开发人员
        /// </summary>
        /// <param name="Name">姓名</param>
        /// <param name="Sex">性别</param>
        /// <param name="QQ">QQ</param>
        /// <param name="Email">邮箱</param>
        /// <param name="Phone">电话</param>
        /// <param name="DepartmentID">部门ID</param>
        /// <param name="ID">开发人员ID</param>
        /// <returns></returns>
        public bool UpdateDevelopers(string Name, string Sex, string QQ, string Email, string Phone, int DepartmentID, int ID)
        {
            var sql = @"Update Developers set Name=@'Name',Sex=@Sex,QQ=@QQ,Email=@Email,Phone=@Phone,DepartmentID=@DepartmentID where ID =@ID";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter() { ParameterName = "@Name", SqlDbType = System.Data.SqlDbType.VarChar, Value = Name });
            pars.Add(new SqlParameter() { ParameterName = "@Sex", SqlDbType = System.Data.SqlDbType.VarChar, Value = Sex });
            pars.Add(new SqlParameter() { ParameterName = "@QQ", SqlDbType = System.Data.SqlDbType.VarChar, Value = QQ });
            pars.Add(new SqlParameter() { ParameterName = "@Email", SqlDbType = System.Data.SqlDbType.VarChar, Value = Email });
            pars.Add(new SqlParameter() { ParameterName = "@Phone", SqlDbType = System.Data.SqlDbType.VarChar, Value = Phone });
            pars.Add(new SqlParameter() { ParameterName = "@DepartmentID", SqlDbType = System.Data.SqlDbType.VarChar, Value = DepartmentID });
            pars.Add(new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = ID });
            return _dbhelper.SavaData(sql, pars.ToArray()) > 0 ? true : false;
        }
        #endregion
        #region 删除开发人员
        /// <summary>
        /// 删除开发人员
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteDevelopers(int ID)
        {
            var sql = @"delete from Developers where ID=@ID";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = ID };
            return _dbhelper.SavaData(sql, par) > 0 ? true : false;
        }
        #endregion
    }
}
