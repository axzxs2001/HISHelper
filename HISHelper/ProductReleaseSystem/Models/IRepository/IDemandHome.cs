using ProductReleaseSystem.ProductRelease;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.Models.IRepository
{
    public interface IDemandHome
    {
        #region 查询全部
        /// <summary>
        /// 查询所有需求
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> QueryAllProducts();
        /// <summary>
        /// 查询需求
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> QueryAllRequestForms(int id);
        /// <summary>
        /// 查询除了未通过审核的需求共有多少条需求
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        object QueryRequestsCount(int id);
        #endregion

        #region 与我相关
        /// <summary>
        /// 查询与我相关的产品
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> QueryAndMeProduct(int id);
        /// <summary>
        /// 查询与我相关的需求
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <param name="pid">产品id</param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> QueryAndMeRequest(int id,int pid);
        /// <summary>
        /// 查询与我相关的产品的需求数
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <param name="pid">产品ID</param>
        /// <returns></returns>
        object QueryAndMeCount(int id, int pid);
        #endregion

        #region 在研项目

        #endregion

        #region 已完成的
        /// <summary>
        /// 已完成的产品
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string,dynamic>> QueryFinishProduct();
        /// <summary>
        /// 已完成的需求
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> QueryFinishRequestForm(int id);
        /// <summary>
        /// 已完成的需求条数
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        object QueryCountFinish(int id);
        #endregion

        #region 未开始的
        /// <summary>
        /// 未开始的产品
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> QueryNoProduct();
        /// <summary>
        /// 根据产品ID查询未开始的需求
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> QueryNoRequestForm(int id);
        /// <summary>
        /// 查询产品有多少条未完成的需求
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        object QueryCountNo(int id);
        #endregion

    }
}
