using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Working.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        protected int UserID
        {
            get
            {
                var id = User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.PrimarySid);
                if (id != null && !string.IsNullOrEmpty(id.Value))
                {
                    return Convert.ToInt32(id.Value);
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}