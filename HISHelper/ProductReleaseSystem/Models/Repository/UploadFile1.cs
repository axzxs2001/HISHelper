using Microsoft.Extensions.Options;
using ProductReleaseSystem.Data;
using ProductReleaseSystem.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.Models.Repository
{
    /// <summary>
    /// 上传查询实体
    /// </summary>
    public partial class UploadFile:IUploadFile
    {
        /// <summary>
        /// 数据库操作对象
        /// </summary>
        DBHelper _dbHelper;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="connections"></param>
        public UploadFile(IOptions<ConnectionSetting> connections)
        {
            _dbHelper = new DBHelper(connections.Value.prConnectionStrings);
        }
        
        /// <summary>
        /// 查询所有产品列表
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> GetProductsList()
        {
            var sql = @"SELECT ID,ProductName,Description FROM dbo.Products";
            return _dbHelper.GetList(sql);

        }
        /// <summary>
        /// 查询指定产品的所有版本信息列表
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> GetVersionsByID(int productID)
        {
            var sql = @"SELECT  Versions.ID,
        ProductName 产品名 ,
        VersionNumber 版本号 ,
        publisher 发布人,
        ReleaseTime 发布时间
FROM    dbo.Versions
        JOIN dbo.Products ON Products.ID = Versions.ProductID
WHERE   ProductID = @ProductID";
            var par = new SqlParameter() { ParameterName = "@ProductID", SqlDbType = System.Data.SqlDbType.Int, Value = productID };
            return _dbHelper.GetList(sql, par);
        }

        /// <summary>
        /// 查询指定版本下的版本信息
        /// </summary>
        /// <param name="versionsID">版本id</param>
        public List<Dictionary<string, dynamic>> GetVersionInfo(int versionsID)
        {
            var sql = @"SELECT  ProductName 产品名 ,
        VersionNumber 版本号 ,
		Versions.Description 版本描述,
        publisher 发布人,
        ReleaseTime 发布时间
FROM    dbo.Versions
        JOIN dbo.Products ON Products.ID = Versions.ProductID
WHERE   Versions.ID = @VersionsID";
            var par = new SqlParameter() { ParameterName = "@VersionsID", SqlDbType = System.Data.SqlDbType.Int, Value = versionsID };
            return _dbHelper.GetList(sql, par);
        }
        /// <summary>
        /// 查询此版本下的相关人员
        /// </summary>
        /// <param name="versionsID"></param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> GetDevelopersByID(int versionsID)
        {
            var sql = @"SELECT  Departments.ID ,
                        Departments.DepartmentName ,
                        Developers.Name ,
                        Developers.QQ ,
                        Developers.Sex ,
                        Developers.Phone
                FROM    dbo.Versions
                        JOIN dbo.RelatedPersonnels ON Versions.id = RelatedPersonnels.VersionID
                        JOIN dbo.Developers ON RelatedPersonnels.PersonID = Developers.ID
                        JOIN Departments ON Departments.ID = Developers.DepartmentID
                WHERE   dbo.Versions.ID = @VersionsID";
            var par = new SqlParameter() { ParameterName = "@VersionsID", SqlDbType = System.Data.SqlDbType.Int, Value = versionsID };
            return _dbHelper.GetList(sql, par);
        }
        /// <summary>
        /// 查询指定版本下的文件信息
        /// </summary>
        /// <param name="versionsID"></param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> GetFilesByID(int versionsID)
        {
            var sql = @" SELECT  Files.FileName ,
                        Files.UploadTime ,
                        Files.UploadPeople
                FROM    Versions
                        JOIN dbo.Files ON Files.VersionsID = Versions.ID
                WHERE   Versions.ID =@VersionsID";
            var par = new SqlParameter() { ParameterName = "@VersionsID", SqlDbType = System.Data.SqlDbType.Int, Value = versionsID };
            return _dbHelper.GetList(sql, par);
        }
    }
}
