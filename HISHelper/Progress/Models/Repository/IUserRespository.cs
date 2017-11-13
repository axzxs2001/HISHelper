using Progress.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Progress.Models.Repository
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public interface IUserRespository
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        UserRole Login(string userName, string password);
        /// <summary>
        /// 按用户ID查询角色
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        List<UserRole> GetUserRole(int userID);

        /// <summary>
        /// 添加用户角色表
        /// </summary>
        /// <param name="userID">用ID</param>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        bool AddUserRole(int userID, int roleID);

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
        /// 移除用户
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        bool RemoveUser(int userID);
    }
}
