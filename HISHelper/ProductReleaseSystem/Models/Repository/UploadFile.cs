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
            var sql = "insert into Products (ProductName,Description) values(@ProductName,@Description)";
            //var pars = new List<SqlParameter>();
            var  par=new SqlParameter() { ParameterName = "@ProductName", SqlDbType = System.Data.SqlDbType.VarChar, Value = products.ProductName };
            var par2=new SqlParameter() { ParameterName = "@Description", SqlDbType = System.Data.SqlDbType.VarChar, Value = products.Description };
            return _dbHelper.SavaData(sql, par,par2) > 0 ? true : false;
        }

        /// <summary>
        /// 添加版本
        /// </summary>
        /// <param name="versions"></param>
        /// <returns></returns>
        public bool addVersions(Versions versions)
        {
            var sql = ("insert into Versions(VersionNumber,ReleaseTime,publisher,ProductID,Description) " +
                "values(" +
                "@VersionNumber," +
                "@ReleaseTime," +
                "@Publisher," +
                "@ProductId," +
                "@Description)");
           // var pars = new List<SqlParameter>();
            var par1=new SqlParameter() { ParameterName = "@VersionNumber", SqlDbType = System.Data.SqlDbType.VarChar, Value = versions.VersionNumber };
            var par2=new SqlParameter() { ParameterName = "@ReleaseTime", SqlDbType = System.Data.SqlDbType.DateTime, Value = versions.ReleaseTime };
           var par3=new SqlParameter() { ParameterName = "@Publisher", SqlDbType = System.Data.SqlDbType.VarChar, Value = versions.Publisher };
            var par4=new SqlParameter() { ParameterName = "@ProductId", SqlDbType = System.Data.SqlDbType.Int, Value = versions.ProductId };
           var par5=new SqlParameter() { ParameterName = "@Description", SqlDbType = System.Data.SqlDbType.VarChar, Value = versions.Description };
            return _dbHelper.SavaData(sql, par1,par2,par3,par4,par5) > 0 ? true : false;
        }

        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public bool addFiles(Files files)
        {
            var sql = @"insert into Files(FileName,UploadTime,UploadPeople,VersionsID)
                values(@FileName,@UploadTime,@UploadPeople,@VersionsId)";
            var pars = new List<SqlParameter>();
            var par1=new SqlParameter() { ParameterName = "@FileName", SqlDbType = System.Data.SqlDbType.VarChar, Value = files.FileName };
            var par2=new SqlParameter() { ParameterName = "@UploadTime", SqlDbType = System.Data.SqlDbType.DateTime, Value = files.UploadTime };
            var par3=new SqlParameter() { ParameterName = "@UploadPeople", SqlDbType = System.Data.SqlDbType.VarChar, Value = files.UploadPeople };
           var par4=new SqlParameter() { ParameterName = "@VersionsId", SqlDbType = System.Data.SqlDbType.VarChar, Value = files.VersionsId };
            return _dbHelper.SavaData(sql,par1,par2,par3,par4) > 0 ? true : false;
        }

    }
}
