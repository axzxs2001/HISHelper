using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductReleaseSystem.Models.IRepository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductReleaseSystem.Controllers
{
    public class InResearchController : Controller
    {
    IResearch _IResearch;
        // GET: /<controller>/
        public InResearchController(IResearch _inResearch)
        {
            _IResearch = _inResearch;


        }
        /// <summary>
        /// 在研项目主页
        /// </summary>
        /// <returns></returns>
        [HttpGet("inresearch")]
        public IActionResult InResearch()
        {
            return View();
        }
    }
}
