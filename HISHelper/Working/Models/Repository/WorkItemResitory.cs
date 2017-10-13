using System;
using System.Collections.Generic;
using System.Linq;
using Working.Models.DataModel;

namespace Working.Model.Repository
{
    /// <summary>
    /// 活动仓储类
    /// </summary>
    public class WorkItemResitory : IWorkItemResitory
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        WorkingDbContext _dbContext;
        /// <summary>
        /// 权限仓储类构造
        /// </summary>
        /// <param name="dbContext">startup注入的数据库对象</param>
        public WorkItemResitory(WorkingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="workItem">工作记录</param>
        /// <returns></returns>
        public bool AddWorkItem(WorkItem workItem)
        {
            _dbContext.WorkItems.Add(workItem);
            var result = _dbContext.SaveChanges();
            return result > 0;
        }
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public WorkItem GetWorkItem(int id)
        {
            return _dbContext.WorkItems.SingleOrDefault(s => s.ID == id);
        }
        /// <summary>
        /// 按用户ID获取活动
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public List<WorkItem> GetAWorkItemsByUserID(int userID)
        {
            return _dbContext.WorkItems.Where(w => w.CreateUserID == userID).OrderByDescending(o => o.CreateTime).ToList();
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="newWorkItem">新实体</param>
        /// <returns></returns>
        public bool ModifyWorkItem(WorkItem newWorkItem)
        {
            var oldWorkItem = _dbContext.WorkItems.SingleOrDefault(s => s.ID == newWorkItem.ID);
            if (oldWorkItem == null)
            {
                return false;
            }
            else
            {
                oldWorkItem.WorkContent = newWorkItem.WorkContent;
                oldWorkItem.CreateUserID = newWorkItem.CreateUserID;
                oldWorkItem.CreateTime = newWorkItem.CreateTime;
                var result = _dbContext.SaveChanges();
                return result > 0;
            }
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="ID">实体ID</param>
        /// <returns></returns>
        public bool RemoveWorkItem(int ID)
        {
            var oldWorkItem = _dbContext.WorkItems.SingleOrDefault(s => s.ID ==ID);
            if (oldWorkItem == null)
            {
                return false;
            }
            else
            {
                _dbContext.WorkItems.Remove(oldWorkItem);
                 var result = _dbContext.SaveChanges();
                return result > 0;
            }
        }
    }
}
