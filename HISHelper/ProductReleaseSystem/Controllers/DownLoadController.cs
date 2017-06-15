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
            var info = _downLoadFile.GetProductsList();
            return new JsonResult(new { List = info }, new Newtonsoft.Json.JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
        }
        [HttpPost("clickproduct")]
        public IActionResult ClickProduct(int ID)
        {
            var info = _downLoadFile.GetVersionsByID(ID);
            return new JsonResult(new { List = info }, new Newtonsoft.Json.JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd"});
        }
    }
}
