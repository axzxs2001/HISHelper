using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Working.Model;
using Working.Models.DataModel;

namespace Working.Model.Repository
{
    /// <summary>
    /// 角色管理接口
    /// </summary>
    public interface IRoleResitory
    {
        /// <summary>
        /// 查询全部角色
        /// </summary>
        /// <returns></returns>
        List<Role> GetAllRole();
        /// <summary>
        /// 查询角色
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        Role GetRole(int roleID);
    }
}
