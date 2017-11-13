﻿using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Progress
{
    /// <summary>
    /// 权限授权Handler
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        /// <summary>
        /// 用户权限
        /// </summary>
        public List<AuthorizePermission> Permissions { get; set; }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            //赋值用户权限
            Permissions = requirement.Permissions;
            //从AuthorizationHandlerContext转成HttpContext，以便取出表求信息
            var httpContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext).HttpContext;
            //请求Url
            var questUrl = httpContext.Request.Path.Value.ToLower();
            //是否经过验证
            var isAuthenticated = httpContext.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                //权限中是否存在请求的url
                if (Permissions.GroupBy(g => g.Action).Where(w => w.Key.ToLower() == questUrl).Count() > 0)
                {
                    var name = httpContext.User.Claims.SingleOrDefault(s => s.Type == requirement.ClaimType).Value;                   
                    //验证权限
                    if (Permissions.Where(w => w.RoleName == name && w.Action.ToLower() == questUrl&&w.Method== httpContext.Request.Method.ToLower()).Count() > 0)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        //无权限跳转到拒绝页面
                        httpContext.Response.Redirect(requirement.DeniedAction);
                    }
                }
                else
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
