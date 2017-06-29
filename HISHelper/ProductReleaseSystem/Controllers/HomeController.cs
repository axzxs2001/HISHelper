using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using System.Collections;
using System.IO;
using ProductReleaseSystem.ProductRelease;
using ProductReleaseSystem.Models.IRepository;

namespace ProductReleaseSystem.Controllers
{      
    public class HomeController : Controller
    {
        IUploadFile _IUploadFile;

        public HomeController(IUploadFile iUploadFile)
        {
            _IUploadFile = iUploadFile;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        } 

        /// <summary>
        /// 允许所有登录者
        /// </summary>
        /// <param name="returnUrl">如果用户访问的不是登录页，returnUrl将把这个url传进来，待登录成功后返回这个地址</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            //判断是否验证
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                //把返回地址保存在前台的hide表单中
                ViewBag.returnUrl = returnUrl;
            }
            ViewBag.error = null;
            return View();
        }

        /// <summary>
        /// 允许所有登录者
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="returnUrl">返回u</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(string username, string password, string returnUrl)
        {
            //从数据库验证用户，关取出用户所需要信息
            var users = new List<dynamic>() {
                new { ID = 1, UserName = "zsf",Password="111", Name = "张三丰", RoleTypeID = 1, RoleType = "admin", RoleTypeName = "管理员" },
                 new { ID = 2, UserName = "zwj",Password="222", Name = "张无忌", RoleTypeID = 2, RoleType = "user", RoleTypeName = "普通用户" }
            };
            var user = users.SingleOrDefault(u => u.UserName == username && u.Password == password);
            if (user != null)
            {
                //登录成功后，设置声明
                var claims = new Claim[] {
                      new Claim(ClaimTypes.UserData,username),
                      new Claim(ClaimTypes.Role,user.RoleType),
                      new Claim(ClaimTypes.Name,user.Name),
                      new Claim(ClaimTypes.Sid,user.ID.ToString())
                };
                HttpContext.Authentication.SignInAsync("loginvalidate", new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookie")));
                HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
                return new RedirectResult(returnUrl == null ? "/" : returnUrl);
            }
            else
            {
                ViewBag.error = "用户名或密码错误！";
                return View();
            }
        }
        /// <summary>
        /// 上传主页
        /// </summary>
        /// <returns></returns>
        [HttpGet("send")]
        public IActionResult UpFile()
        {
            return View();
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="env">环境</param>
        /// <param name="UploadPeople">上传人ID</param>
        /// <param name="VersionsID">版本ID</param>
        /// <returns></returns>
        [HttpPost("sendfile")]
        public async Task<IActionResult> UpFile([FromServices] IHostingEnvironment env, int VersionsID)
        {

            try
            {
                var file = HttpContext.Request.Form.Files[0];
                var filePath = env.WebRootPath;
                var fileName = file.FileName;
                var path = filePath + '\\' + fileName;
                var length = path.Length;
                if (!Directory.Exists(path))
                {
                    using (var fStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fStream);
                    }
                    var upFile = new Files { FileName = fileName, UploadTime = System.DateTime.Now, VersionsId = VersionsID, FilePath = path };

                    _IUploadFile.addFiles(upFile);
                    return Ok(new { result = 1, message = "上传文件成功" });
                }
                else
                {
                    return new JsonResult(new { result = 0, message = "文件已存在" });
                }
            } catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = exc.Message });
            }
        }
        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="product">产品名</param>
        /// <returns></returns>
        [HttpPost("addproduct")]
        public bool AddProduct(Products product)
        {
            return _IUploadFile.addProduct(product);
        }

        /// <summary>
        /// 添加版本
        /// </summary>
        /// <param name="product">版本</param>
        /// <returns></returns>
        [HttpPost("addversion")]
        public bool AddVersion(Versions product)
        {
            return _IUploadFile.addVersions(product);
        }

        /// <summary>
        ///查询所有产品信息 
        /// </summary>
        /// <returns></returns>
        [HttpGet("queryproducts")]
        public IActionResult QueryProducts()
        {
            try
            {
                var data = _IUploadFile.GetProductsList();
                return new JsonResult(new { result = 1, message = "查询产品成功", data = data });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = exc.Message });
            }
        }

        /// <summary>
        /// 通过产品ID查询所有版本
        /// </summary>
        /// <param name="ProductID">产品ID</param>
        /// <returns></returns>
        [HttpGet("queryversions")]
        public IActionResult QueryVersionsByProductID(int ProductID)
        {
            try
            {
                var dataList = _IUploadFile.GetVersionsByID(ProductID);
                return new JsonResult(new { result = 1, message = "查询成功", data = dataList }, new Newtonsoft.Json.JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = exc.Message });
            }

        }

        /// <summary>
        /// 人员维护
        /// </summary>
        /// <returns></returns>
        [HttpGet("information")]
        public IActionResult informationMaintenance()
        {
            return View();
        }

        [HttpPost("deleterelatedpersionnels")]
        /// <summary>
        /// 根据版本ID删除相关人员
        /// </summary>
        /// <param name="id"></param>版本ID
        /// <returns></returns>
        public IActionResult DeleteRelatedPersonnels(int id)
        {
            if (_IUploadFile.DeleteRelatedPersonnels(id))
            {
                return new JsonResult(new { result = 1, message = "" });
            }
            else
            {
                return new JsonResult(new { result = 0, message = "" });
            }
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="departmentName">部门名称</param>
        /// <returns></returns>
        [HttpPost("adddepartment")]
        public IActionResult AddDepartment(string departmentName)
        {
            if (_IUploadFile.AddDepartment(departmentName))
            {
                return new JsonResult(new { result = 1, message = "添加部门成功" });
            }
            else
            {
                return new JsonResult(new { result = 0, message = "添加部门失败" });
            }
        }

        /// <summary>
        /// 添加开发人员
        /// </summary>
        /// <param name="developer">开发人员信息</param>
        /// <returns></returns>
        [HttpPost("adddevelopers")]
        public IActionResult AddDeveloper(Developers developer)
        {
            if (_IUploadFile.AddPerson(developer))
            {
                return new JsonResult(new { result = 1, message = "添加开发人员成功" });
            }
            else
            {
                return new JsonResult(new { result = 0, message = "添加开发人员失败" });
            }
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        [HttpPost("adduser")]
        public IActionResult AddUser(Users user)
        {
            if (_IUploadFile.AddUser(user))
            {
                return new JsonResult(new { result = 1, message = "添加用户成功" });
            }
            else
            {
                return new JsonResult(new { result = 0, message = "添加用户失败" });
            }
        }


        /// <summary>
        /// 根据人员ID删除相关人员
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns></returns>
        [HttpPost("deleterp")]
        public IActionResult deleteRp(int id)
        {
            if (_IUploadFile.deleteRp(id))
            {
                return new JsonResult(new { result = 1, message = "移除成功" });
            }
            else
            {
                return new JsonResult(new { result = 0, message = "移除失败" });
            }
        }



        [HttpGet("getusers")]
        public IActionResult GetUsers()
        {
            return new JsonResult(new { Result = 1, message = "查询成功", data = _IUploadFile.QueryUsers() });
        }


        //查询所有开发成员
        [HttpGet("getdevelopers")]
        public IActionResult GetDevelopers(int departmentID)
        {
            return new JsonResult(new { result = 1, nessage = "查询开发人员成功", data = _IUploadFile.QueryDevelopers(departmentID) });
        }
        /// <summary>
        /// 查询所有部门
        /// </summary>
        /// <returns></returns>
        [HttpGet("getdepartments")]
        public IActionResult GetDepartments()
        {
            return new JsonResult(new { result = 1, message = "", data = _IUploadFile.QueryDepartments() });
        }

        [HttpGet("getDeveloper")]
        /// <summary>
        /// 根据成员ID查询成员信息
        /// </summary>
        /// <param name="id"></param>成员ID
        /// <returns></returns>
        public IActionResult GetDeveloper(int id)
        {
            return new JsonResult(new { result = 1, message = "", data = _IUploadFile.QueryDeveloper(id) });
        }

        [HttpGet("getkf")]
        /// <summary>
        /// 查询所有开发人员
        /// </summary>
        /// <returns></returns>
        public IActionResult getkf()
        {
            return new JsonResult(new { result = 1, message = "", data = _IUploadFile.Querykf() });
        }

        /// <summary>
        /// 添加相关人员表
        /// </summary>
        /// <param name="relatedPersonnels"></param>
        /// <returns></returns>
        [HttpPost("addrp")]
        public IActionResult addRelatedPersonnels(int? id,int[] idArray,string productID,int versionID)
        {
            var relatedPersonnels = new RelatedPersonnels();
            foreach (var i in idArray)
            {
                relatedPersonnels.PersonId = i;
                relatedPersonnels.VersionId = versionID;
                relatedPersonnels.Personneltype = "";
                if (_IUploadFile.addRelatedPersonnels(relatedPersonnels))
                {
                    if (id!=null)
                    {
                        _IUploadFile.UpdatePersonType(id);
                        
                    }
                }
                else
                {
                    return new JsonResult(new { result = 0, message = "添加失败" });
                }

            }
           
            return new JsonResult(new { result = 1, message = "添加成功" });
        }

        /// <summary>
        /// 根据版本ID查询所有文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("queryallfiles")]
        public IActionResult QueryAllFiles(int id)
        {
            return new JsonResult(new { result = 1, message = "", data = _IUploadFile.QueryAllFiles(id) });
        }

        /// <summary>
        /// 根据版本ID查询所有相关人员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("queryrelatedpersonnel")]
        public IActionResult QueryRelatedPersonnel(int id)
        {
            return new JsonResult(new { result = 1, message = "", data = _IUploadFile.QueryRelatedPersonnels(id) });
        }
    }
}
