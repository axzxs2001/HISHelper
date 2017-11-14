using System;
using System.Collections.Generic;

namespace Progress.Models.DataModels
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }
        /// <summary>
        /// 路由地址
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 访问谓词
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public int? Pid { get; set; }
        /// <summary>
        /// 元素ID
        /// </summary>
        public string ElementID { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
    }
}
