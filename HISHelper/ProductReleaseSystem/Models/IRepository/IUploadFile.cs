using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductReleaseSystem.ProductRelease;
namespace ProductReleaseSystem.Models.IRepository
{
    public partial interface IUploadFile
    {
        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        bool addProduct(Products products);


        /// <summary>
        /// 根据人员ID查询所有相关人员
        /// </summary>
        /// <param name="id"></param>人员ID
        /// <returns></returns>
        List<Dictionary<string, dynamic>> QueryDeveloper(int id);

        /// <summary>
        /// 添加版本
        /// </summary>
        /// <param name="versions"></param>
        /// <returns></returns>
        bool addVersions(Versions versions);

        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        bool addFiles(Files files);
        #region 部门信息
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="departmentName">部门名称</param>
        /// <returns></returns>
        bool AddDepartment(string departmentName);
        /// <summary>
        /// 查询所有部门
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> QueryDepartments();
        /// <summary>
        /// 查询所有权限
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> AuthorityTable();
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id">部门</param>
        /// <returns></returns>
        bool DeleteDepartments(int id);
        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="departmentName">部门名称</param>
        /// <returns></returns>
        bool UpdateDepartment(int id,string departmentName);
        #endregion

        /// <summary>
        /// 根据版本ID删除相关人员
        /// </summary>
        /// <param name="id"></param>版本ID
        /// <returns></returns>
        bool DeleteRelatedPersonnels(int id);

        #region  维护开发人员信息
        /// <summary>
        /// 添加人员
        /// </summary>
        /// <returns></returns>
        bool AddPerson(Developers developer);

        /// <summary>
        /// 获取开发人员信息
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> QueryDevelopers();
        /// <summary>
        /// 通过部门查询人员
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> SelectDevelopers(int departmentID);

        /// <summary>
        /// 修改开发人员信息
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        bool UpdateDeveloper(Developers developer);
        /// <summary>
        /// 删除开发人员信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        bool DeleteDeveloper(int id);


        #endregion

        #region 维护用户表
        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        bool AddUser(Users user);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        bool UpdataUser(Users user);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        bool DeleteUser(int id);

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <returns></returns>
         List<Dictionary<string,dynamic>> QueryUsers();

        /// <summary>
        /// 通过姓名查询用户ID
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> InsertUsers(string name);


        #endregion

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <returns></returns>
        List <Dictionary<string,dynamic>> SelectUsers(string username,string password);

        /// <summary>
        /// 查询所有开发人员
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> Querykf();

        /// <summary>
        /// 根据版本号查询所有文件
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> QueryAllFiles(int id);

        /// <summary>
        /// 添加相关人员
        /// </summary>
        /// <param name="relatedPersonnels"></param>
        /// <returns></returns>
        bool addRelatedPersonnels(RelatedPersonnels relatedPersonnels);


        /// <summary>
        /// 根据版本号查询所有相关人员
        /// </summary>
        /// <param name="id"></param>版本号
        /// <returns></returns>
        List<Dictionary<string, dynamic>> QueryRelatedPersonnels(int id);
        /// <summary>
        /// 修改病人为管理员
        /// </summary>
        /// <param name="id">病人ID</param>
        /// <returns></returns>
        bool UpdatePersonType(int? id,int versionID);

        /// <summary>
        /// 根据人员ID删除相关人员
        /// </summary>
        /// <param name="id"></param>人员ID
        /// <returns></returns>
        bool deleteRp(int id,int versionID);

        /// <summary>
        /// 根据版本ID删除版本
        /// </summary>
        /// <param name="id">版本ID</param>
        /// <returns></returns>
        bool deleteVersion(int id);

        /// <summary>
        /// 修改版本信息
        /// </summary>
        /// <param name="version">版本实体类</param>
        /// <returns></returns>
        bool updateVersion(Versions version);

        /// <summary>
        /// 根据版本号查询版本
        /// </summary>
        /// <param name="id">版本ID</param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> selectVersionById(int id);

        /// <summary>
        /// 根据产品ID删除产品
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        bool deleteProduct(int id);

        /// <summary>
        /// 根据文件ID删除文件
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <returns></returns>
        bool deleteFile(int id);

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        object getFilePath(int id);

        /// <summary>
        /// 修改产品信息
        /// </summary>
        /// <param name="Products">产品实体类</param>
        /// <returns></returns>
        bool updateProduct(Products products);

        /// <summary>
        /// 根据产品ID查询产品信息
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        List<Dictionary<string, dynamic>> selectProductsById(int id);
    }
}
