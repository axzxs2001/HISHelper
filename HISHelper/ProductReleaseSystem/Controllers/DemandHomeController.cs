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
using ProductReleaseSystem.ProductRelease;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductReleaseSystem.Controllers
{
    public class DemandHomeController : Controller
    {
        IDemandHome _demandHome;
        public DemandHomeController(IDemandHome idemandHome)
        {
            _demandHome = idemandHome;
        }
        // GET: /<controller>/
        [AllowAnonymous]
        [HttpGet("demandhome")]
        public IActionResult DemandHome()
        {
            return View();
        }
        #region 查询全部
        #region 查询产品
        /// <summary>
        /// 查询产品
        /// </summary>
        /// <returns></returns>
        [HttpGet("queryallproducts")]
        public IActionResult QueryAllProducts()
        {
            try
            {
                var list = _demandHome.QueryAllProducts();
                return new JsonResult(new { result = 1, data = list, message = "查询成功" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion
        #region 查询需求
        /// <summary>
        /// 根据产品ID查询需求
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("queryallrequestforms")]
        public IActionResult QueryAllRequestForms(int id)
        {
            try
            {
                var list = _demandHome.QueryAllRequestForms(id);
                return new JsonResult(new { result = 1, data = list, message = "查询成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }catch(Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion
        #region 查询条数
        /// <summary>
        /// 查询条数
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("queryrequestscount")]
        public IActionResult QueryRequestsCount(int id)
        {
            try {
                var count = _demandHome.QueryRequestsCount(id);
                return new JsonResult(new { result = 1, data = count, message = "查询成功" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败:{exc.Message}" });
            }
         }
        #endregion
        #endregion

        #region 与我相关
        #region 查询与我相关的产品
        /// <summary>
        /// 查询与我相关的产品
        /// </summary>
        /// <param name="id">人员id</param>
        /// <returns></returns>
        [HttpPost("queryandmeproduct")]
        public IActionResult QueryAndMeProduct(int id)
        {
            try
            {
                var list = _demandHome.QueryAndMeProduct(id);
                return new JsonResult(new { result = 1, data = list, message = "查询成功" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败:{exc.Message}" });
            }
        }
        #endregion
        #region 根据产品id查询与我相关的需求
        /// <summary>
        /// 根据产品id查询与我相关的需求
        /// </summary>
        /// <param name="id">人员id</param>
        /// <param name="pid">产品id</param>
        /// <returns></returns>
        [HttpPost("queryandmerequest")]
        public IActionResult QueryAndMeRequest(int id,int pid)
        {
            try
            {
                var list = _demandHome.QueryAndMeRequest(id, pid);
                return new JsonResult(new { result = 1, data = list, message = "查询成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch(Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion
        #region 查询与我相关的产品的需求数
        /// <summary>
        /// 查询与我相关的产品的需求数
        /// </summary>
        /// <param name="id">人员id</param>
        /// <param name="pid">产品id</param>
        /// <returns></returns>
        [HttpPost("queryandmecount")]
        public IActionResult QueryAndMeCount(int id, int pid)
        {
            try
            {
                var count = _demandHome.QueryAndMeCount(id, pid);
                return new JsonResult(new { result = 1, data = count, message = "查询成功" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败:{exc.Message}" });
            }
        }
        #endregion
        #endregion

        #region 在研项目
        #region 查询在研产品
        /// <summary>
        /// 查询在研产品
        /// </summary>
        /// <returns></returns>
        [HttpGet("queryzyproduct")]
        public IActionResult QueryZyProduct()
        {
            try
            {
                var list = _demandHome.QueryZyProduct();
                return new JsonResult(new { result = 1, data = list, message = "查询成功" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion
        #region 根据产品ID查询在研需求
        /// <summary>
        /// 根据产品ID查询在研需求
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("queryzyrequestform")]
        public IActionResult QueryZyRequestForm(int id)
        {
            try
            {
                var list = _demandHome.QueryZyRequestForm(id);
                return new JsonResult(new { result = 1, data = list, message = "查询成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion
        #region 根据产品ID查询在研项目需求条数
        /// <summary>
        /// 根据产品ID查询在研项目需求条数
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("queryzycount")]
        public IActionResult QueryZyCount(int id)
        {
            try
            {
                var count = _demandHome.QueryZyCount(id);
                return new JsonResult(new { result = 1, data = count, message = "查询成功" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion
        #endregion

        #region 已完成的
        #region 查询已完成的产品
        /// <summary>
        /// 查询已完成的产品
        /// </summary>
        /// <returns></returns>
        [HttpGet("queryfinishproduct")]
        public IActionResult QueryFinishProduct()
        {
            try
            {
                var list = _demandHome.QueryFinishProduct();
                return new JsonResult(new { result = 1, data = list, message = "查询成功" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败:{exc.Message}" });
            }
        }
        #endregion
        #region 根据产品ID查询已完成的需求
        /// <summary>
        /// 根据产品ID查询已完成的需求
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("queryfinishrequestform")]
        public IActionResult QueryFinishRequestForm(int id)
        {
            try
            {
                var list = _demandHome.QueryFinishRequestForm(id);
                return new JsonResult(new { result = 1, data = list, message = "查询成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion
        #region 根据产品ID查询已完成的需求条数
        /// <summary>
        /// 根据产品ID查询已完成的需求条数
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("querycountfinish")]
        public IActionResult QueryCountFinish(int id)
        {
            try
            {
                var count = _demandHome.QueryCountFinish(id);
                return new JsonResult(new { result = 1, data = count, message = "查询成功" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败:{exc.Message}" });
            }
        }
        #endregion
        #endregion

        #region 未开始的
        #region 查询未开始的需求的产品
        /// <summary>
        /// 查询未开始的需求的产品
        /// </summary>
        /// <returns></returns>
        [HttpGet("querynoproduct")]
        public IActionResult QueryNoProduct()
        {
            try
            {
                var list = _demandHome.QueryNoProduct();
                return new JsonResult(new { result = 1, data = list, message = "查询成功" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败:{exc.Message}" });
            }
        }
        #endregion
        #region 根据产品ID查询未开始的需求
        /// <summary>
        /// 根据产品ID查询未开始的需求
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("querynorequestform")]
        public IActionResult QueryNoRequestForm(int id)
        {
            try
            {
                var list = _demandHome.QueryNoRequestForm(id);
                return new JsonResult(new { result = 1, data = list, message = "查询成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion
        #region 根据产品ID查询未开始的需求条数
        /// <summary>
        /// 根据产品ID查询未开始的需求条数
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("querycountno")]
        public IActionResult QueryCountNo(int id)
        {
            try
            {
                var count = _demandHome.QueryCountNo(id);
                return new JsonResult(new { result = 1, data = count, message = "查询成功" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败:{exc.Message}" });
            }
        }
        #endregion
        #endregion
    }
}
