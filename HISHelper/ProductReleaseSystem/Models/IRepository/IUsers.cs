using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.Models.IRepository
{
    public interface IUsers
    {
        /// <summary>
        /// 查询用户所有信息
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> SelectUsers();
        /// <summary>
        /// 添加用户详细信息
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <param name="Characte">角色</param>
        /// <returns></returns>
         bool InsertUsers(string UserName, string PassWord, string Characte);
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <param name="Characte">密码</param>
        /// <param name="ID">用户ID</param>
        /// <returns></returns>
        bool UpdateUsers(String UserName, string PassWord,string Characte, int ID);

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        bool DeleteUsers(int ID);
        /// <summary>
        /// 查询部门所有信息
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> SelectDepartments();
        /// <summary>
        ///  添加部门详细信息
        /// </summary>
        /// <param name="DepartmentName">部门名称</param>
        /// <returns></returns>
        bool InsertDepartments(string DepartmentName);
        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="DepartmentName">部门名称</param>
        /// <param name="ID">部门ID</param>
        /// <returns></returns>
        bool UpdateDepartments(String DepartmentName, int ID);
        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="ID">部门ID</param>
        /// <returns></returns>
        bool DeleteDepartments(int ID);

        /// <summary>
        /// 查询开发人员信息
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> SelectDevelopers();

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
        bool InsertDevelopers(string Name,string Sex,string QQ,string Email,string Phone,int DepartmentID);
        /// <summary>
        /// 修改开发人员信息
        /// </summary>
        /// <param name="Name">姓名</param>
        /// <param name="Sex">性别</param>
        /// <param name="QQ">QQ</param>
        /// <param name="Email">邮箱</param>
        /// <param name="Phone">电话</param>
        /// <param name="DepartmentID">部门ID</param>
        /// <param name="ID">开发人员ID</param>
        /// <returns></returns>
        bool UpdateDevelopers(string Name, string Sex, string QQ, string Email, string Phone, int DepartmentID,int ID);

        /// <summary>
        /// 删除开发人员信息
        /// </summary>
        /// <param name="ID">开发人员ID</param>
        /// <returns></returns>
        bool DeleteDevelopers(int ID);


    }
}
