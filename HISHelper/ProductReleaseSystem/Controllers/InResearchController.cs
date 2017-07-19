using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductReleaseSystem.Models.IRepository;
using Newtonsoft.Json;
using ProductReleaseSystem.ProductRelease;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductReleaseSystem.Controllers
{
    public class InResearchController : Controller
    {
        //定义接口变量
        IResearch _IResearch;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_inResearch"></param>
        public InResearchController(IResearch _inResearch)
        {
            _IResearch = _inResearch;


        }

        #region 在研项目主页
        /// <summary>
        /// 在研项目主页
        /// </summary>
        /// <returns></returns>
        [HttpGet("inresearch")]
        public IActionResult InResearch()
        {
            return View();
        }
        #endregion

        #region 在研项目查看主页
        /// <summary>
        /// 在研项目查看主页
        /// </summary>
        /// <returns></returns>
        [HttpGet("inresearchdown")]
        public IActionResult InResearchDownLoad()
        {
            return View();
        }
        #endregion

        #region 查询在研项目
        /// <summary>
        /// 查询在研项目信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("selectinesearch")]
        public IActionResult SelectInesearch()
        {
            try
            {
                var list = _IResearch.SelectResearch();
                return new JsonResult(new { result = 1, message = $"查询在研项目名称成功", data = list }, new JsonSerializerSettings()
                {
                    ContractResolver = new LowercaseContractResolver()
                });
            }
            catch (Exception exc)
            {
                // _log.Log(NLog.LogLevel.Error, $"查询全部部门：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"查询在研项目名称失败：{exc.Message}" });
            }
        }
        #endregion

        #region 通过ID查询在研项目
        /// <summary>
        /// 查询在研项目信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("selectesearchid")]
        public IActionResult SelectesearchID(int id)
        {
            try
            {
                var list = _IResearch.SelectResearch(id);
                return new JsonResult(new { result = 1, message = $"查询在研项目名称成功", data = list }, new JsonSerializerSettings()
                {
                    ContractResolver = new LowercaseContractResolver()
                });
            }
            catch (Exception exc)
            {
                // _log.Log(NLog.LogLevel.Error, $"查询全部部门：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"查询在研项目名称失败：{exc.Message}" });
            }
        }
        #endregion

        #region 查询部门信息
        /// <summary>
        /// 查询部门信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("selectdepartments")]
        public IActionResult GetDepartments()
        {
            try
            {
                var list = _IResearch.SelectDepartments();
                return new JsonResult(new { result = 1, message = $"查询全部部门成功", data = list }, new JsonSerializerSettings()
                {
                    ContractResolver = new LowercaseContractResolver()
                });
            }
            catch (Exception exc)
            {
                // _log.Log(NLog.LogLevel.Error, $"查询全部部门：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"查询部门失败：{exc.Message}" });
            }
        }
        #endregion

        #region 根据部门查询开发人员
        [HttpPost("selectdevelopers")]
        public IActionResult SelectDevelopers(int departmentID)
        {
            try
            {
                var list = _IResearch.SelectDevelopers(departmentID);
                return new JsonResult(new { result = 1, message = $"查询全部部门成功", data = list }, new JsonSerializerSettings()
                {
                    ContractResolver = new LowercaseContractResolver()
                });
            }
            catch (Exception exc)
            {
                // _log.Log(NLog.LogLevel.Error, $"查询全部部门：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"查询部门失败：{exc.Message}" });
            }
        }
        #endregion

        #region 根据人员ID查询人员信息
        [HttpPost("developersid")]
        public IActionResult Developers(int Id)
        {
            try
            {
                var list = _IResearch.SelectRenYuan(Id);
                return new JsonResult(new { result = 1, message = $"查询全部部门成功", data = list }, new JsonSerializerSettings()
                {
                    ContractResolver = new LowercaseContractResolver()
                });
            }
            catch (Exception exc)
            {
                // _log.Log(NLog.LogLevel.Error, $"查询全部部门：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"查询部门失败：{exc.Message}" });
            }
        }
        #endregion

        #region 添加在研项目
        /// <summary>
        /// 添加在研项目
        /// </summary>
        /// <param name="researchprojects">在研项目信息</param>
        /// <returns></returns>
        [HttpPost("insertresearch")]
        public IActionResult InsertResearch(ResearchProjects researchprojects)
        {
            try
            {
                var list = _IResearch.InsertResearch(researchprojects);
                return new JsonResult(new { result = 1, message = $"添加项目成功！", data = list }, new JsonSerializerSettings()
                {
                    ContractResolver = new LowercaseContractResolver()
                });
            }
            catch (Exception exc)
            {
                // _log.Log(NLog.LogLevel.Error, $"查询全部部门：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"添加项目失败：{exc.Message}" });
            }
        }
        #endregion

        #region 添加相关人员
        /// <summary>
        /// 添加在研项目
        /// </summary>
        /// <param name="researchprojects">在研项目信息</param>
        /// <returns></returns>
        [HttpPost("insertresearchers")]
        public IActionResult InsertResearchers(int ResearchProjectsID, int[] idArray, int? id)
        {
            var researchers = new Researchers();
            foreach (var i in idArray)
            {
                researchers.ResearchProjectsID = ResearchProjectsID;
                researchers.PersonID = i;
                researchers.Personneltype = "";
                if (_IResearch.InsertResearchers(researchers))
                {
                    if (id != null)
                    {
                        _IResearch.UpdatePersonType(id);
                    }
                }
                else
                {
                    return new JsonResult(new { result = 0, message = $"添加相关人员失败！" });
                }

            }
            return new JsonResult(new { result = 1, message = "添加成功" });
        }
        #endregion
        #region 根据项目ID查询相关人员
        /// <summary>
        /// 根据项目ID查询相关人员
        /// </summary>
        /// <param name="researchprojects">在研项目信息</param>
        /// <returns></returns>
        [HttpPost("SelectInesearchers")]
        public IActionResult UpdateResearch(int id)
        {
            try
            {
                var list = _IResearch.SelectRenYuan(id);
                return new JsonResult(new { result = 1, message = $"修改项目成功！", data = list }, new JsonSerializerSettings()
                {
                    ContractResolver = new LowercaseContractResolver()
                });
            }
            catch (Exception exc)
            {
                // _log.Log(NLog.LogLevel.Error, $"查询全部部门：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"修改项目失败：{exc.Message}" });
            }
        }
        #endregion


        #region 修改在研项目
        /// <summary>
        /// 修改在研项目
        /// </summary>
        /// <param name="researchprojects">在研项目信息</param>
        /// <returns></returns>
        [HttpPost("updateresearch")]
    public IActionResult UpdateResearch(ResearchProjects upresearch)
    {
        try
        {
            var list = _IResearch.UpdateResearch(upresearch);
            return new JsonResult(new { result = 1, message = $"修改项目成功！", data = list }, new JsonSerializerSettings()
            {
                ContractResolver = new LowercaseContractResolver()
            });
        }
        catch (Exception exc)
        {
            // _log.Log(NLog.LogLevel.Error, $"查询全部部门：{exc.Message}");
            return new JsonResult(new { result = 0, message = $"修改项目失败：{exc.Message}" });
        }
    }
    #endregion



}
}
