using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductReleaseSystem.ProductRelease;
namespace ProductReleaseSystem.Models.IRepository
{
    public partial interface IUploadFile
    {
        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        bool addProduct(Products products);

        /// <summary>
        /// 添加版本
        /// </summary>
        /// <param name="versions"></param>
        /// <returns></returns>
        bool addVersions(Versions versions);
        
        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        bool addFiles(Files files);
    }
}
