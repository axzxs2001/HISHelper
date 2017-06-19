using ProductReleaseSystem.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductReleaseSystem.ProductRelease;
using System.Data.SqlClient;

namespace ProductReleaseSystem.Models.Repository
{
    public partial class UploadFile : IUploadFile
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
            var par = new SqlParameter() { ParameterName = "@ProductName", SqlDbType = System.Data.SqlDbType.VarChar, Value = products.ProductName };
            var par2 = new SqlParameter() { ParameterName = "@Description", SqlDbType = System.Data.SqlDbType.VarChar, Value = products.Description };
            return _dbHelper.SavaData(sql, par, par2) > 0 ? true : false;
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
            var par1 = new SqlParameter() { ParameterName = "@VersionNumber", SqlDbType = System.Data.SqlDbType.VarChar, Value = versions.VersionNumber };
            var par2 = new SqlParameter() { ParameterName = "@ReleaseTime", SqlDbType = System.Data.SqlDbType.DateTime, Value = versions.ReleaseTime };
            var par3 = new SqlParameter() { ParameterName = "@Publisher", SqlDbType = System.Data.SqlDbType.VarChar, Value = versions.Publisher };
            var par4 = new SqlParameter() { ParameterName = "@ProductId", SqlDbType = System.Data.SqlDbType.Int, Value = versions.ProductId };
            var par5 = new SqlParameter() { ParameterName = "@Description", SqlDbType = System.Data.SqlDbType.VarChar, Value = versions.Description };
            return _dbHelper.SavaData(sql, par1, par2, par3, par4, par5) > 0 ? true : false;
        }

        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public bool addFiles(Files files)
        {
            var sql = @"insert into Files(FileName,UploadTime,UploadPeople,VersionsID,FilePath)
                values(@FileName,@UploadTime,@UploadPeople,@VersionsId,@filePath)";
            var pars = new List<SqlParameter>();
            var par1 = new SqlParameter() { ParameterName = "@FileName", SqlDbType = System.Data.SqlDbType.VarChar, Value = files.FileName };
            var par2 = new SqlParameter() { ParameterName = "@UploadTime", SqlDbType = System.Data.SqlDbType.DateTime, Value = files.UploadTime };
            var par3 = new SqlParameter() { ParameterName = "@UploadPeople", SqlDbType = System.Data.SqlDbType.VarChar, Value = files.UploadPeople };
            var par4 = new SqlParameter() { ParameterName = "@VersionsId", SqlDbType = System.Data.SqlDbType.VarChar, Value = files.VersionsId };
            var par5 = new SqlParameter() { ParameterName = "@filePath", SqlDbType = System.Data.SqlDbType.VarChar, Value = files.FilePath };
            return _dbHelper.SavaData(sql, par1, par2, par3,par4, par5) > 0 ? true : false;
        }


        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="departmentName">部门名称</param>
        /// <returns></returns>
        public bool AddDepartment(string departmentName)
        {
            var sql = @"INSERT INTO dbo.Departments
        (DepartmentName)
VALUES(@DepartmentName)";
            var par = new SqlParameter() { ParameterName = "@DepartmentName",SqlDbType=System.Data.SqlDbType.VarChar,Value=departmentName };
            return _dbHelper.SavaData(sql, par) > 0 ? true : false;
        }

        /// <summary>
        /// 查询所有部门ID和名称
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>>QueryDepartments()
        {
            var sql = "SELECT ID,DepartmentName FROM dbo.Departments";
            return _dbHelper.GetList(sql);
        }

        #region 维护开发人员信息
        /// <summary>
        /// 添加开发人员
        /// </summary>
        /// <param name="developer">开发人员信息</param>
        /// <returns></returns>
        public bool AddPerson(Developers developer)
        {
            var sql = @"INSERT INTO dbo.Developers
        ( Name ,
          Sex ,
          QQ ,
          Email ,
          Phone ,
          DepartmentID,
           remarks
        )
VALUES  ( @name,@sex,@qq,@email,@phone,@departmentID,@remarks
        )";
            var par1 = new SqlParameter() { ParameterName= "@name",SqlDbType=System.Data.SqlDbType.VarChar,Value=developer.Name };
            var par2 = new SqlParameter() { ParameterName = "@sex", SqlDbType = System.Data.SqlDbType.Bit, Value = developer.Sex };
            var par3 = new SqlParameter() { ParameterName= "@qq",SqlDbType=System.Data.SqlDbType.VarChar,Value=developer.Qq };
            var par4 = new SqlParameter() { ParameterName= "@email",SqlDbType=System.Data.SqlDbType.VarChar,Value=developer.Email };
            var par5 = new SqlParameter() { ParameterName= "@phone",SqlDbType=System.Data.SqlDbType.VarChar,Value=developer.Phone };
            var par6 = new SqlParameter() { ParameterName= "@departmentID",SqlDbType=System.Data.SqlDbType.Int,Value=developer.DepartmentId };
            var par7 = new SqlParameter() { ParameterName = "@remarks", SqlDbType = System.Data.SqlDbType.VarChar, Value = developer.Remarks };
            return _dbHelper.SavaData(sql,par1,par2,par3,par4,par5,par6,par7)>0?true:false;
        }

        /// <summary>
        /// 通过部门ID获取该部门下的所有开发人员信息
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        public List<Dictionary<string,dynamic>> QueryDevelopers()
        {
            var sql = @"SELECT  a.ID ID ,
        Name ,
        Sex ,
        qq ,
        Email ,
        Phone ,
        DepartmentName,
        Remarks
FROM    dbo.Developers a JOIN dbo.Departments b
ON a.DepartmentID=b.ID 
";
           
            return _dbHelper.GetList(sql);
        }

        /// <summary>
        /// 修改开发人员信息
        /// </summary>
        /// <param name="developer">要修改的开发人员信息</param>
        /// <returns></returns>
        public bool UpdateDeveloper(Developers developer)
        {
            var sql = @"UPDATE  dbo.Developers
SET     Name = @name ,
        Sex = @sex ,
        QQ = @qq ,
        Email = @email ,
        Phone = @phone,
        Remarks=@remarks
WHERE   DepartmentID = @departmentID";
            var par1 = new SqlParameter() { ParameterName= " @name",SqlDbType=System.Data.SqlDbType.VarChar,Value=developer.Name };
            var par2 = new SqlParameter() { ParameterName = "@sex", SqlDbType = System.Data.SqlDbType.Int, Value = developer.Sex };
            var par3 = new SqlParameter() { ParameterName = "@qq", SqlDbType = System.Data.SqlDbType.VarChar, Value = developer.Qq };
            var par4= new SqlParameter() { ParameterName = "@email", SqlDbType = System.Data.SqlDbType.VarChar, Value = developer.Email };
            var par5= new SqlParameter() { ParameterName = "@phone", SqlDbType = System.Data.SqlDbType.VarChar, Value = developer.Phone };
            var par6 = new SqlParameter() { ParameterName = "@@remarks", SqlDbType = System.Data.SqlDbType.VarChar, Value = developer.Remarks };
            var par7 = new SqlParameter() { ParameterName = "@departmentID", SqlDbType = System.Data.SqlDbType.Int, Value = developer.DepartmentId };
          
            return _dbHelper.SavaData(sql,par1,par2,par3,par4,par5,par6,par7)>0?true:false;
        }

        /// <summary>
        /// 通过部门ID删除开发人员信息
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        public bool DeleteDeveloper(int departmentID) {
            var sql = @"DELETE dbo.Developers WHERE DepartmentID=@departmentID";
            var par = new SqlParameter() { ParameterName= "@departmentID",SqlDbType=System.Data.SqlDbType.Int,Value=departmentID };
            return _dbHelper.SavaData(sql,par)>0?true:false;
        }


        #endregion

        #region 维护用户表
       /// <summary>
       /// 新建用户
       /// </summary>
       /// <param name="user">用户信息</param>
       /// <returns></returns>
        public bool AddUser(ProductRelease.Users user)
        {
            var sql = @"INSERT INTO dbo.Users
        ( UserName, PassWord, Character )
VALUES  ( @username,@password,@character
          )";
            var par1 = new SqlParameter() { ParameterName = "@username", SqlDbType = System.Data.SqlDbType.VarChar, Value = user.UserName };
            var par2 = new SqlParameter() { ParameterName = "@password", SqlDbType = System.Data.SqlDbType.VarChar, Value = user.PassWord };
            var par3 = new SqlParameter() { ParameterName = "@character", SqlDbType = System.Data.SqlDbType.VarChar, Value = user.Character };
            return _dbHelper.SavaData(sql, par1, par2, par3) > 0 ? true : false;
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public bool UpdataUser(ProductRelease.Users user)
        {
            var sql = @"UPDATE  dbo.Users
SET     UserName = @username ,
        PassWord = @password ,
        Character = @character
WHERE   ID = @id";
            var par1 = new SqlParameter() { ParameterName= "@username",SqlDbType=System.Data.SqlDbType.VarChar,Value=user.UserName };
            var par2 = new SqlParameter() { ParameterName= "@password",SqlDbType=System.Data.SqlDbType.VarChar,Value=user.PassWord };
            var par3 = new SqlParameter() { ParameterName= "@character",SqlDbType=System.Data.SqlDbType.VarChar,Value=user.Character };
            var par4 = new SqlParameter() { ParameterName="@id",SqlDbType=System.Data.SqlDbType.Int,Value=user.Id};
            return _dbHelper.SavaData(sql, par1, par2, par3) > 0 ? true : false;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户表主键</param>
        /// <returns></returns>
        public bool DeleteUser(int id)
        {
            var sql = @"DELETE dbo.Users WHERE ID=@id
";
            var par = new SqlParameter() { ParameterName= "@id",SqlDbType=System.Data.SqlDbType.Int,Value=id };
            return _dbHelper.SavaData(sql,par)>0?true:false;
        }
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> QueryUsers()
        {
            var sql = @"SELECT  ID ,
        UserName ,
        PassWord ,
        Character
FROM    dbo.Users";
            return _dbHelper.GetList(sql);
        }
        #endregion


    }
}
