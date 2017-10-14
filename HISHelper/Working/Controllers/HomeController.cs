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
        public IActionResult MyWorks( )
        {
            return View();
        }
        [HttpGet("monthworks")]
        public IActionResult GetWorksByMonth(int? year,int? month)
        {
            try
            {
                if(!year.HasValue)
                {
                    year = DateTime.Now.Year;
                }
                if(!month.HasValue)
                {
                    month = DateTime.Now.Month;
                }
                var workItems = _workItemResitory.GetWorkItemsByUserID(UserID,year.Value,month.Value);
                return Json(new { result = 1, message = "查询成功", data = workItems }, new JsonSerializerSettings());
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
                var result = _workItemResitory.AddWorkItem(workItem);
                return Json(new { result = 1, message = "查询成功", data = result }, new JsonSerializerSettings());
            }
            catch (Exception exc)
            {
                return Json(new { result = 0, message = $"查询失败:{exc.Message}" }, new JsonSerializerSettings());
            }          
        }
    }
}
