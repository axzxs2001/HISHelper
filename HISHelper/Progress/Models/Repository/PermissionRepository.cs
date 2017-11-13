using Microsoft.EntityFrameworkCore;
using Progress.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Progress.Models.Repository
{
    public class PermissionRepository : IPermissionRepository
    {
        string _permissionConnectionString;
        public PermissionRepository(IConfiguration configuration)
        {
            _permissionConnectionString = configuration.GetConnectionString("PermissionConnectionString");
        }
        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <returns></returns>
        public List<AuthorizePermission> GetRolePermissions()
        {
            using (var con = new SqlConnection(_permissionConnectionString))
            {
                return con.Query<AuthorizePermission>($@"select RoleName,Method,Action 
                from  dbo.Permissions INNER JOIN
                dbo.RolePermissions ON dbo.Permissions.ID =dbo.RolePermissions.PermissionID INNER JOIN
                dbo.Roles ON dbo.RolePermissions.RoleID = dbo.Roles.ID").ToList();
            }
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="permission">权限</param>
        /// <returns></returns>
        public bool AddPermission(Permission permission)
        {
            using (var con = new SqlConnection(_permissionConnectionString))
            {
                return con.Execute($@"insert into permissions(permissionname,action,method,pid,memo) values(@permissionname,@action,@method,@pid,@memo)"，new { permissionname=permission.PermissionName, action=permission.Action, method=permission.Memo, pid=permission.Pid, memo=permission.Memo })>0;
            }
        }
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="permission">权限</param>
        /// <returns></returns>
        public bool ModifyPermission(Permission permission)
        {
            using (var con = new SqlConnection(_permissionConnectionString))
            {
                return con.Execute($@"update permissions set permissionname=@permissionname,action=@action,method=@method,pid=@pid,memo=@memo where id=@id",new {id=permission.ID, permissionname = permission.PermissionName, action = permission.Action, method = permission.Memo, pid = permission.Pid, memo = permission.Memo }) > 0;
            }
        }
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="permission">权限</param>
        /// <returns></returns>
        public bool RemovePermission(int permissionID)
        {
            using (var con = new SqlConnection(_permissionConnectionString))
            {
                return con.Execute($@"delete permissions where id=@id", new { id = permissionID}) > 0;
            }
        }
    }
}
