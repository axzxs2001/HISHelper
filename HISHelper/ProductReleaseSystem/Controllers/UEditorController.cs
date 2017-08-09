using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UEditorNetCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductReleaseSystem.Controllers
{
    [Route("api/[controller]")]
    public class UEditorController : Controller
    {
        private UEditorService ue;
        // GET: /<controller>/
        public UEditorController(UEditorService ue)
        {
            this.ue = ue;
        }
        public void Do()
        {
            ue.DoAction(HttpContext);
        }
    }
}
