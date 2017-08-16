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
                if (list.Count == 0)
                {
                    return new JsonResult(new { result = 1, message = $"查询成功,没有需求！" });
                }
                else 
                {
                return new JsonResult(new { result = 1, message = "查询成功", data = list }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
                }
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
                ViewData["count"] = count;
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
    }
}
