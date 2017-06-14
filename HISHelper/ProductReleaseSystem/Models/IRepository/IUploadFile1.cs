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
        /// 查询指定版本下的文件名
        /// </summary>
        /// <param name="versionsID">版本号</param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> GetFilesByID(int versionsID);

    }
}
