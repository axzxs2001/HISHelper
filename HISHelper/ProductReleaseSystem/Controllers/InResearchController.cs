using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductReleaseSystem.Models.IRepository;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductReleaseSystem.Controllers
{
    public class InResearchController : Controller
    {
        //定义接口变量
        IResearch _IResearch;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_inResearch"></param>
        public InResearchController(IResearch _inResearch)
        {
            _IResearch = _inResearch;


        }

        #region 在研项目主页
        /// <summary>
        /// 在研项目主页
        /// </summary>
        /// <returns></returns>
        [HttpGet("inresearch")]
        public IActionResult InResearch()
        {
            return View();
        }
        #endregion

        #region 在研项目查看主页
        /// <summary>
        /// 在研项目查看主页
        /// </summary>
        /// <returns></returns>
        [HttpGet("inresearchdown")]
        public IActionResult InResearchDownLoad()
        {
            return View();
        }
        #endregion

        #region 查询部门信息
        /// <summary>
        /// 查询部门信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("selectdepartments")]
        public IActionResult GetDepartments()
        {
            try
            {
                var list = _IResearch.SelectDepartments();
                return new JsonResult(new { result = 1, message = $"查询全部部门成功", data = list }, new JsonSerializerSettings()
                {
                    ContractResolver = new LowercaseContractResolver()
                });
            }
            catch (Exception exc)
            {
                // _log.Log(NLog.LogLevel.Error, $"查询全部部门：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"查询部门失败：{exc.Message}" });
            }
        }
        #endregion

        #region 根据部门查询开发人员
        [HttpPost("selectdevelopers")]
        public IActionResult SelectDevelopers(int departmentID)
        {
            try
            {
                var list = _IResearch.SelectDevelopers(departmentID);
                return new JsonResult(new { result = 1, message = $"查询全部部门成功", data = list }, new JsonSerializerSettings()
                {
                    ContractResolver = new LowercaseContractResolver()
                });
            }
            catch (Exception exc)
            {
                // _log.Log(NLog.LogLevel.Error, $"查询全部部门：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"查询部门失败：{exc.Message}" });
            }
        }
        #endregion

        #region 根据人员ID查询人员信息
        [HttpPost("developersid")]
        public IActionResult Developers(int Id)
        {
            try
            {
                var list = _IResearch.SelectDevelopers(Id);
                return new JsonResult(new { result = 1, message = $"查询全部部门成功", data = list }, new JsonSerializerSettings()
                {
                    ContractResolver = new LowercaseContractResolver()
                });
            }
            catch (Exception exc)
            {
                // _log.Log(NLog.LogLevel.Error, $"查询全部部门：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"查询部门失败：{exc.Message}" });
            }
        }
        #endregion

        


    }
}
