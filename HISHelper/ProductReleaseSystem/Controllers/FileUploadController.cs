using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductReleaseSystem.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: /<controller>/
        [HttpGet("fileupload")]
        public IActionResult FileUpload()
        {
            return View();
        }
    }
}
