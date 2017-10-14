using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Working.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            get
            {
                var id = User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.PrimarySid);
                if (id != null && string.IsNullOrEmpty(id.Value))
                {
                    return Convert.ToInt32(id.Value);
                }
                else
                {
                    return 1;
                }
            }
        }
    }
}