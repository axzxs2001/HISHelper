using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Working.Model;
using Working.Models.DataModel;

namespace Working.Model.Repository
{
    /// <summary>
    /// 部门管理接口
    /// </summary>
    public class DepartmentResitory: IDepartmentResitory
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        WorkingDbContext _dbContext;
        public DepartmentResitory(WorkingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 获取全部部门
        /// </summary>
        /// <returns></returns>
        public dynamic GetAllDeparments()
        {           
            return _dbContext.Departments.Include(department => department.PDepartment).Select(department => new { department.ID, department.DepartmentName,pdepartmentid= department.PDepartmentID, PDepartmentName=department.PDepartment.DepartmentName }).OrderBy(o => o.ID).ToList();
        }
        /// <summary>
        /// 获取全部门，包括公司
        /// </summary>
        /// <returns></returns>
        public List<Department> GetAllPDepartment()
        {
            return _dbContext.Departments.ToList();
        }
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public bool AddDepartment(Department department)
        {
            _dbContext.Departments.Add(department);
            return _dbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public bool ModifyDepartment(Department department)
        {
            var oldDepartment = _dbContext.Departments.SingleOrDefault(s => s.ID == department.ID);
            if (oldDepartment != null)
            {
                oldDepartment.PDepartmentID = department.PDepartmentID;
                oldDepartment.DepartmentName = department.DepartmentName;           
                return _dbContext.SaveChanges() > 0;
            }
            return false;
        }
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departmentID">部门</param>
        /// <returns></returns>
        public bool RemoveDepartment(int departmentID)
        {
            var oldDepartment = _dbContext.Departments.SingleOrDefault(s => s.ID == departmentID);
            if (oldDepartment != null)
            {
                _dbContext.Departments.Remove(oldDepartment);
                return _dbContext.SaveChanges() > 0;
            }
            return false;
        }
    }
}
