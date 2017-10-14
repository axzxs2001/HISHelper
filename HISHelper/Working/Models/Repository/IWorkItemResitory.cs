using System.Collections.Generic;
using Working.Models.DataModel;

namespace Working.Model.Repository
{
    /// <summary>
    /// 活动管理仓储接口
    /// </summary>
    public interface IWorkItemResitory
    {
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="workItem">工作记录</param>
        /// <returns></returns>
        bool AddWorkItem(WorkItem workItem);
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        WorkItem GetWorkItem(int id);
        /// <summary>
        /// 按用户ID获取活动
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        List<WorkItem> GetWorkItemsByUserID(int userID);
        /// <summary>
        /// 按用户，年，月获取用户工作记录
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        List<WorkItem> GetWorkItemsByUserID(int userID, int year, int month);
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="newWorkItem">新实体</param>
        /// <returns></returns>
        bool ModifyWorkItem(WorkItem newWorkItem);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="ID">实体ID</param>
        /// <returns></returns>
        bool RemoveWorkItem(int ID);
    }
}
