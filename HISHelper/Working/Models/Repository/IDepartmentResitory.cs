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
        /// 获取全部部门
        /// </summary>
        /// <returns></returns>
        dynamic GetAllDeparments();
        /// <summary>
        /// 获取全部门，包括公司
        /// </summary>
        /// <returns></returns>
        List<Department> GetAllPDepartment();
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        bool AddDepartment(Department department);
        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        bool ModifyDepartment(Department department);
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departmentID">部门</param>
        /// <returns></returns>
        bool RemoveDepartment(int departmentID);
    }
}
