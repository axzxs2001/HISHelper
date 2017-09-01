using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using System.Collections;
using ProductReleaseSystem.ProductRelease;
using ProductReleaseSystem.Models.IRepository;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using ProductReleaseSystem.Models.Data;

namespace ProductReleaseSystem.Controllers
{
    /// <summary>
    /// 本Controller允许admin和user两种角色可以访问
    /// </summary>
    //	Authorize 授权，批准，委托    Roles 角色
    public class HomeController : Controller
    {
        IUploadFile _IUploadFile;
       
        public HomeController(IUploadFile iUploadFile)
        {
            _IUploadFile = iUploadFile;
        }
        #region 原始页面
        [Authorize(Roles = "管理员,开发负责人,实施负责人,开发人员,实施人员,产品人员")]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// About只允许角色访问
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "user")]
        public IActionResult About()
        {
            var id = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        /// <summary>
        /// Contact只允许admin角色访问
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        public IActionResult Contact()
        {
            var id = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        #endregion
        /// <summary>
        /// 说明文档
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Documentation()
        {
            return View();
        }
        #region 允许所有登录者
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
        #endregion

        #region 允许所有登录者
        /// <summary>
        ///允许所有登录者
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="returnUrl">返回</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(string username, string password, string returnUrl, string verificationcode)
        {
            //从数据库验证用户，关取出用户所需要信息

            try
            {
                if (username == null || password == null)
                {
                    return new JsonResult(new { result = 0, message = $"账号或密码错误！" });
                }
                else if (verificationcode != verification)
                {
                    return new JsonResult(new { result = 0, message = $"验证码错误！" });
                }
                else
                {
                    var list = _IUploadFile.SelectUsers(username, password);
                    if (list.Count != 0)
                    {
                        //登录成功后，设置声明
                        var claims = new Claim[] {
                      new Claim(ClaimTypes.UserData,username),
                      new Claim(ClaimTypes.Role,list[0]["Authority"]),
                      new Claim(ClaimTypes.Name,list[0]["UserName"].ToString()),
                      new Claim(ClaimTypes.Sid,list[0]["ID"].ToString())
                        };
                        HttpContext.Authentication.SignInAsync("loginvalidate", new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookie")));
                        HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));

                        return new JsonResult(new { result = 1, message = $"查询全部部门成功", data = list }, new JsonSerializerSettings());

                    }
                    else
                    {
                        return new JsonResult(new { result = 0, message = $"账号或密码错误！" });
                    }
                }
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = $"登录失败！：{exc.Message}" });
            }
        }

        [HttpGet("logout")]
        public IActionResult LgoOut()
        {
            HttpContext.Authentication.SignOutAsync("loginvalidate");
            return Redirect("/login");
        }

        #endregion

        [HttpGet("authority")]
        public IActionResult Authority()
        {
            return View();
        }

        /// <summary>
        /// 用户协议
        /// </summary>
        /// <returns></returns>
        [HttpGet("userprotocol")]
        public IActionResult UserProtocol()
        {
            return View();
        }

       public static string verification = "";

        /// <summary>
        /// 图形验证码
        /// </summary>
        /// <param name="_vierificationCodeServices"></param>
        /// <returns></returns>
        [HttpGet("validatecode")]
        public IActionResult ValidateCode([FromServices]VierificationCodeServices _vierificationCodeServices)
        {
            string code = "";
            MemoryStream ms = _vierificationCodeServices.Create(out code);
            HttpContext.Session.SetString("LoginValidateCode", code);
            Response.Body.Dispose();
            verification = code.ToLower();
            return File(ms.ToArray(), @"images/png");
        }

        #region 人员维护
        /// <summary>
        /// 人员维护
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "管理员,开发负责人,开发人员")]
        [HttpGet("information")]
        public IActionResult InformationMaintenance()
        {
            return View();
        }
        #endregion

        #region 人员管理增删改查
        #region 部门增删改查
        #region  添加部门



        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="departmentName">部门名称</param>
        /// <returns></returns>
        [HttpPost("adddepartment")]
        public IActionResult AddDepartment(string departmentName)
        {
            try
            {
                _IUploadFile.AddDepartment(departmentName);
                return new JsonResult(new { result = 1, message = $"添加部门成功！" });
            }
            catch (Exception exc)
            {
                //_log.Log(NLog.LogLevel.Error, $"添加菜单：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"添加部门失败！：{exc.Message}" });
            }
        }
        #endregion
        #region 查询部门信息
        /// <summary>
        /// 查询部门信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("getdepartments")]
        public IActionResult GetDepartments()
        {
            try
            {
                var list = _IUploadFile.QueryDepartments();
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
        #region 删除部门
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        [HttpPost("deletedepartments")]
        public IActionResult DeleteDepartments(int id)
        {
            try
            {
                _IUploadFile.DeleteDepartments(id);
                return new JsonResult(new { result = 1, message = $"删除部门成功！" });
            }
            catch (Exception exc)
            {
                //_log.Log(NLog.LogLevel.Error, $"删除菜单：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"删除部门失败！：{exc.Message}" });

            }
        }
        #endregion
        #region 修改部门
        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        [HttpPost("updatedepartments")]
        public IActionResult UpdateDepartments(int id, string departmentName)
        {
            try
            {
                _IUploadFile.UpdateDepartment(id, departmentName);
                return new JsonResult(new { result = 1, message = $"修改部门成功！" });
            }
            catch (Exception exc)
            {
                //_log.Log(NLog.LogLevel.Error, $"删除菜单：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"修改部门失败！：{exc.Message}" });

            }
        }
        #endregion
        #endregion
        #region 查询权限信息
        /// <summary>
        /// 查询权限信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("authoritytable")]
        public IActionResult AuthorityTable()
        {
            try
            {
                var list = _IUploadFile.AuthorityTable();
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
        #region 开发人员增删改查
        #region 添加开发人员
        /// <summary>
        /// 添加开发人员
        /// </summary>
        /// <param name="developer">开发人员信息</param>
        /// <returns></returns>
        [HttpPost("adddevelopers")]
        public IActionResult AddDeveloper(Developers developer)
        {
            try
            {
                _IUploadFile.AddPerson(developer);
                return new JsonResult(new { result = 1, message = $"添加开发人员成功！" });
            }
            catch (Exception exc)
            {
                //_log.Log(NLog.LogLevel.Error, $"添加菜单：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"添加开发人员失败！：{exc.Message}" });
            }
        }
        #endregion
        #region 查询开发人员信息
        /// <summary>
        /// 查询开发人员信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("getdevelopers")]
        public IActionResult GetDevelopers()
        {
            try
            {
                var list = _IUploadFile.QueryDevelopers();
                return new JsonResult(new { result = 1, message = $"查询开发人员成功！", data = list }, new JsonSerializerSettings()
                {
                    ContractResolver = new LowercaseContractResolver()
                });
            }
            catch (Exception exc)
            {
                // _log.Log(NLog.LogLevel.Error, $"查询全部部门：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"查询开发人员失败！：{exc.Message}" });
            }
        }


        #endregion
        #region 删除开发人员
        /// <summary>
        /// 删除开发人员
        /// </summary>
        /// <param name="id">开发人员ID</param>
        /// <returns></returns>
        [HttpPost("deletedeveloper")]
        public IActionResult DeleteDeveloper(int id)
        {
            try
            {
                _IUploadFile.DeleteDeveloper(id);
                return new JsonResult(new { result = 1, message = $"删除开发人员成功" });
            }
            catch (Exception exc)
            {
                //_log.Log(NLog.LogLevel.Error, $"删除菜单：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"删除开发人员失败：{exc.Message}" });

            }
        }
        #endregion
        #region 修改开发人员
        /// <summary>
        /// 修改开发人员
        /// </summary>
        /// <param name="developer">开发人员信息</param>
        /// <returns></returns>
        [HttpPost("updatedeveloper")]
        public IActionResult UpdateDeveloper(Developers developer)
        {
            try
            {
                _IUploadFile.UpdateDeveloper(developer);
                return new JsonResult(new { result = 1, message = $"修改开发人员成功！" });
            }
            catch (Exception exc)
            {
                //_log.Log(NLog.LogLevel.Error, $"删除菜单：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"修改开发人员失败！：{exc.Message}" });

            }
        }
        #endregion
        #endregion

        #region 用户增删改查
        #region 添加用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        [HttpPost("adduser")]
        public IActionResult AddUser(Users user)
        {
            try
            {
                _IUploadFile.AddUser(user);
                return new JsonResult(new { result = 1, message = $"添加用户成功！" });
            }
            catch (Exception exc)
            {
                //_log.Log(NLog.LogLevel.Error, $"添加菜单：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"添加用户失败！：{exc.Message}" });
            }
        }
        #endregion
        #region 修改用户
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        [HttpPost("updateuser")]
        public IActionResult UpdateUser(Users user)
        {
            try
            {
                _IUploadFile.UpdataUser(user);
                return new JsonResult(new { result = 1, message = $"修改用户成功！" });
            }
            catch (Exception exc)
            {
                //_log.Log(NLog.LogLevel.Error, $"添加菜单：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"修改用户失败！：{exc.Message}" });
            }
        }
        #endregion
        #region 查询用户信息
        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("getusers")]
        public IActionResult GetUsers()
        {
            try

            {
                var list = _IUploadFile.QueryUsers();
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
        #region 删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpPost("deleteuser")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _IUploadFile.DeleteUser(id);
                return new JsonResult(new { result = 1, message = $"删除用户成功" });
            }
            catch (Exception exc)
            {
                //_log.Log(NLog.LogLevel.Error, $"删除菜单：{exc.Message}");
                return new JsonResult(new { result = 0, message = $"删除用户失败：{exc.Message}" });
            }
        }
        #endregion


        #region 通过姓名用户查询ID
        /// <summary>
        /// 通过姓名用户查询ID
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost("insertiser")]
        public IActionResult InsertUser(string name)
        {
            try

            {
                var list = _IUploadFile.InsertUsers(name);
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
        #endregion
        #endregion


        #region 上传管理方法
        #region 添加产品
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
        #endregion
        #region 查询所有产品信息
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
        #endregion
        #region 添加版本
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

        #endregion
        #region 通过产品ID查询所有版本
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
        #endregion
        #region 上传主页
        /// <summary>
        /// 上传主页
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "管理员,开发负责人,开发人员")]
        [HttpGet("send")]
        public IActionResult UpFile()
        {
            return View();
        }
        #endregion
        #region 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="env">环境</param>
        /// <param name="UploadPeople">上传人ID</param>
        /// <param name="VersionsID">版本ID</param>
        /// <returns></returns>
        [HttpPost("sendfile")]
        public async Task<IActionResult> UpFile([FromServices] IHostingEnvironment env, int UploadPeople, int VersionsID, string ProjectName)
        {

            try
            {
                var file = HttpContext.Request.Form.Files[0];
                var filePath = env.WebRootPath;
                var fileName = file.FileName;
                var url = filePath + '\\' +
                        "产品项目" + '\\' + ProjectName;
                if (!Directory.Exists(url))
                {
                    System.IO.Directory.CreateDirectory(filePath + '\\' +
                        "产品项目" + '\\' + ProjectName);
                }
                var path = filePath + '\\' + "产品项目" + '\\' + ProjectName + '\\' + fileName;
                if (!Directory.Exists(path))
                {
                    using (var fStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fStream);
                    }
                    var upFile = new Files { FileName = fileName, UploadTime = System.DateTime.Now, UploadPeople = UploadPeople, VersionsId = VersionsID, FilePath = $"/产品项目/{ProjectName}/{fileName}" };

                    _IUploadFile.addFiles(upFile);
                    return Ok(new { result = 1, message = "上传文件成功" });
                }
                else
                {
                    return new JsonResult(new { result = 0, message = "文件已存在" });
                }
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 0, message = exc.Message });
            }
        }
        #endregion
        # region 删除文件
        /// <summary>
        /// 根据文件ID删除文件
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <returns></returns>
        [HttpPost("deletefile")]
        public IActionResult deleteFile(int id)
        {
            var a = _IUploadFile.getFilePath(id);
            var path = "wwwroot" + a;
            if (_IUploadFile.deleteFile(id))
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return new JsonResult(new { result = 1, message = "删除成功" });
                }
                else
                {
                    return new JsonResult(new { result = 0, message = "删除失败" });
                }
            }
            else
            {
                return new JsonResult(new { result = 0, message = "删除失败" });
            }
        }
        #endregion
        #region  根据人员ID删除相关人员 
        /// <summary>
        /// 根据人员ID删除相关人员
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns></returns>
        [HttpPost("deleterp")]
        public IActionResult DeleteRp(int id, int versionID)
        {
            if (_IUploadFile.deleteRp(id, versionID))
            {
                return new JsonResult(new { result = 1, message = "移除成功" });
            }
            else
            {
                return new JsonResult(new { result = 0, message = "移除失败" });
            }
        }
        #endregion
        #region 通过部门查询开发成员
        /// <summary>
        /// 通过部门查询开发成员
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        [HttpGet("getdevelopers")]
        public IActionResult GetDevelopers(int departmentID)
        {
            return new JsonResult(new { result = 1, nessage = "查询开发人员成功", data = _IUploadFile.SelectDevelopers(departmentID) });
        }
        #endregion
        #region 查询所有开发人员
        /// <summary>
        /// 查询所有开发人员
        /// </summary>
        /// <returns></returns>
        [HttpGet("getkf")]
        public IActionResult Getkf()
        {
            return new JsonResult(new { result = 1, message = "", data = _IUploadFile.Querykf() });
        }

        #endregion
        #region 添加相关人员表
        /// <summary>
        /// 添加相关人员表
        /// </summary>
        /// <param name="relatedPersonnels"></param>
        /// <returns></returns>

        [HttpPost("addrp")]
        public IActionResult AddRelatedPersonnels(int? id, int[] idArray, string productID, int versionID)
        {
            var relatedPersonnels = new RelatedPersonnels();
            foreach (var i in idArray)
            {
                relatedPersonnels.PersonId = i;
                relatedPersonnels.VersionId = versionID;
                relatedPersonnels.Personneltype = "";
                if (_IUploadFile.addRelatedPersonnels(relatedPersonnels))
                {
                    if (id != null)
                    {
                        _IUploadFile.UpdatePersonType(id, versionID);

                    }
                }
                else
                {
                    return new JsonResult(new { result = 0, message = "添加失败" });
                }

            }

            return new JsonResult(new { result = 1, message = "添加成功" });
        }

        #endregion
        #region 根据成员ID查询成员信息
        /// <summary>
        /// 根据成员ID查询成员信息
        /// </summary>
        /// <param name="id">成员ID</param>
        /// <returns></returns>
        [HttpGet("getDeveloper")]
        public IActionResult GetDeveloper(int id)
        {
            return new JsonResult(new { result = 1, message = "", data = _IUploadFile.QueryDeveloper(id) });
        }

        #endregion
        #region 根据版本ID查询所有文件
        /// <summary>
        /// 根据版本ID查询所有文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("queryallfiles")]
        public IActionResult QueryAllFiles(int id)
        {
            return new JsonResult(new { result = 1, message = "", data = _IUploadFile.QueryAllFiles(id) }, new Newtonsoft.Json.JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd HH:mm" });
        }
        #endregion
        #region 根据版本ID查询所有相关人员
        /// <summary>
        /// 根据版本ID查询所有相关人员
        /// </summary>
        /// <param name="id">版本ID</param>
        /// <returns></returns>
        [HttpGet("queryrelatedpersonnel")]
        public IActionResult QueryRelatedPersonnel(int id)
        {
            return new JsonResult(new { result = 1, message = "", data = _IUploadFile.QueryRelatedPersonnels(id) });
        }
        #endregion
        #region 查询所有部门
        /// <summary>
        /// 查询所有部门
        /// </summary>
        /// <returns></returns>
        [HttpPost("selectdepartments")]
        public IActionResult SelectDepartments()
        {
            return new JsonResult(new { result = 1, message = "", data = _IUploadFile.QueryDepartments() });
        }
        #endregion
        #region 根据版本ID删除相关人员
        /// <summary>
        /// 根据版本ID删除相关人员
        /// </summary>
        /// <param name="id"></param>版本ID
        /// <returns></returns>
        [HttpPost("deleterelatedpersionnels")]
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
        #endregion
        #region 根据版本ID删除版本
        /// <summary>
        /// 根据版本ID删除版本
        /// </summary>
        /// <param name="id">版本ID</param>
        /// <returns></returns>
        [HttpPost("deleteversion")]
        public IActionResult deleteVersion(int id)
        {
            try
            {
                if (_IUploadFile.deleteVersion(id))
                {
                    return new JsonResult(new { result = 1, message = "移除成功" });
                }
                else
                {
                    return new JsonResult(new { result = 0, message = "移除失败" });
                }
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 3, message = "请先删除版本内内容" });
            }
        }
        #endregion
        #region 根据版本ID查询版本信息
        /// <summary>
        /// 根据版本ID查询版本信息
        /// </summary>
        /// <param name="id">版本ID</param>
        /// <returns></returns>
        [HttpPost("selectversionbyid")]
        public IActionResult SelectVersionById(int id)
        {
            return new JsonResult(new { result = 1, message = "", data = _IUploadFile.selectVersionById(id) }, new Newtonsoft.Json.JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
        }
        #endregion
        #region 修改版本
        /// <summary>
        /// 修改版本
        /// </summary>
        /// <param name="id">版本id</param>
        /// <param name="versionNumber">版本号</param>
        /// <param name="releaseTime">修改时间</param>
        /// <param name="publisher">发布人</param>
        /// <param name="description">版本描述</param>
        /// <returns></returns>
        [HttpPost("updateversion")]
        public IActionResult updateVersion(int id, string versionNumber, DateTime releaseTime, string publisher, string description)
        {
            var version = new Versions();
            version.Id = id;
            version.VersionNumber = versionNumber;
            version.ReleaseTime = releaseTime;
            version.Publisher = publisher;
            version.Description = description;
            if (_IUploadFile.updateVersion(version))
            {
                return new JsonResult(new { result = 1, message = "修改成功" });
            }
            else
            {
                return new JsonResult(new { result = 0, message = "修改失败" });
            }
        }
        #endregion
        #region 根据产品ID删除产品
        /// <summary>
        /// 根据产品ID删除产品
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("deleteproduct")]
        public IActionResult DeleteProduct(int id, int versionId)
        {
            try
            {
                if (_IUploadFile.deleteProduct(id))
                {
                    return new JsonResult(new { result = 1, message = "移除成功" });
                }
                else
                {
                    return new JsonResult(new { result = 0, message = "移除失败" });
                }
            }
            catch (Exception exc)
            {
                return new JsonResult(new { result = 2, message = "请先删除小版本" });
            }
        }
        #endregion
        #region 根据产品ID查询产品信息
        /// <summary>
        /// 根据产品ID查询产品信息
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        [HttpPost("selectproductbyid")]
        public IActionResult SelectProductById(int id)
        {
            return new JsonResult(new { result = 1, message = "", data = _IUploadFile.selectProductsById(id) });
        }
        #endregion
        #region 修改产品
        /// <summary>
        /// 修改产品
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <param name="productName">产品名称</param>
        /// <param name="description">产品描述</param>
        /// <returns></returns>
        [HttpPost("updateproduct")]
        public IActionResult updateProduct(int id, string productName, string description)
        {
            var product = new Products();
            product.Id = id;
            product.ProductName = productName;
            product.Description = description;
            if (_IUploadFile.updateProduct(product))
            {
                return new JsonResult(new { result = 1, message = "修改成功" });
            }
            else
            {
                return new JsonResult(new { result = 0, message = "修改失败" });
            }
        }
        #endregion
        #endregion



    }
}