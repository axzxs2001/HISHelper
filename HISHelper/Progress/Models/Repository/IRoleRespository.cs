using Microsoft.Extensions.Configuration;
using Progress.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;

namespace Progress.Models.Repository
{
    /// <summary>
    /// 角色仓储接口
    /// </summary>
    public interface IRoleRespository
    {
        /// <summary>
        /// 添加角色要权限表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="permissionID">权限ID</param>
        /// <returns></returns>
        bool AddRolePermission(int roleID, int permissionID);

        /// <summary>
        /// 获取全部角色权限
        /// </summary>
        /// <returns></returns>
        List<RolePermission> GetRolePermissions();
        /// <summary>
        /// 根据角色ID获取角色权限
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        List<RolePermission> GetRolePermissionsByRoleID(int roleID);
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        bool AddRole(string roleName);

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="role">角色</param>
        /// <returns></returns>
         bool ModifyRole(Role role);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        bool RemoveRole(int roleID);

    }
}
