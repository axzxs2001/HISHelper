using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Working.Model;
using Working.Models.DataModel;

namespace Working.Model.Repository
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleResitory : IRoleResitory
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        WorkingDbContext _dbContext;
        public RoleResitory(WorkingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 查询全部角色
        /// </summary>
        /// <returns></returns>
        public List<Role> GetAllRole()
        {
            return _dbContext.Roles.ToList();
        }
        /// <summary>
        /// 查询角色
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        public Role GetRole(int roleID)
        {
            return _dbContext.Roles.SingleOrDefault(s => s.ID == roleID);
        }
    }
}
