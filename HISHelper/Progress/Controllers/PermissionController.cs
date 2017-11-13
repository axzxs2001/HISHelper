using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Progress.Controllers
{
    [Authorize(Policy = "Permission")]
    public class PermissionController : Controller
    {
       
        public PermissionController()
        {

        }
        public IActionResult UserRole()
        {
            return View();
        }

        public IActionResult RolePermission()
        {
            return View();
        }
    }
}