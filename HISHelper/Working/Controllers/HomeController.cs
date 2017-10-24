using System;
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
    [Authorize(Roles = "Manager, Leader,Employee")]
    public class HomeController : BaseController
    {
        /// <summary>
        /// 工作记录仓储
        /// </summary>
        IWorkItemResitory _workItemResitory;
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
        public HomeController(IWorkItemResitory workItemResitory, IDepartmentResitory departmentResitory, IUserResitory userResitory, IRoleResitory roleResitory)
        {
            _workItemResitory = workItemResitory;
            _userResitory = userResitory;
            _departmentResitory = departmentResitory;
            _roleResitory = roleResitory;
        }

        public IActionResult Index()
        {

            return View();
        }
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        #region 工作记录操作
        [HttpGet("myworks")]
        public IActionResult MyWorks()
        {
            return View();
        }
        /// <summary>
        /// 按年，月查询自己的工作记录
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        [HttpGet("monthworks")]
        public IActionResult GetWorksByMonth(int? year, int? month)
        {
            try
            {
                if (!year.HasValue)
                {
                    year = DateTime.Now.Year;
                }
                if (!month.HasValue)
                {
                    month = DateTime.Now.Month;
                }
                var workItems = _workItemResitory.GetWorkItemsByUserID(UserID, year.Value, month.Value);
                return Json(new
                {
                    result = 1,
                    message = "查询成功",
                    data = workItems
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
        /// 添加工作记录
        /// </summary>
        /// <param name="workItem">工作记录</param>
        /// <returns></returns>
        [HttpPost("addwork")]
        public IActionResult AddWork(WorkItem workItem)
        {
            try
            {
                var result = _workItemResitory.AddWorkItem(workItem, UserID);
                return Json(new { result = 1, message = "编辑成功", data = result }, new JsonSerializerSettings());
            }
            catch (Exception exc)
            {
                return Json(new { result = 0, message = $"编辑失败:{exc.Message}" }, new JsonSerializerSettings());
            }
        }

        [HttpGet("queryworks")]
        public IActionResult QueryWorks()
        {
            return View();
        }

        [HttpGet("querydepartmentworks")]
        public IActionResult QueryDepartmentWorks()
        {
            return View();
        }

        /// <summary>
        /// 查询部门用户
        /// </summary>
        /// <returns></returns>
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
        /// 按年，月，用户查询工作记录
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="userID">用户</param>
        /// <returns></returns>
        [HttpGet("queryuserworks")]
        public IActionResult QueryUserWorks(int? year, int? month, int userID = 0)
        {
            try
            {
                if (!year.HasValue)
                {
                    year = DateTime.Now.Year;
                }
                if (!month.HasValue)
                {
                    month = DateTime.Now.Month;
                }
                var workItems = _workItemResitory.GetWorkItemsByUserID(userID, year.Value, month.Value);
                return Json(new
                {
                    result = 1,
                    message = "查询成功",
                    data = workItems
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
                var departments = _departmentResitory.GetDeparments(UserID);
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


        #endregion




        #region 登录页
        /// <summary>
        /// 登录页
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns> 
        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            //判断是否验证
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                //把返回地址保存在前台的hide表单中
                ViewBag.returnUrl = returnUrl;
            }
            ViewBag.error = null;
            HttpContext.SignOutAsync("loginvalidate");
            return View();
        }
        /// <summary>
        /// 实现登录
        /// </summary>
        /// <param name="fname"></param>
        /// <param name="password"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(string userName, string password, string returnUrl)
        {
            //查询users
            var user = _userResitory.Login(userName, password);
            if (user != null)
            {
                //查询角色名称
                var roleName = _roleResitory.GetRole(user.RoleID).RoleName;
                //查看是否有下级部门
                var departments = _departmentResitory.GetDeparments(user.ID);
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.UserData,user.UserName),
                    new Claim(ClaimTypes.Role,roleName),
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Sid,(departments.Count>0).ToString()),
                    new Claim(ClaimTypes.PrimarySid,user.ID.ToString())
                 };
                HttpContext.SignOutAsync("loginvalidate");
                HttpContext.SignInAsync("loginvalidate", new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookie")));
                HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
                return new RedirectResult(returnUrl == null ? "/myworks" : returnUrl);
            }
            else
            {
                ViewBag.error = "用户名或密码错误！";
                return View();
            }
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("loginvalidate");
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
