using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Working.Models.DataModel;

namespace Working.Model.Repository
{
    /// <summary>
    /// 部门管理接口
    /// </summary>
    public interface IDepartmentResitory
    {
        /// <summary>
        /// 按用户ID获取此人所在部门的所有下级部门
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        List<Department> GetDeparments(int userID);
    }
}
