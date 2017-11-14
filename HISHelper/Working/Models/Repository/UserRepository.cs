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
    /// 用户管理
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        WorkingDbContext _dbContext;
        public UserRepository(WorkingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public User Login(string userName, string password)
        {
            return _dbContext.Users.SingleOrDefault(s => s.UserName == userName && s.Password == password);
        }
        /// <summary>
        /// 获取本部门用户
        /// </summary>
        /// <param name="departmentID">部门编号</param>
        /// <returns></returns>
        public List<User> GetDepartmentUsers(int departmentID)
        {
            return _dbContext.Users.Where(s => s.DepartmentID == departmentID).OrderBy(o => o.ID).ToList();
        }
        /// <summary>
        /// 查询部门下的用户
        /// </summary>
        /// <param name="departmentID">部门编号</param>
        /// <returns></returns>
        public dynamic QueryDepartmentUsers(int departmentID)
        {
            return _dbContext.Users.Where(s => s.DepartmentID == departmentID).Include(user => user.Role).Select(user => new { user.ID, user.Name, user.UserName, user.Role.RoleName, user.RoleID }).OrderBy(o => o.ID).ToList();
        }
        /// <summary>
        /// 按照ID获取用户
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public User GetUser(int userID)
        {
            return _dbContext.Users.SingleOrDefault(s => s.ID == userID);
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public bool AddUser(User user)
        {
            _dbContext.Users.Add(user);
            return _dbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public bool ModifyUser(User user)
        {
            var oldUser = _dbContext.Users.SingleOrDefault(s => s.ID == user.ID);
            if (oldUser != null)
            {
                oldUser.RoleID = user.RoleID;
                oldUser.Name = user.Name;
                oldUser.UserName = user.UserName;
                return _dbContext.SaveChanges() > 0;
            }
            return false;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public bool RemoveUser(int userID)
        {
            var oldUser = _dbContext.Users.SingleOrDefault(s => s.ID == userID);
            if (oldUser != null)
            {
                _dbContext.Users.Remove(oldUser);
                return _dbContext.SaveChanges() > 0;
            }
            return false;
        }

        /// <summary>
        /// 按用户ID获取此人所在部门的所有下级部门
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public dynamic GetDeparments(int userID)
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
        dynamic GetMyDeparments(int departmentID, List<Department> allDeparments)
        {
            var departments = new List<dynamic>();
            foreach (var department in allDeparments.Where(s => s.PDepartmentID == departmentID).Select(s=>new {s.ID,s.DepartmentName }))
            {
                departments.Add(department);
                departments.AddRange(GetMyDeparments(department.ID, allDeparments));
            }
            return departments;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="newPassword">新密码</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public bool ModifyPassword(string newPassword,int userID)
        {
            var user = _dbContext.Users.SingleOrDefault(s => s.ID == userID);
            if(user!=null)
            {
                user.Password = newPassword;
                return _dbContext.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }
    }
}
