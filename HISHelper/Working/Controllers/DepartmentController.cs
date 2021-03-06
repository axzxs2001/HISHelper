﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Working.Models;
using Microsoft.AspNetCore.Authorization;
using Working.Model.Repository;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Newtonsoft.Json;
using Working.Models.DataModel;

namespace Working.Controllers
{
    [Authorize(Roles = "Manager")]
    public class DepartmentController : BaseController
    {
  
        /// <summary>
        /// 用户仓储
        /// </summary>
        IUserRepository _userResitory;
        /// <summary>
        /// 部门仓储
        /// </summary>
        IDepartmentRepository _departmentResitory;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="workItemResitory">工作记录仓储</param>
        /// <param name="departmentResitory">部门仓储</param>
        /// <param name="userResitory">用户仓储</param>
        /// <param name="roleResitory">角色仓储</param>
        public DepartmentController(IDepartmentRepository departmentResitory, IUserRepository userResitory, IRoleRepository roleResitory)
        {        
            _userResitory = userResitory;
            _departmentResitory = departmentResitory;     
        }

        #region 部门操作 
        [HttpGet("departments")]
        public IActionResult Departments()
        {
            return View();
        }

        /// <summary>
        /// 查询用户部门的所有下属部门
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Manager,Leader")]
        [HttpGet("mydepartments")]
        public IActionResult GetMyDepartments()
        {
            try
            {
                var departments = _userResitory.GetDeparments(UserID);
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


        /// <summary>
        /// 查询所有部门
        /// </summary>
        /// <returns></returns>
        [HttpGet("alldepartments")]
        public IActionResult GetAllDepartments()
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
                    ContractResolver = new LowercaseContractResolver()
                });
            }
            catch (Exception exc)
            {
                return Json(new { result = 0, message = $"查询失败:{exc.Message}" }, new JsonSerializerSettings());
            }
        }
        /// <summary>
        /// 查询所有父部门
        /// </summary>
        /// <returns></returns>
        [HttpGet("allpdepartments")]
        public IActionResult GetAllPDepartments()
        {
            try
            {
                var departments = _departmentResitory.GetAllPDepartment();
                return Json(new
                {
                    result = 1,
                    message = "查询成功",
                    data = departments
                }, new JsonSerializerSettings()
                {
                    ContractResolver = new LowercaseContractResolver()
                });
            }
            catch (Exception exc)
            {
                return Json(new { result = 0, message = $"查询失败:{exc.Message}" }, new JsonSerializerSettings());
            }
        }
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        [HttpPost("adddepartment")]
        public IActionResult AddDepartment(Department department)
        {
            try
            {
                var result = _departmentResitory.AddDepartment(department);
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
        /// 获取除公司外的所有部门
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        [HttpPut("modifydepartment")]
        public IActionResult ModifyDepartment(Department department)
        {
            try
            {
                var result = _departmentResitory.ModifyDepartment(department);
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
        /// 删除部门
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        [HttpDelete("removedepartment")]
        public IActionResult RemoveUser(int departmentID)
        {
            try
            {
                var result = _departmentResitory.RemoveDepartment(departmentID);
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
