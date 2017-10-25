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
        /// 按用户ID获取此人所在部门的所有下级部门
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public List<Department> GetDeparments(int userID)
        {
            var user = _dbContext.Users.SingleOrDefault(s => s.ID == userID);
            if (user != null)
            {
                return GetMyDeparments(user.DepartmentID, _dbContext.Departments.ToList());
            }
            else
            {
                return new List<Department>();
            }
        }
        /// <summary>
        /// 递归获取所有部门
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        List<Department> GetMyDeparments(int departmentID, List<Department> allDeparments)
        {
            var departments = new List<Department>();
            foreach (var department in allDeparments.Where(s => s.PID == departmentID))
            {
                departments.Add(department);
                departments.AddRange(GetMyDeparments(department.ID, allDeparments));
            }
            return departments;
        }
        /// <summary>
        /// 获取全部部门
        /// </summary>
        /// <returns></returns>
        public List<Department> GetAllDeparments()
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
                oldDepartment.PID = department.PID;
                oldDepartment.DepartMentName = department.DepartMentName;           
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
