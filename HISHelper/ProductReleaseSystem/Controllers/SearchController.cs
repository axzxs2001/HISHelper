using Microsoft.AspNetCore.Mvc;
using ProductReleaseSystem.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.Controllers
{
    public class SearchController: Controller
    {
        //定义接口变量
        IUploadFile _uploadFile;

        /// <summary>
        /// 构函
        /// </summary>
        /// <param name="medicalRecords"></param>
        public SearchController(IUploadFile uploadFile)
        {
            _uploadFile = uploadFile;
        }

        /// <summary>
        /// 页面入口
        /// </summary>
        /// <returns></returns>
        [HttpGet("testuploadfile")]
        public IActionResult TestUpLoadFile()
        {
            return View();
        }
        /// <summary>
        /// 查询产品列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetProductsList()
        {
            var result= _uploadFile.GetProductsList();
            return new JsonResult(result);
        }
    }
}
