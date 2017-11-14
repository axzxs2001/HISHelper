
namespace Progress
{
    /// <summary>
    /// 用户或角色或其他凭据实体
    /// </summary>
    public class AuthorizePermission
    {
        /// <summary>
        /// 用户或角色或其他凭据名称
        /// </summary>
        public virtual string RoleName
        { get; set; }
        /// <summary>
        /// 请求action对应的Url
        /// </summary>
        public virtual string Action
        { get; set; }

        /// <summary>
        /// 访问谓词 get,post,put,delete
        /// </summary>
        public virtual string Method
        { get; set; }

    }
}
