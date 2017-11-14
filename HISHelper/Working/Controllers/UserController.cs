using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Working.Model.Repository;
using Newtonsoft.Json;
using Working.Models;
using Working.Models.DataModel;

namespace Working.Controllers
{

    public class UserController : BaseController
    {

        /// <summary>
        /// 用户仓储
        /// </summary>
        IUserRepository _userResitory; 
        /// <summary>
        /// 角色仓储
        /// </summary>
        IRoleRepository _roleResitory;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="roleResitory">角色仓储</param>
        /// <param name="userResitory">用户仓储</param>
        public UserController(IUserRepository userResitory, IRoleRepository roleResitory)
        {
            _userResitory = userResitory;
            _roleResitory = roleResitory;
        }

        #region 用户操作
        [Authorize(Roles = "Manager")]
        [HttpGet("users")]
        public IActionResult Users()
        {
            return View();
        }
        /// <summary>
        /// 查询部门下的用户
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [HttpGet("userroles")]
        public IActionResult GetRoleUserByDepartmentID(int departmentID)
        {
            try
            {
                var departments = _userResitory.QueryDepartmentUsers(departmentID);
                return Json(new
                {
                    result = 1,
                    message = "查询成功",
                    data = departments
                }, new JsonSerializerSettings()
                {
                    DateFormatString = "yyyy年MM月dd日",
                    ContractResolver = new LowercaseContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                });
            }
            catch (Exception exc)
            {
                return Json(new { result = 0, message = $"查询失败:{exc.Message}" }, new JsonSerializerSettings());
            }
        }
        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [HttpGet("roles")]
        public IActionResult GetAllRole()
        {
            try
            {
                var roles = _roleResitory.GetAllRole();
                return Json(new
                {
                    result = 1,
                    message = "查询成功",
                    data = roles
                }, new JsonSerializerSettings()
                {
                    DateFormatString = "yyyy年MM月dd日",
                    ContractResolver = new LowercaseContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                });
            }
            catch (Exception exc)
            {
                return Json(new { result = 0, message = $"查询失败:{exc.Message}" }, new JsonSerializerSettings());
            }
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [HttpPost("adduser")]
        public IActionResult AddUser(User user)
        {
            try
            {
                user.Password = user.UserName;
                var result = _userResitory.AddUser(user);
                if (result)
                {
                    return Json(new
                    {
                        result = 1,
                        message = "添加成功",
                        data = true
                    }, new JsonSerializerSettings()
                   );
                }
                else
                {
                    return Json(new
                    {
                        result = 0,
                        message = "添加失败",
                        data = false
                    }, new JsonSerializerSettings()
              );
                }
            }
            catch (Exception exc)
            {
                return Json(new { result = 0, message = $"添加失败:{exc.Message}" }, new JsonSerializerSettings());
            }
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [HttpPut("modifyuser")]
        public IActionResult ModifyUser(User user)
        {
            try
            {                
                var result = _userResitory.ModifyUser(user);
                if (result)
                {
                    return Json(new
                    {
                        result = 1,
                        message = "修改成功",
                        data = true
                    }, new JsonSerializerSettings()
                   );
                }
                else
                {
                    return Json(new
                    {
                        result = 0,
                        message = "修改失败",
                        data = false
                    }, new JsonSerializerSettings()
              );
                }
            }
            catch (Exception exc)
            {
                return Json(new { result = 0, message = $"修改失败:{exc.Message}" }, new JsonSerializerSettings());
            }
        }
        /// <summary>
        /// 移除用户
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [HttpDelete("removeuser")]
        public IActionResult RemoveUser(int userID)
        {
            try
            {
                var result = _userResitory.RemoveUser(userID);
                if (result)
                {
                    return Json(new
                    {
                        result = 1,
                        message = "删除成功",
                        data = true
                    }, new JsonSerializerSettings()
                   );
                }
                else
                {
                    return Json(new
                    {
                        result = 0,
                        message = "删除失败",
                        data = false
                    }, new JsonSerializerSettings()
              );
                }
            }
            catch (Exception exc)
            {
                return Json(new { result = 0, message = $"删除失败:{exc.Message}" }, new JsonSerializerSettings());
            }
        }

        /// <summary>
        /// 查询部门用户
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Manager,Leader")]
        [HttpGet("getdepartmentusers")]
        public IActionResult DepartmentUsers()
        {
            try
            {
                var user = _userResitory.GetUser(UserID);
                if (user != null)
                {
                    var users = _userResitory.GetDepartmentUsers(user.DepartmentID);
                    return Json(new { result = 1, data = users, message = $"查询成功！" }, new JsonSerializerSettings()
                    {
                        DateFormatString = "yyyy年MM月dd日",
                        ContractResolver = new LowercaseContractResolver()
                    });
                }
                else
                {
                    return Json(new { result = 0, message = $"查询失败:按{UserID}查询不到用户" }, new JsonSerializerSettings());
                }
            }
            catch (Exception exc)
            {
                return Json(new { result = 0, message = $"查询失败:{exc.Message}" }, new JsonSerializerSettings());
            }
        }

        /// <summary>
        /// 按照部门ID查本部门用户
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        [Authorize(Roles = "Manager,Leader")]
        [HttpGet("departmentusers")]
        public IActionResult DepartmentUsers(int departmentID)
        {
            try
            {
                var users = _userResitory.GetDepartmentUsers(departmentID);
                return Json(new { result = 1, data = users, message = $"查询成功！" }, new JsonSerializerSettings()
                {
                    DateFormatString = "yyyy年MM月dd日",
                    ContractResolver = new LowercaseContractResolver()
                });
            }
            catch (Exception exc)
            {
                return Json(new { result = 0, message = $"查询失败:{exc.Message}" }, new JsonSerializerSettings());
            }
        }
        #endregion
    }
}