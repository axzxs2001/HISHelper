﻿using ProductReleaseSystem.ProductRelease;
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
        /// 查询部门
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> SelectDepartments();
        /// <summary>
        /// 查询产品
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> Products(); 
        /// <summary>
        /// 通过姓名查询用户ID
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> InsertUsers(string name);
        /// <summary>
        /// 查询所有产品
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> QueryProducts();
        /// <summary>
        /// 根据产品ID查询需求信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> QueryRequestByProductId(int id);
        /// <summary>
        /// 根据产品ID查询需求条数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        object QueryRequestCount(int id);
        /// <summary>
        /// 查询详细需求
        /// </summary>
        /// <param name="id">需求ID</param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> QueryDetailedRequirements(int id);

        /// <summary>
        /// 通过姓名ID查询产品需求
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> SelectDemand(int id);
        /// <summary>
        /// 根据产品ID查询需求信息
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <param name="nameid">人员ID</param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> QueryfbProductId(int id,int nameid);
        /// <summary>
        /// 根据产品ID查询需求条数
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <param name="nameid">人员ID</param>
        /// <returns></returns>
        object QueryfbCount(int id,int nameid);
        /// <summary>
        /// 查询已完成的所有产品
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> CarryOutProducts();
        /// <summary>
        /// 根据产品ID查询需求信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> QueryCarryOutProductId(int id);
        /// <summary>
        /// 根据产品ID查询需求条数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        object QueryCarryOutCount(int id); 
        /// <summary>
        /// 修改产品需求把需求放在垃圾箱里
        /// </summary>
        /// <param name="ID">需求ID</param>
        /// <returns></returns>
        bool DeleteStatus(int deletestatus, int ID);
        /// <summary>
        /// 查询所有产品
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> DeleteQueryProducts();
        /// <summary>
        /// 根据产品ID查询需求信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> DeleteQueryRequestByProductId(int id);
        /// <summary>
        /// 根据产品ID查询需求条数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        object DeleteQueryRequestCount(int id);
        /// <summary>
        /// 修改产品需求把需求放在垃圾箱里
        /// </summary>
        /// <param name="ID">需求ID</param>
        /// <returns></returns>
        bool Reduction(int deletestatus, int ID); 
    }
}
