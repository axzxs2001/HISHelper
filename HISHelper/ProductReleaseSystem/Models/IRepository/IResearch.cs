using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductReleaseSystem.ProductRelease;

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
        /// 修改病人为管理员
        /// </summary>
        /// <param name="id">病人ID</param>
        /// <returns></returns>
        bool UpdatePersonType(int? id,int ResearchProjectsID);
        /// <summary>
        /// 删除开发人员ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteResearchers(int ResearchProjectsID, int personID);

        /// <summary>
        /// 根据项目ID删除相关人员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteAllResearchers(int id);

        /// <summary>
        /// 通过部门ID查询开发人员
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> SelectDevelopers(int departmentID);

        /// <summary>
        /// 通过人员ID查询开发人员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> SelectRenYuan(int id);

        /// <summary>
        /// 查询部门信息
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> SelectDepartments();
        /// <summary>
        /// 删除在研项目
        /// </summary>
        /// <param name="id">在研项目ID</param>
        /// <returns></returns>
        bool DeleteResearch(int id);

        /// <summary>
        /// 修改在研项目信息
        /// </summary>
        /// <param name="upresearch"></param>
        /// <returns></returns>
        bool UpdateResearch(ResearchProjects upresearch);
        /// <summary>
        /// 根据项目ID查询所有相关人员
        /// </summary>
        /// <param name="id"></param>版本号
        /// <returns></returns>
        List<Dictionary<string, dynamic>> SelectInresearchers(int id);
    }
}
