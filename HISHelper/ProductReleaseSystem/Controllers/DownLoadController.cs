using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.Controllers
{
    public class DownLoadController:Controller
    {
        //定义接口变量
        IDownLoadFile _downLoadFile;

        /// <summary>
        /// 构函
        /// </summary>
        /// <param name="medicalRecords"></param>
        public DownLoadController(IDownLoadFile downLoadFile)
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
    }
}
