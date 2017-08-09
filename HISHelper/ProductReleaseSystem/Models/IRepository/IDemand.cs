using ProductReleaseSystem.ProductRelease;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.Models.IRepository
{
    public interface IDemand
    {
        /// <summary>
        /// 添加需求项目
        /// </summary>
        /// <param name="requestform">需求信息</param>
        /// <returns></returns>
        bool InsertDemand(RequestForm requestform);
        /// <summary>
        /// 查询需求全部信息
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> SelectDemand();
        /// <summary>
        /// 修改需求项目
        /// </summary>
        /// <param name="researchprojects"></param>
        /// <returns></returns>
        bool UpdateDemand(RequestForm requestform);
        /// <summary>
        /// 删除需求项目
        /// </summary>
        /// <param name="ID">需求ID</param>
        /// <returns></returns>
        bool DeleteDemand(int ID);
        /// <summary>
        /// 添加产品需求表
        /// </summary>
        /// <param name="requestform">需求信息</param>
        /// <returns></returns>
        bool InsertProductDemand(ProductDemandTable productdemand);
        /// <summary>
        /// 查询产品需求表
        /// </summary>
        /// <param name="requestform">需求信息</param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> SelectProductDemand();
        /// <summary>
        /// 修改产品需求项目
        /// </summary>
        /// <param name="researchprojects"></param>
        /// <returns></returns>
        bool UpdateProductDemand(ProductDemandTable productdemand);
        /// <summary>
        /// 删除产品需求项目
        /// </summary>
        /// <param name="ID">需求ID</param>
        /// <returns></returns>
        bool DeleteProductDemand(int ID);
        /// <summary>
        /// 查询产品需求表
        /// </summary>
        /// <param name="requestform">需求信息</param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> SelectDepartments();
        /// <summary>
        /// 通过姓名查询用户ID
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> InsertUsers(string name);


    }
}
