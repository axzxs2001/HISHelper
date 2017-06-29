using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.Models.IRepository
{
    /// <summary>
    /// 上传查询接口
    /// </summary>
    public partial interface IUploadFile
    {
        /// <summary>
        /// 查询所有产品列表
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> GetProductsList();

        /// <summary>
        /// 查询指定产品的所有版本信息列表
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> GetVersionsByID(int productID);
        /// <summary>
        /// 查询指定版本下的信息
        /// </summary>
        /// <param name="versionsID">版本id</param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> GetVersionInfo(int versionsID);
        /// <summary>
        /// 查询指定版本的相关人员信息
        /// </summary>
        /// <param name="versionsID">版本id</param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> GetDevelopersByID(int versionsID);
        /// <summary>
        /// 查询指定版本下的文件信息
        /// </summary>
        /// <param name="versionsID"></param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> GetFilesByID(int versionsID);
        /// <summary>
        /// 查询产品对应描述
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> ProductsDescription(int productID);
        /// <summary>
        /// 查询版本对应描述
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> VersionDescription(int productID);

        /// <summary>
        /// 文件查询
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> FileDownLoad(int VersionID); 


    }
}
