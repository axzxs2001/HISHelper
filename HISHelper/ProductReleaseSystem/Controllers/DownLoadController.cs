using Microsoft.AspNetCore.Mvc;
using ProductReleaseSystem.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.Controllers
{
    public class DownLoadController:Controller
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


        /// <summary>
        /// 页面入口
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("download")]
        public IActionResult DownLoad()
        {
            return View();
        }
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
    }
}
