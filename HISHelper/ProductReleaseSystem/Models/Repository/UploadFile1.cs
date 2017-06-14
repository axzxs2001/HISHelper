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
        /// 查询指定版本下的文件名
        /// </summary>
        /// <param name="versionsID">版本号</param>
        public List<Dictionary<string, dynamic>> GetFilesByID(int versionsID)
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
    }
}
