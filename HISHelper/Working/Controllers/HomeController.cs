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
        /// 构造
        /// </summary>
        /// <param name="workItemResitory">工作记录仓储</param>
        /// <param name="userResitory">用户仓储</param>
        public HomeController(IWorkItemResitory workItemResitory, IUserResitory userResitory)
        {
            _workItemResitory = workItemResitory;
            _userResitory = userResitory;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #region 登录页
        /// <summary>
        /// 登录页
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns> 
        [AllowAnonymous]
        [Route("login")]
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
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.UserData,user.UserName),
                    new Claim(ClaimTypes.Role,"admin"),
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Sid,user.ID.ToString())
                 };
                HttpContext.SignOutAsync("loginvalidate");
                HttpContext.SignInAsync("loginvalidate", new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookie")));
                HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
                return new RedirectResult(returnUrl == null ? "/myactives" : returnUrl);
            }
            else
            {
                ViewBag.error = "用户名或密码错误！";
                return View();
            }
        }

        #endregion
        [HttpGet("myworks")]
        public IActionResult MyWorks()
        {
            return View();
        }
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
        [HttpPost("addwork")]
        public IActionResult AddWork(WorkItem workItem)
        {
            try
            {
                if (workItem.ID == 0)
                {
                    workItem.CreateTime = DateTime.Now;
                    workItem.CreateUserID = UserID;
                    var result = _workItemResitory.AddWorkItem(workItem);
                    return Json(new { result = 1, message = "编辑成功", data = result }, new JsonSerializerSettings());

                }
                else
                {

                    var result = _workItemResitory.ModifyWorkItem(workItem);
                    return Json(new { result = 1, message = "编辑成功", data = result });
                }
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
                    if (user.IsDeparmentLeader)
                    {
                        var users = _userResitory.GetGetDepartmentUsers(user.DepartmentID);
                        return Json(new { result = 1, data = users, message = $"查询成功！" }, new JsonSerializerSettings()
                        {
                            DateFormatString = "yyyy年MM月dd日",
                            ContractResolver = new LowercaseContractResolver()
                        });
                    }
                    else
                    {
                        return Json(new { result = 0, message = $"你不是当前部分的负责人，没有权限查询其他人工作记录！" }, new JsonSerializerSettings());
                    }
                }else
                {
                    return Json(new { result = 0, message = $"查询失败:按{UserID}查询不到用户" }, new JsonSerializerSettings());
                }
            }
            catch (Exception exc)
            {
                return Json(new { result = 0, message = $"查询失败:{exc.Message}" }, new JsonSerializerSettings());
            }
        }



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
    }
}
