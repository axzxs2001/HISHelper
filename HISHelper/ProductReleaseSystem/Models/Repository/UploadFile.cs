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
            var sql = @"insert into Files(FileName,UploadTime,VersionsID,FilePath)
                values(@FileName,@UploadTime,@VersionsId,@filePath)";
            var pars = new List<SqlParameter>();
            var par1 = new SqlParameter() { ParameterName = "@FileName", SqlDbType = System.Data.SqlDbType.VarChar, Value = files.FileName };
            var par2 = new SqlParameter() { ParameterName = "@UploadTime", SqlDbType = System.Data.SqlDbType.DateTime, Value = files.UploadTime };
            var par4 = new SqlParameter() { ParameterName = "@VersionsId", SqlDbType = System.Data.SqlDbType.VarChar, Value = files.VersionsId };
            var par5 = new SqlParameter() { ParameterName = "@filePath", SqlDbType = System.Data.SqlDbType.VarChar, Value = files.FilePath };
            return _dbHelper.SavaData(sql, par1, par2, par4, par5) > 0 ? true : false;
        }

        #region 部门信息
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
            var par = new SqlParameter() { ParameterName = "@DepartmentName", SqlDbType = System.Data.SqlDbType.VarChar, Value = departmentName };
            return _dbHelper.SavaData(sql, par) > 0 ? true : false;
        }

        /// <summary>
        /// 查询所有部门ID和名称
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> QueryDepartments()
        {
            var sql = "SELECT ID,DepartmentName FROM dbo.Departments";
            return _dbHelper.GetList(sql);
        }
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteDepartments(int id)
        {
            var sql = @"DELETE dbo.Departments WHERE ID=@id
";
            var par = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.SavaData(sql, par) > 0 ? true : false;
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateDepartment(int id, string departmentName)
        {
            var sql = @"UPDATE dbo.Departments SET DepartmentName=@departmentName WHERE ID=@id";
            var par1 = new SqlParameter() { ParameterName = "@departmentName", SqlDbType = System.Data.SqlDbType.VarChar, Value = departmentName };
            var par = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = id };

            return _dbHelper.SavaData(sql, par1, par) > 0 ? true : false;
        }
        #endregion

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
            var par1 = new SqlParameter() { ParameterName = "@name", SqlDbType = System.Data.SqlDbType.VarChar, Value = developer.Name };
            var par2 = new SqlParameter() { ParameterName = "@sex", SqlDbType = System.Data.SqlDbType.Bit, Value = developer.Sex };
            var par3 = new SqlParameter() { ParameterName = "@qq", SqlDbType = System.Data.SqlDbType.VarChar, Value = developer.Qq };
            var par4 = new SqlParameter() { ParameterName = "@email", SqlDbType = System.Data.SqlDbType.VarChar, Value = developer.Email };
            var par5 = new SqlParameter() { ParameterName = "@phone", SqlDbType = System.Data.SqlDbType.VarChar, Value = developer.Phone };
            var par6 = new SqlParameter() { ParameterName = "@departmentID", SqlDbType = System.Data.SqlDbType.Int, Value = developer.DepartmentId };
            var par7 = new SqlParameter() { ParameterName = "@remarks", SqlDbType = System.Data.SqlDbType.VarChar, Value = developer.Remarks };
            return _dbHelper.SavaData(sql, par1, par2, par3, par4, par5, par6, par7) > 0 ? true : false;
        }

        /// <summary>
        /// 查询开发人员信息
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> QueryDevelopers()
        {
            var sql = @"SELECT  a.ID,
b.DepartmentName,
b.ID as departmentid,
        Name,
        Sex,
        qq,
        Email,
        Phone,
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
        Remarks=@remarks,
        DepartmentID = @departmentID
WHERE   ID=@Id";
            var par1 = new SqlParameter() { ParameterName = "@name", SqlDbType = System.Data.SqlDbType.VarChar, Value = developer.Name };
            var par2 = new SqlParameter() { ParameterName = "@sex", SqlDbType = System.Data.SqlDbType.Int, Value = developer.Sex };
            var par3 = new SqlParameter() { ParameterName = "@qq", SqlDbType = System.Data.SqlDbType.VarChar, Value = developer.Qq };
            var par4 = new SqlParameter() { ParameterName = "@email", SqlDbType = System.Data.SqlDbType.VarChar, Value = developer.Email };
            var par5 = new SqlParameter() { ParameterName = "@phone", SqlDbType = System.Data.SqlDbType.VarChar, Value = developer.Phone };
            var par6 = new SqlParameter() { ParameterName = "@remarks", SqlDbType = System.Data.SqlDbType.VarChar, Value = developer.Remarks };
            var par7 = new SqlParameter() { ParameterName = "@departmentID", SqlDbType = System.Data.SqlDbType.Int, Value = developer.DepartmentId };
            var par8 = new SqlParameter() { ParameterName = "@Id", SqlDbType = System.Data.SqlDbType.Int, Value = developer.Id };

            return _dbHelper.SavaData(sql, par1, par2, par3, par4, par5, par6, par7, par8) > 0 ? true : false;
        }

        /// <summary>
        /// 通过部门ID删除开发人员信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public bool DeleteDeveloper(int id)
        {
            var sql = @"DELETE dbo.Developers WHERE ID=@id";
            var par = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.SavaData(sql, par) > 0 ? true : false;
        }

        


        #endregion

        #region 维护用户表
        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public bool AddUser(Users user)
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
        public bool UpdataUser(Users user)
        {
            var sql = @"UPDATE  dbo.Users
SET     UserName = @username ,
        PassWord = @password,
        Character = @character
WHERE   ID = @id";
            var par1 = new SqlParameter() { ParameterName = "@username", SqlDbType = System.Data.SqlDbType.VarChar, Value = user.UserName };
            var par2 = new SqlParameter() { ParameterName = "@password", SqlDbType = System.Data.SqlDbType.VarChar, Value = user.PassWord };
            var par3 = new SqlParameter() { ParameterName = "@character", SqlDbType = System.Data.SqlDbType.VarChar, Value = user.Character };
            var par4 = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = user.Id };
            return _dbHelper.SavaData(sql, par1, par2,par3,par4) > 0 ? true : false;
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
            var par = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.SavaData(sql, par) > 0 ? true : false;
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

        /// <summary>
        /// 通过姓名查询用户ID
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> InsertUsers(string name)
        {
            var sql = @"SELECT  ID ,
        UserName ,
        PassWord ,
        Character
FROM    dbo.Users where UserName=@username";
            var par1 = new SqlParameter() { ParameterName = "@username", SqlDbType = System.Data.SqlDbType.VarChar, Value = name };
            return _dbHelper.GetList(sql,par1);
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <returns></returns>
        public List< Dictionary<string,dynamic>> SelectUsers(string username,string password)
        {
            var sql = @"SELECT  ID ,
        UserName ,
        PassWord ,
        Character
FROM    dbo.Users
where UserName=@username and PassWord=@password";
            var par1 = new SqlParameter() {ParameterName= "@username",SqlDbType=System.Data.SqlDbType.VarChar,Value=username };
            var par2 = new SqlParameter() { ParameterName= "@password",SqlDbType=System.Data.SqlDbType.VarChar,Value=password };
            return _dbHelper.GetList(sql,par1,par2);
        }
        #endregion

        /// <summary>
        /// 通过版本ID删除相关人员
        /// </summary>
        /// <param name="id"></param>版本ID
        /// <returns></returns>
        public bool DeleteRelatedPersonnels(int id)
        {
            var sql = @"delete RelatedPersonnels where VersionID=@id";
            var par = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.SavaData(sql, par) > 0 ? true : false;
        }
        /// <summary>
        /// 根据成员ID查询成员信息
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string,dynamic>> QueryDeveloper(int id)
        {
            var sql = (@"SELECT a.ID ID ,
                        Name ,
                        Sex ,
                        qq ,
                        Email ,
                        Phone ,
                        Remarks,
                        DepartmentName
                        FROM  dbo.Developers a join
                        dbo.Departments b on a.DepartmentID=b.ID 
                        where a.id=@ID
        ");
            var par1 = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };

            return _dbHelper.GetList(sql,par1);
        }

        /// <summary>
        /// 查询所有开发人员
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string,dynamic>> Querykf()
        {
            var sql = @"select ID,UserName from Users where Character=2";
            return _dbHelper.GetList(sql);
        }


        /// <summary>
        /// 添加相关人员
        /// </summary>
        /// <param name="relatedPersonnels"></param>
        /// <returns></returns>
        public bool addRelatedPersonnels(RelatedPersonnels relatedPersonnels)
        {
            var sql = @"insert into RelatedPersonnels(VersionID,PersonID,Personneltype) values(@VersionID,@PersonID,@Personneltype)";
            var par1 = new SqlParameter()
            {
                ParameterName = "@VersionID",
                SqlDbType = System.Data.SqlDbType.Int

,
                Value = relatedPersonnels.VersionId
            };
            var par2 = new SqlParameter()
            {
                ParameterName = "@PersonID",
                SqlDbType = System.Data.SqlDbType.Int

,
                Value = relatedPersonnels.PersonId
            };
            var par3 = new SqlParameter() { ParameterName = "@Personneltype", SqlDbType = System.Data.SqlDbType.VarChar, Value = relatedPersonnels.Personneltype };
            return _dbHelper.SavaData(sql, par1, par2, par3) > 0 ? true : false;
        }

        /// <summary>
        /// 根据版本号查询所有文件
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string,dynamic>> QueryAllFiles(int id)
        {
            var sql = @"select ID,FileName,UploadTime from Files where VersionsID=@VersionsID";
            var par1 = new SqlParameter() { ParameterName = "@VersionsID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetList(sql, par1);
        }

        /// <summary>
        /// 查询所有相关人员信息
        /// </summary>
        /// <param name="id"></param>版本ID
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> QueryRelatedPersonnels(int id)
        {
            var sql = @"select
a.id,
b.DepartmentName,
a.Name,
a.Sex,
a.Phone,
a.QQ,
a.Email,
c.Personneltype
from Developers a 
join Departments b on
a.DepartmentID=b.ID 
join RelatedPersonnels c 
on a.ID=c.PersonID 
join Versions d 
on c.VersionID=d.ID 
where c.VersionID=@id";
            var par1 = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetList(sql, par1);
        }
        /// <summary>
        /// 修改人员为负责人
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns></returns>
        public bool UpdatePersonType(int? id)
        {
            var sql = @"update RelatedPersonnels set Personneltype='管理员' where PersonID=@id";
            var par = new SqlParameter() { ParameterName= "@id",SqlDbType=System.Data.SqlDbType.Int,Value=id} ;
            return _dbHelper.SavaData(sql, par) > 0 ? true : false;


        }

        /// <summary>
        /// 根据人员ID删除相关人员
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns></returns>
        public bool deleteRp(int id)
        {
            var sql = @"delete RelatedPersonnels where PersonID=@id";
            var par = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.SavaData(sql, par) > 0 ? true : false;
        }

        /// <summary>
        /// 通过部门查询开发人员
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> SelectDevelopers(int departmentID)
        {
            var sql = (@"SELECT ID ID ,
        Name ,
        Sex ,
        qq ,
        Email ,
        Phone ,
     
        Remarks
FROM    dbo.Developers
where DepartmentID=@DepartmentID
");
            var par1 = new SqlParameter()
            {
                ParameterName = "@DepartmentID",
                SqlDbType = System.Data.SqlDbType.Int

,
                Value = departmentID
            };

            return _dbHelper.GetList(sql, par1);
        }

        /// <summary>
        /// 修改版本信息
        /// </summary>
        /// <param name="version">版本实体类</param>
        /// <returns></returns>
        public bool updateVersion(Versions version)
        {
            var sql = @"update Versions set VersionNumber=@VersionNumber,ReleaseTime=@ReleaseTime,publisher=@publisher,Description=@Description where ID=@ID";
            var par1 = new SqlParameter() { ParameterName = "@VersionNumber", SqlDbType = System.Data.SqlDbType.VarChar, Value = version.VersionNumber };
            var par2 = new SqlParameter() { ParameterName = "@ReleaseTime", SqlDbType = System.Data.SqlDbType.DateTime, Value = version.ReleaseTime };
            var par3 = new SqlParameter() { ParameterName = "@publisher", SqlDbType = System.Data.SqlDbType.VarChar, Value = version.Publisher };
            var par4 = new SqlParameter() { ParameterName = "@Description", SqlDbType = System.Data.SqlDbType.VarChar, Value = version.Description };
            var par5 = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = version.Id };
            return _dbHelper.SavaData(sql, par1, par2, par3, par4, par5) > 0 ? true : false;
        }


        /// <summary>
        /// 根据版本ID查询版本信息
        /// </summary>
        /// <param name="id">版本ID</param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> selectVersionById(int id)
        {
            var sql = @"select VersionNumber,ReleaseTime,publisher,Description from Versions where ID=@ID";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetList(sql, par);
        }

        /// <summary>
        /// 根据版本ID删除版本
        /// </summary>
        /// <param name="id">版本ID</param>
        /// <returns></returns>
        public bool deleteVersion(int id)
        {
            var sql = @"delete from Versions where ID=@ID";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.SavaData(sql, par) > 0 ? true : false;
        }

        /// <summary>
        /// 根据产品ID删除产品
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        public bool deleteProduct(int id)
        {
            var sql = @"delete from Products where ID=@ID";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.SavaData(sql, par) > 0 ? true : false;
        }

        /// <summary>
        /// 修改产品信息
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public bool updateProduct(Products products)
        {
            var sql = @"update Products set ProductName=@ProductName,Description=@Description where ID=@ID";
            var par1 = new SqlParameter() { ParameterName = "@ProductName", SqlDbType = System.Data.SqlDbType.VarChar, Value = products.ProductName };
            var par2 = new SqlParameter() { ParameterName = "@Description", SqlDbType = System.Data.SqlDbType.VarChar, Value = products.Description };
            var par3 = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = products.Id };
            return _dbHelper.SavaData(sql, par1, par2, par3) > 0 ? true : false;
        }

        /// <summary>
        /// 根据产品ID查询产品信息
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> selectProductsById(int id)
        {
            var sql = @"select ProductName,Description from Products where ID=@ID";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetList(sql, par);
        }

        /// <summary>
        /// 根据文件ID删除文件
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <returns></returns>
        public bool deleteFile(int id)
        {
            var sql = @"delete from Files where ID=@ID";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.SavaData(sql, par) > 0 ? true : false;
        }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <returns></returns>
        public object getFilePath(int id)
        {
            var sql = @"select FilePath from Files where ID=@ID";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetValue(sql, par);
        }
    }
  
}
