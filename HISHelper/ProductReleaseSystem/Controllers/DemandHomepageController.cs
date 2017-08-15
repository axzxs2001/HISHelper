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
        #region 查看需求页面
        /// <summary>
        /// 查看需求页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("demandhomepage")]
        public IActionResult DemandHomepage()
        {
            return View();
        }
        #endregion
        #region 查询所有产品
        /// <summary>
        /// 查询所有产品
        /// </summary>
        /// <returns></returns>
        [HttpGet("queryproducts")]
        public IActionResult QueryProducts()
        {
            try
            {
                var list = _idemand.QueryProducts();
                return new JsonResult(new { result = 1, message = "查询成功", data = list });
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
                return new JsonResult(new { result = 1, message = "查询成功", data = list });
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
    }
}
