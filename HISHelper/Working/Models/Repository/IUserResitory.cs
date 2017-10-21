using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Working.Models.DataModel;

namespace Working.Model.Repository
{
    /// <summary>
    /// 用户管理接口
    /// </summary>
    public interface IUserResitory
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        User Login(string userName, string password);

        /// <summary>
        /// 获取本部门用户
        /// </summary>
        /// <param name="departmentID">部门编号</param>
        /// <returns></returns>
        List<User> GetGetDepartmentUsers(int departmentID);

        /// <summary>
        /// 按照ID获取用户
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        User GetUser(int userID);


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        bool AddUser(User user);
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        bool ModifyUser(User user);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        bool RemoveUser(int userID);
    }
}
