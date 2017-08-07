using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductReleaseSystem.Models.IRepository;
using ProductReleaseSystem.ProductRelease;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductReleaseSystem.Controllers
{
    public class DemandController : Controller
    {
        IDemand _demand;
        public DemandController(IDemand idemand)
        {
            _demand = idemand;
        }
        /// <summary>
        /// 需求提交页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("demand")]
        // GET: /<controller>/
        public IActionResult Demand()
        {
            return View();
        }
        #region 实施添加需求
        /// <summary>
        /// 实施添加需求
        /// </summary>
        /// <param name="requestform"></param>
        /// <returns></returns>
        [HttpPost("adddemand")] 
        public IActionResult AddDemand(RequestForm requestform)
        {
            try
            {
                _demand.InsertDemand(requestform);
                return new JsonResult(new { result = 1, message = $"添加成功！" });
            }
            catch (Exception exc)
            {
                //_log.Log(NLog.LogLevel.Error, $"删除菜单：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"添加失败！：{exc.Message}" });

            }
        }
        #endregion
        /// <summary>
        /// 部门添加
        /// </summary>
        /// <returns></returns>
        [HttpPost("departments")]
        public IActionResult Departments()
        {
            try
            {
                var list = _demand.SelectDepartments();
                return new JsonResult(new { result = 1, message = $"查询开发人员成功！", data = list }, new JsonSerializerSettings()
                {
                    ContractResolver = new LowercaseContractResolver()
                });
            }
            catch (Exception exc)
            {
                // _log.Log(NLog.LogLevel.Error, $"查询全部部门：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"查询开发人员失败！：{exc.Message}" });
            }
        }
    }
}
