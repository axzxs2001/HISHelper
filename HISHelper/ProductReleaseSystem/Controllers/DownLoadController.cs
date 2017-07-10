using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductReleaseSystem.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.Controllers
{
    public class DownLoadController : Controller
    {
        //定义接口变量
        IUploadFile _downLoadFile;

        /// <summary>
        /// 构函
        /// </summary>
        /// <param name="medicalRecords"></param>
        public DownLoadController(IUploadFile downLoadFile)
        {
            _downLoadFile = downLoadFile;


        }

        #region 下载页面入口
        /// <summary>
        /// 下载页面入口
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "1,2,3")]
        [HttpGet("download")]
        public IActionResult DownLoad()
        {
            return View();
        }
        #endregion
        #region 查询所有产品信息
        /// <summary>
        /// 查询所有产品信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("productsdownload")]
        public IActionResult ProductsDownload()
        {

            try
            {
                var data = _downLoadFile.GetProductsList();
                return new JsonResult(new { result = 1, message = "查询产品成功", data = data });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = exc.Message });
            }
        }
        #endregion
        #region 通过产品ID查询版本
        /// <summary>
        /// 通过产品ID查询版本
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        [HttpPost("clickproduct")]
        public IActionResult ClickProduct(int ProductID)
        {

            try
            {
                var dataList = _downLoadFile.GetVersionsByID(ProductID);
                return new JsonResult(new { result = 1, message = "查询成功", data = dataList }, new Newtonsoft.Json.JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = exc.Message });
            }
        }
        #endregion
        #region 点击产品显示描述信息
        /// <summary>
        /// 点击产品显示描述信息
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns></returns>
        [HttpPost("productsdescription")]
        public IActionResult ProductsDescription(int productID)
        {
            try
            {
                var dataList = _downLoadFile.ProductsDescription(productID);
                return new JsonResult(new { result = 1, message = "查询成功", data = dataList }, new Newtonsoft.Json.JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = exc.Message });
            }
        }
        #endregion
        #region 点击版本显示描述信息
        /// <summary>
        /// 点击版本显示描述信息
        /// </summary>
        /// <param name="VersionID">版本ID</param>
        /// <returns></returns>
        [HttpPost("versiondescription")]
        public IActionResult VersionDescription(int VersionID)
        {
            try
            {
                var dataList = _downLoadFile.VersionDescription(VersionID);
                return new JsonResult(new { result = 1, message = "查询成功", data = dataList }, new Newtonsoft.Json.JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = exc.Message });
            }
        }
        #endregion
        #region 显示开发人员信息
        /// <summary>
        /// 显示开发人员信息
        /// </summary>
        /// <param name="VersionID">版本ID</param>
        /// <returns></returns>
        [HttpPost("developers")]
        public IActionResult GetDevelopersByID(int VersionID)
        {
            try
            {
                var dataList = _downLoadFile.GetDevelopersByID(VersionID);
                return new JsonResult(new { result = 1, message = "查询成功", data = dataList }, new Newtonsoft.Json.JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = exc.Message });
            }
        }
        #endregion
        #region 文件查询
        /// <summary>
        /// 文件查询
        /// </summary>
        /// <param name="VersionID">版本ID</param>
        /// <returns></returns>
        [HttpPost("filedownload")]
        public IActionResult FileDownLoad(int VersionID)
        {
            try
            {
                var dataList = _downLoadFile.FileDownLoad(VersionID);
                return new JsonResult(new { result = 1, message = "查询成功", data = dataList }, new Newtonsoft.Json.JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = exc.Message });
            }
        }
        #endregion

        #region 根据版本ID查询所有小版本
        /// <summary>
        /// 根据版本ID查询所有小版本
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("postsomallversions")]
        public IActionResult selectSmallVersions(int id)
        {
            try
            {
                var dataList = _downLoadFile.selectSmallVersions(id);
                return new JsonResult(new { result = 1, message = "查询成功", data = dataList }, new Newtonsoft.Json.JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = exc.Message });
            }
        }
        #endregion
    }
}
