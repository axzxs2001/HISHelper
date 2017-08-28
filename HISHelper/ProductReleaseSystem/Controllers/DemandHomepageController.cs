using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductReleaseSystem.Models.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using ProductReleaseSystem.ProductRelease;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductReleaseSystem.Controllers
{
    public class DemandHomepageController : Controller
    {
        IDemand _idemand;
        // GET: /<controller>/
        public DemandHomepageController(IDemand idemand)
        {
            _idemand = idemand;
        }
        /// <summary>
        /// 产品需求主页
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("productdemand")]
        public IActionResult ProductDemand()
        {
            return View();
        }
        /// <summary>
        /// 产品需求查看主页
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("viewrequirements")]
        public IActionResult ViewRequirements()
        {
            return View();
        }
        #region 查询所有产品
        /// <summary>
        /// 查询所有产品
        /// </summary>
        /// <returns></returns>
        [HttpGet("queryselectproducts")]
        public IActionResult QueryProducts()
        {
            try
            {
                var list = _idemand.QueryProducts();
                return new JsonResult(new { result = 1, data = list, message = "查询成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败:{exc.Message}" });
            }
        }
        #endregion
        #region 根据产品ID查询需求信息
        /// <summary>
        /// 根据产品ID查询需求信息
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("queryrequestbyproductid")]
        public IActionResult QueryRequestByProductId(int id)
        {
            try
            {
                var list = _idemand.QueryRequestByProductId(id);
                return new JsonResult(new { result = 1, message = "查询成功", data = list }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion

        #region 根据产品ID查询需求条数
        /// <summary>
        /// 根据产品ID查询需求条数
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("queryrequestcount")]
        public IActionResult QueryRequestCount(int id)
        {
            try
            {
                var count = _idemand.QueryRequestCount(id);
                return new JsonResult(new { result = 1, message = "查询成功", data = count });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion

        #region 查看详细需求
        /// <summary>
        /// 查看详细需求
        /// </summary>
        /// <param name="id">需求ID</param>
        /// <returns></returns>
        [HttpPost("querydetailedrequirements")]
        public IActionResult QueryDetailedRequirements(int id)
        {
            try
            {
                var list = _idemand.QueryDetailedRequirements(id);
                return new JsonResult(new { result = 1, data = list, message = "查询成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败:{exc.Message}" });
            }
        }
        #endregion

        #region 根据需求ID查询人员意见
        /// <summary>
        /// 根据需求ID查询人员意见
        /// </summary>
        /// <param name="id">需求ID</param>
        /// <returns></returns>
        [HttpPost("queryopinion")]
        public IActionResult QueryOpinion(int id)
        {
            try
            {
                var list = _idemand.QueryOpinion(id);
                return new JsonResult(new { result = 1, data = list, message = "查询成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败:{exc.Message}" });
            }
        }
        #endregion

        #region 添加人员意见
        /// <summary>
        /// 添加人员意见
        /// </summary>
        /// <param name="option">人员意见实体类</param>
        /// <returns></returns>
        [HttpPost("addopinion")]
        public IActionResult AddOpinion(Opinion option)
        {
            if (_idemand.AddOpinion(option))
            {
                return new JsonResult(new { result = 1, message = "添加成功" });
            }
            else
            {
                return new JsonResult(new { result = 0, message = "添加失败" });
            }
        }
        #endregion

        #region 我发布的页面

        #region 通过姓名用户查询ID
        /// <summary>
        /// 通过姓名用户查询ID
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost("selectchaxun")]
        public IActionResult SelectUsers(string name)
        {
            try

            {
                var list = _idemand.InsertUsers(name);
                return new JsonResult(new { result = 1, message = $"查询用户成功！", data = list }, new JsonSerializerSettings()
                {
                    ContractResolver = new LowercaseContractResolver()
                });
            }
            catch (Exception exc)
            {
                // _log.Log(NLog.LogLevel.Error, $"查询全部部门：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"查询用户失败！：{exc.Message}" });
            };
        }
        #endregion

        #region 通过姓名ID查询产品需求
        /// <summary>
        /// 通过姓名ID查询产品需求
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost("selectdemand")]
        public IActionResult SelectDemand(int id)
        {
            try

            {
                var list = _idemand.SelectDemand(id);
                return new JsonResult(new { result = 1, message = $"查询用户成功！", data = list }, new JsonSerializerSettings()
                {
                    ContractResolver = new LowercaseContractResolver()
                });
            }
            catch (Exception exc)
            {
                // _log.Log(NLog.LogLevel.Error, $"查询全部部门：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"查询用户失败！：{exc.Message}" });
            };
        }
        #endregion

        #region 根据产品ID查询需求信息(我发布的)
        /// <summary>
        /// 根据产品ID查询需求信息(我发布的)
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("queryfbproductid")]
        public IActionResult QueryfbProductId(int id, int nameid)
        {
            try
            {
                var list = _idemand.QueryfbProductId(id, nameid);
                return new JsonResult(new { result = 1, message = "查询成功", data = list }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion

        #region 根据产品ID查询需求条数(我发布的)
        /// <summary>
        /// 根据产品ID查询需求条数(我发布的)
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("queryfbcount")]
        public IActionResult QueryfbCount(int id, int nameid)
        {
            try
            {
                var count = _idemand.QueryfbCount(id, nameid);
                return new JsonResult(new { result = 1, message = "查询成功", data = count });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion
        #endregion


        #region  已完成的页面
        #region 查询已完成的所有产品
        /// <summary>
        /// 查询已完成的所有产品
        /// </summary>
        /// <returns></returns>
        [HttpGet("carryoutproducts")]
        public IActionResult CarryOutProducts()
        {
            try
            {
                var list = _idemand.CarryOutProducts();
                return new JsonResult(new { result = 1, data = list, message = "查询成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败:{exc.Message}" });
            }
        }
        #endregion
        #region 根据产品ID查询已完成需求信息
        /// <summary>
        /// 根据产品ID查询已完成需求信息
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("querycarryoutproductid")]
        public IActionResult QueryCarryOutProductId(int id)
        {
            try
            {
                var list = _idemand.QueryCarryOutProductId(id);
                return new JsonResult(new { result = 1, message = "查询成功", data = list }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion
        #region 根据产品ID查询已完成需求条数
        /// <summary>
        /// 根据产品ID查询需求条数
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("querycarryoutcount")]
        public IActionResult QueryCarryOutCount(int id)
        {
            try
            {
                var count = _idemand.QueryCarryOutCount(id);
                return new JsonResult(new { result = 1, message = "查询成功", data = count });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion
        #endregion

        #region 删除需求改变需求的状态放在垃圾箱里
        /// <summary>
        /// 删除需求改变需求的状态放在垃圾箱里
        /// </summary>
        /// <returns></returns>
        [HttpPost("deletestatus")]
        public IActionResult DeleteStatus(int deletestatus, int ID)
        {
            try
            {
                var list = _idemand.DeleteStatus(deletestatus, ID);
                return new JsonResult(new { result = 1, data = list, message = "删除成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"删除失败:{exc.Message}" });
            }
        }
        #endregion

        #region 垃圾箱页面方法

        #region 垃圾箱查询所有产品
        /// <summary>
        /// 垃圾箱查询所有产品
        /// </summary>
        /// <returns></returns>
        [HttpGet("deletequeryselectproducts")]
        public IActionResult DeleteQueryProducts()
        {
            try
            {
                var list = _idemand.DeleteQueryProducts();
                return new JsonResult(new { result = 1, data = list, message = "查询成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败:{exc.Message}" });
            }
        }
        #endregion

        #region 垃圾箱根据产品ID查询需求信息
        /// <summary>
        /// 垃圾箱根据产品ID查询需求信息
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("deletequeryrequestbyproductid")]
        public IActionResult DeleteQueryRequestByProductId(int id)
        {
            try
            {
                var list = _idemand.DeleteQueryRequestByProductId(id);
                return new JsonResult(new { result = 1, message = "查询成功", data = list }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion

        #region 垃圾箱根据产品ID查询需求条数
        /// <summary>
        /// 垃圾箱根据产品ID查询需求条数
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("deletequeryrequestcount")]
        public IActionResult DeleteQueryRequestCount(int id)
        {
            try
            {
                var count = _idemand.DeleteQueryRequestCount(id);
                return new JsonResult(new { result = 1, message = "查询成功", data = count });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion

        #region 垃圾箱还原需求
        /// <summary>
        /// 垃圾箱还原需求
        /// </summary>
        /// <returns></returns>
        [HttpPost("reduction")]
        public IActionResult Reduction(int deletestatus, int ID)
        {
            try
            {
                var list = _idemand.Reduction(deletestatus, ID);
                return new JsonResult(new { result = 1, data = list, message = "还原成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"还原失败:{exc.Message}" });
            }
        }
        #endregion
        #endregion

        #region 删除需求信息
        /// <summary>
        /// 删除需求信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete("deletedata")]
        public IActionResult DeleteData(int ID)
        {
            try
            {
                var list = _idemand.DeleteDemand(ID);
                return new JsonResult(new { result = 1, data = list, message = "删除成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"删除失败:{exc.Message}" });
            }
        }
        #endregion

        #region  审核通过需改状态
        /// <summary>
        /// 审核通过需改状态
        /// </summary>
        /// <param name="Status">状态</param>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        [HttpPost("review")]
        public IActionResult ExaminationPassed(string Status, int ID)
        {
            try
            {
                var list = _idemand.Review(Status, ID);
                return new JsonResult(new { result = 1, data = list, message = "审核通过成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"审核通过失败:{exc.Message}" });
            }
        }
        #endregion

        #region 只看产品查询所有产品
        /// <summary>
        /// 只看产品查询所有产品
        /// </summary>
        /// <returns></returns>
        [HttpPost("queryzkcpproducts")]
        public IActionResult QueryzkcpProducts(int? currentPageIndex = 1, int? RecordPerPage = 10, int? pagePerGroup = 10)
        {
            //try
            //{
            //    var list = _idemand.QueryzkcpProducts(currentPageIndex.Value, recordPerPage.Value, pagePerGroup.Value);
            //    var Count = _idemand.GetCount(currentPageIndex.Value, recordPerPage.Value, pagePerGroup.Value);
            //    return new JsonResult(new { result = 1, RecordCount = Count, data = list, message = "查询成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            //}
            //catch (Exception exc)
            //{
            //    return new JsonResult(new { result = 0, message = $"查询失败:{exc.Message}" });
            //}
            var info = _idemand.QueryzkcpProducts(currentPageIndex.Value, RecordPerPage.Value, pagePerGroup.Value);
            var Count = _idemand.GetCount(currentPageIndex.Value, RecordPerPage.Value, pagePerGroup.Value);
           return new JsonResult(new { RecordCount = Count, List = info }, new Newtonsoft.Json.JsonSerializerSettings()
            { DateFormatString = "yyyy-MM-dd" });
        }
        #endregion

        #region 草稿箱查询所有产品
        /// <summary>
        /// 草稿箱查询所有产品
        /// </summary>
        /// <returns></returns>
        [HttpGet("draftselectproducts")]
        public IActionResult DraftSelectProducts()
        {
            try
            {
                var list = _idemand.DraftSelectProducts();
                return new JsonResult(new { result = 1, data = list, message = "查询成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败:{exc.Message}" });
            }
        }
        #endregion

        #region 草稿箱根据产品ID查询需求信息
        /// <summary>
        /// 草稿箱根据产品ID查询需求信息
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("requestbyselectproductid")]
        public IActionResult RequestBySelectProductId(int id)
        {
            try
            {
                var list = _idemand.RequestBySelectProductId(id);
                return new JsonResult(new { result = 1, message = "查询成功", data = list }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion

        #region 草稿箱根据产品ID查询需求条数
        /// <summary>
        /// 草稿箱根据产品ID查询需求条数
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("queryselectrequestcount")]
        public IActionResult QuerySelectRequestCount(int id)
        {
            try
            {
                var count = _idemand.QuerySelectRequestCount(id);
                return new JsonResult(new { result = 1, message = "查询成功", data = count });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"查询失败：{exc.Message}" });
            }
        }
        #endregion

        #region 添加产品信息
        /// <summary>
        /// 添加产品信息
        /// </summary>
        /// <param name="ProductName"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        [HttpPost("porductinsert")]
        public IActionResult PorductInsert(string ProductName, string Description)
        {
            try
            {
                var list = _idemand.PordcutInsert(ProductName, Description);
                return new JsonResult(new { result = 1, data = list, message = "添加成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"添加失败:{exc.Message}" });
            }
        }
        #endregion

        #region 删除产品对应需求
        /// <summary>
        /// 删除产品对应需求
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete("deleterequestform")]
        public IActionResult DeleteRequestForm(int ID)
        {
            try
            {
                var list = _idemand.DeleteRequestForm(ID);
                return new JsonResult(new { result = 1, data = list, message = "删除需求成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"删除需求失败:{exc.Message}" });
            }
        }
        #endregion

        #region 删除产品
        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        [HttpDelete("deleteproducts")]
        public IActionResult DeleteProducts(int ID)
        {
            try
            {
                var list = _idemand.DeleteProducts(ID);
                return new JsonResult(new { result = 1, data = list, message = "删除成功" }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"删除失败:{exc.Message}" });
            }
        }
        #endregion

        #region 模糊查询产品
        /// <summary>
        /// 模糊查询产品
        /// </summary>
        /// <param name="ProductName"></param>
        /// <returns></returns>
       [HttpPost("selectblurry")]
        public IActionResult SelectBlurry(string ProductName)
        {
            try
            {
                var list = _idemand.SelectBlurry(ProductName);
                return new JsonResult(new { result = 1, data = list, message = "模糊查询成功！" }, new JsonSerializerSettings());
            }
            catch (Exception)
            {
                return new JsonResult(new { result=0,message="模糊查询失败！"});
            }
        }
        #endregion
    }
}
