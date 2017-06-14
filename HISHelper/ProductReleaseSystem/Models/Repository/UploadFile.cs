using ProductReleaseSystem.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductReleaseSystem.ProductRelease;
using System.Data.SqlClient;

namespace ProductReleaseSystem.Models.Repository
{
    public partial class UploadFile:IUploadFile
    {

        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public bool addProduct(Products products)
        {
            var sql = ("insert into Products(ProductName,Description) values(@products.ProductName,@products.Description)");
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter() { ParameterName = "@products.ProductName", SqlDbType = System.Data.SqlDbType.Int, Value = products.ProductName });
            pars.Add(new SqlParameter() { ParameterName = "@products.Description", SqlDbType = System.Data.SqlDbType.VarChar, Value = products.Description });
            return _dbHelper.SavaData(sql, pars.ToArray()) > 0 ? true : false;
        }

        /// <summary>
        /// 添加版本
        /// </summary>
        /// <param name="versions"></param>
        /// <returns></returns>
        public bool addVersions(Versions versions)
        {
            var sql = ("insert into Versions(VersionNumber,ReleaseTime,publisher，ProductID，Description) " +
                "values(" +
                "@versions.VersionNumber," +
                "@versions.ReleaseTime," +
                "@versions.Publisher," +
                "@versions.ProductId," +
                "@versions.Description)");
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter() { ParameterName = "@versions.VersionNumber", SqlDbType = System.Data.SqlDbType.Int, Value = versions.VersionNumber });
            pars.Add(new SqlParameter() { ParameterName = "@versions.ReleaseTime", SqlDbType = System.Data.SqlDbType.VarChar, Value = versions.ReleaseTime });
            pars.Add(new SqlParameter() { ParameterName = "@versions.Publisher", SqlDbType = System.Data.SqlDbType.VarChar, Value = versions.Publisher });
            pars.Add(new SqlParameter() { ParameterName = "@versions.ProductId", SqlDbType = System.Data.SqlDbType.VarChar, Value = versions.ProductId });
            pars.Add(new SqlParameter() { ParameterName = "@versions.Description", SqlDbType = System.Data.SqlDbType.VarChar, Value = versions.Description });
            return _dbHelper.SavaData(sql, pars.ToArray()) > 0 ? true : false;
        }

        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public bool addFiles(Files files)
        {
            var sql = ("insert into Files(FileName,UploadTime,UploadPeople,VersionsID) " +
                "values(" +
                "@files.FileName," +
                "@files.UploadTime, " +
                "@files.UploadPeople," +
                "@files.VersionsId)");
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter() { ParameterName = "@files.FileName", SqlDbType = System.Data.SqlDbType.Int, Value = files.FileName });
            pars.Add(new SqlParameter() { ParameterName = "@files.UploadTime", SqlDbType = System.Data.SqlDbType.VarChar, Value = files.UploadTime });
            pars.Add(new SqlParameter() { ParameterName = "@files.UploadPeople", SqlDbType = System.Data.SqlDbType.VarChar, Value = files.UploadPeople });
            pars.Add(new SqlParameter() { ParameterName = "@files.VersionsId", SqlDbType = System.Data.SqlDbType.VarChar, Value = files.VersionsId });
            return _dbHelper.SavaData(sql, pars.ToArray()) > 0 ? true : false;
        }

    }
}
