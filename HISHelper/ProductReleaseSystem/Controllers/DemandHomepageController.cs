using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductReleaseSystem.Models.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductReleaseSystem.Controllers
{
    public class DemandHomepageController : Controller
    {
        IDemand _idemand;
        // GET: /<controller>/
        public DemandHomepageController(IDemand idemand)
        {
            _idemand = idemand;
        }
        /// <summary>
        /// 产品需求主页
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("productdemand")]
        public IActionResult ProductDemand()
        {
            return View();
        }
        /// <summary>
        /// 产品需求查看主页
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("viewrequirements")]
        public IActionResult ViewRequirements()
        {
            return View();
        }
        #region 查询所有产品
        /// <summary>
        /// 查询所有产品
        /// </summary>
        /// <returns></returns>
        [HttpGet("queryselectproducts")]
        public IActionResult QueryProducts()
        {
            try
            {
                var list = _idemand.QueryProducts();
                return new JsonResult(new { result = 1, data = list, message = "查询成功"},new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败:{exc.Message}"});
            }
        }
        #endregion
        #region 根据产品ID查询需求信息
        /// <summary>
        /// 根据产品ID查询需求信息
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("queryrequestbyproductid")]
        public IActionResult QueryRequestByProductId(int id)
        {
            try
            {
                var list = _idemand.QueryRequestByProductId(id);
                return new JsonResult(new { result = 1, message = "查询成功", data = list }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            } 
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion

        #region 根据产品ID查询需求条数
        /// <summary>
        /// 根据产品ID查询需求条数
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("queryrequestcount")]
        public IActionResult QueryRequestCount(int id)
        {
            try
            {
                var count = _idemand.QueryRequestCount(id);
                return new JsonResult(new { result = 1, message = "查询成功", data = count });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion

        #region 查看详细需求
        /// <summary>
        /// 查看详细需求
        /// </summary>
        /// <param name="id">需求ID</param>
        /// <returns></returns>
        [HttpPost("querydetailedrequirements")]
        public IActionResult QueryDetailedRequirements(int id)
        {
            try
            {
                var list = _idemand.QueryDetailedRequirements(id);
                return new JsonResult(new { result = 1, data = list, message = "查询成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            } catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败:{exc.Message}" });
            }
        }
        #endregion


        #region 我发布的页面

        #region 通过姓名用户查询ID
        /// <summary>
        /// 通过姓名用户查询ID
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost("selectchaxun")]
        public IActionResult SelectUsers(string name)
        {
            try

            {
                var list = _idemand.InsertUsers(name);
                return new JsonResult(new { result = 1, message = $"查询用户成功！", data = list }, new JsonSerializerSettings()
                {
                    ContractResolver = new LowercaseContractResolver()
                });
            }
            catch (Exception exc)
            {
                // _log.Log(NLog.LogLevel.Error, $"查询全部部门：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"查询用户失败！：{exc.Message}" });
            };
        }
        #endregion

        #region 通过姓名ID查询产品需求
        /// <summary>
        /// 通过姓名ID查询产品需求
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost("selectdemand")]
        public IActionResult SelectDemand(int id)
        {
            try

            {
                var list = _idemand.SelectDemand(id);
                return new JsonResult(new { result = 1, message = $"查询用户成功！", data = list }, new JsonSerializerSettings()
                {
                    ContractResolver = new LowercaseContractResolver()
                });
            }
            catch (Exception exc)
            {
                // _log.Log(NLog.LogLevel.Error, $"查询全部部门：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"查询用户失败！：{exc.Message}" });
            };
        }
        #endregion

        #region 根据产品ID查询需求信息(我发布的)
        /// <summary>
        /// 根据产品ID查询需求信息(我发布的)
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("queryfbproductid")]
        public IActionResult QueryfbProductId(int id,int nameid)
        {
            try
            {
                var list = _idemand.QueryfbProductId(id, nameid);
                return new JsonResult(new { result = 1, message = "查询成功", data = list }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion

        #region 根据产品ID查询需求条数(我发布的)
        /// <summary>
        /// 根据产品ID查询需求条数(我发布的)
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("queryfbcount")]
        public IActionResult QueryfbCount(int id,int nameid)
        {
            try
            {
                var count = _idemand.QueryfbCount(id,nameid);
                return new JsonResult(new { result = 1, message = "查询成功", data = count });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion
        #endregion


        #region  已完成的页面
        #region 查询已完成的所有产品
        /// <summary>
        /// 查询已完成的所有产品
        /// </summary>
        /// <returns></returns>
        [HttpGet("carryoutproducts")]
        public IActionResult CarryOutProducts()
        {
            try
            {
                var list = _idemand.CarryOutProducts();
                return new JsonResult(new { result = 1, data = list, message = "查询成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败:{exc.Message}" });
            }
        }
        #endregion
        #region 根据产品ID查询已完成需求信息
        /// <summary>
        /// 根据产品ID查询已完成需求信息
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("querycarryoutproductid")]
        public IActionResult QueryCarryOutProductId(int id)
        {
            try
            {
                var list = _idemand.QueryCarryOutProductId(id);
                return new JsonResult(new { result = 1, message = "查询成功", data = list }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion
        #region 根据产品ID查询已完成需求条数
        /// <summary>
        /// 根据产品ID查询需求条数
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("querycarryoutcount")]
        public IActionResult QueryCarryOutCount(int id)
        {
            try
            {
                var count = _idemand.QueryCarryOutCount(id);
                return new JsonResult(new { result = 1, message = "查询成功", data = count });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion
        #endregion
    }
}
