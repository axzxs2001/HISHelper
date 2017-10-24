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
    [Authorize(Roles = "Manager, Leader,Employee")]
    public class UserController : BaseController
    {

        /// <summary>
        /// 用户仓储
        /// </summary>
        IUserResitory _userResitory;
        /// <summary>
        /// 部门仓储
        /// </summary>
        IDepartmentResitory _departmentResitory;
        /// <summary>
        /// 角色仓储
        /// </summary>
        IRoleResitory _roleResitory;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="workItemResitory">工作记录仓储</param>
        /// <param name="userResitory">用户仓储</param>
        public UserController(IUserResitory userResitory, IDepartmentResitory departmentResitory, IRoleResitory roleResitory)
        {
            _departmentResitory = departmentResitory;
            _userResitory = userResitory;
            _roleResitory = roleResitory;
        }

        #region 用户操作

        [HttpGet("users")]
        public IActionResult UserIndex()
        {
            return View();
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("alldepartment")]
        public IActionResult GetAllDepartment()
        {
            try
            {
                var departments = _departmentResitory.GetAllDeparments();
                return Json(new
                {
                    result = 1,
                    message = "查询成功",
                    data = departments
                }, new JsonSerializerSettings()
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
        #endregion
    }
}