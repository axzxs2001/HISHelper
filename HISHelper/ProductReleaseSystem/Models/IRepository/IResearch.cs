using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductReleaseSystem.Models.ProductRelease;

namespace ProductReleaseSystem.Models.IRepository
{
    public interface IResearch
    {
        /// <summary>
        /// 添加在研项目
        /// </summary>
        /// <param name="researchprojects"></param>
        /// <returns></returns>
        bool InsertResearch(ResearchProjects researchprojects);
        /// <summary>
        /// 查询在研项目名称
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> SelectResearch();
        /// <summary>
        /// 查询在研项目信息
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> SelectResearch(int ID);
        /// <summary>
        /// 添加在研人员
        /// </summary>
        /// <param name="researchprojects"></param>
        /// <returns></returns>
        bool InsertResearchers(Researchers researchers);
        /// <summary>
        /// 删除开发人员ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteResearchers(int id);

        /// <summary>
        /// 通过部门ID查询开发人员
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> SelectDevelopers(int departmentID);

        /// <summary>
        /// 查询部门信息
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> SelectDepartments();

    }
}
