using ProductReleaseSystem.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductReleaseSystem.ProductRelease;
using System.Data.SqlClient;
using ProductReleaseSystem.Data;
using Microsoft.Extensions.Options;

namespace ProductReleaseSystem.Models.Repository
{
    public class Demand : IDemand
    {
        /// <summary>
        /// 数据库操作对象
        /// </summary> 
        DBHelper _dbHelper;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="connections"></param>
        public Demand(IOptions<ConnectionSetting> connections)
        {
            _dbHelper = new DBHelper(connections.Value.prConnectionStrings);
        }
        #region 实施上传需求信息
        #region 删除需求
        /// <summary>
        /// 删除需求
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteDemand(int ID)
        {
            var sql = @"delete RequestForm where ID=@id";
            var par = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = ID };
            return _dbHelper.SavaData(sql, par) > 0 ? true : false;
        }
        #endregion

        #region 添加需求
        /// <summary>
        /// 添加需求
        /// </summary>
        /// <param name="requestform">需求信息</param>
        /// <returns></returns>
        public bool InsertDemand(RequestForm requestform)
        {
            var sql = "insert into RequestForm (DemandNname,RequirementsDescription,Priority,UserName,Producer,ContactInformation,ImplementerID,MakeTime,VersionNumber,DeliveryDepartment,Status,ProductID,Address) values(@DemandNname,@RequirementsDescription,@Priority,@UserName,@Producer,@ContactInformation,@ImplementerID,@MakeTime,@VersionNumber,@DeliveryDepartment,@Status,@ProductID,@Address)";
            var par = new SqlParameter() { ParameterName = "@DemandNname", SqlDbType = System.Data.SqlDbType.VarChar, Value = requestform.DemandNname };
            var par2 = new SqlParameter() { ParameterName = "@RequirementsDescription", SqlDbType = System.Data.SqlDbType.Text, Value = requestform.RequirementsDescription };
            var par3 = new SqlParameter() { ParameterName = "@Priority", SqlDbType = System.Data.SqlDbType.VarChar, Value = requestform.Priority };
            var par4 = new SqlParameter() { ParameterName = "@UserName", SqlDbType = System.Data.SqlDbType.VarChar, Value = requestform.UserName };
            var par5 = new SqlParameter() { ParameterName = "@Producer", SqlDbType = System.Data.SqlDbType.VarChar, Value = requestform.Producer };
            var par6 = new SqlParameter() { ParameterName = "@ContactInformation", SqlDbType = System.Data.SqlDbType.VarChar, Value = requestform.ContactInformation };
            var par7 = new SqlParameter() { ParameterName = "@ImplementerID", SqlDbType = System.Data.SqlDbType.Int, Value = requestform.ImplementerID };
            var par8 = new SqlParameter() { ParameterName = "@MakeTime", SqlDbType = System.Data.SqlDbType.DateTime, Value = requestform.MakeTime };
            var par9 = new SqlParameter() { ParameterName = "@VersionNumber", SqlDbType = System.Data.SqlDbType.VarChar, Value = requestform.VersionNumber };
            var par10 = new SqlParameter() { ParameterName = "@DeliveryDepartment", SqlDbType = System.Data.SqlDbType.Int, Value = requestform.DeliveryDepartment };
            var par11 = new SqlParameter() { ParameterName = "@Status", SqlDbType = System.Data.SqlDbType.VarChar, Value = requestform.Status };
            var par12 = new SqlParameter() { ParameterName = "@ProductID", SqlDbType = System.Data.SqlDbType.VarChar, Value = requestform.ProductID };
            var par13 = new SqlParameter() { ParameterName = "@Address", SqlDbType = System.Data.SqlDbType.VarChar, Value = requestform.Address };
            return _dbHelper.SavaData(sql, par, par2, par3, par4, par5, par6, par7, par8, par9,par10,par11,par12, par13) > 0 ? true : false;
        }
        #endregion

        #region 查询需求全部信息
        /// <summary>
        /// 查询需求全部信息
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> SelectDemand()
        {
            var sql = $@"select a.ID,
DemandNname,
RequirementsDescription,
Priority,
UserName,
Producer,
ContactInformation,
Name,
MakeTime,
VersionNumber,
DeliveryDepartment,
Status,
ProductID,
Address
 from RequestForm a
 JOIN Developers b
 ON a.ImplementerID = b.ID";
            return _dbHelper.GetList(sql);
        }
        #endregion

        #region  修改需求
        /// <summary>
        /// 修改需求
        /// </summary>
        /// <param name="requestform"></param>
        /// <returns></returns>
        public bool UpdateDemand(RequestForm requestform)
        {
            var sql = @"UPDATE RequestForm SET 
DemandNname=@DemandNname,
RequirementsDescription=@RequirementsDescription,
Priority=@Priority,
UserName=@UserName,
Producer=@Producer,
ContactInformation=@ContactInformation,
ImplementerID=@ImplementerID,
MakeTime=@MakeTime,
VersionNumber=@VersionNumber,
DeliveryDepartment=@DeliveryDepartment,
Status=@Status,
ProductID=@ProductID,
Address=@Address
WHERE ID=@id";
            var par1 = new SqlParameter() { ParameterName = "@DemandNname", SqlDbType = System.Data.SqlDbType.VarChar, Value = requestform.DemandNname };
            var par2 = new SqlParameter() { ParameterName = "@RequirementsDescription", SqlDbType = System.Data.SqlDbType.Text, Value = requestform.RequirementsDescription };
            var par3 = new SqlParameter() { ParameterName = "@Priority", SqlDbType = System.Data.SqlDbType.VarChar, Value = requestform.Priority };
            var par4 = new SqlParameter() { ParameterName = "@UserName", SqlDbType = System.Data.SqlDbType.VarChar, Value = requestform.UserName };
            var par5 = new SqlParameter() { ParameterName = "@Producer", SqlDbType = System.Data.SqlDbType.VarChar, Value = requestform.Producer };
            var par6 = new SqlParameter() { ParameterName = "@ContactInformation", SqlDbType = System.Data.SqlDbType.Text, Value = requestform.ContactInformation };
            var par7 = new SqlParameter() { ParameterName = "@ImplementerID", SqlDbType = System.Data.SqlDbType.Int, Value = requestform.ImplementerID };
            var par8 = new SqlParameter() { ParameterName = "@MakeTime", SqlDbType = System.Data.SqlDbType.DateTime, Value = requestform.MakeTime };
            var par9 = new SqlParameter() { ParameterName = "@VersionNumber", SqlDbType = System.Data.SqlDbType.VarChar, Value = requestform.VersionNumber };
            var par10 = new SqlParameter() { ParameterName = "@DeliveryDepartment", SqlDbType = System.Data.SqlDbType.Int, Value = requestform.DeliveryDepartment };
            var par11 = new SqlParameter() { ParameterName = "@Status", SqlDbType = System.Data.SqlDbType.VarChar, Value = requestform.Status };
            var par12 = new SqlParameter() { ParameterName = "@ProductID", SqlDbType = System.Data.SqlDbType.VarChar, Value = requestform.ProductID };
            var par13 = new SqlParameter() { ParameterName = "@Address", SqlDbType = System.Data.SqlDbType.VarChar, Value = requestform.Address };
            var par = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = requestform.ID };

            return _dbHelper.SavaData(sql, par1, par2, par3, par4, par5, par6, par7, par8, par9,par10,par11,par12, par13,par) > 0 ? true : false;
        }
        #endregion
        #endregion



        #region 添加产品需求表
        /// <summary>
        ///添加产品需求表
        /// </summary>
        /// <param name="productdemand">产品需求添加信息</param>
        /// <returns></returns>
        public bool InsertProductDemand(ProductDemandTable productdemand)
        {
            var sql = "insert into ProductDemandTable (DemandID,DemandName,DemandModify,DemandPurpose,Priority,ProductID,ChangeTime,VersionNumber) values(@DemandID,@DemandName,@DemandModify,@DemandPurpose,@Priority,@ProductID,@ChangeTime,@VersionNumber)";
            var par = new SqlParameter() { ParameterName = "@DemandID", SqlDbType = System.Data.SqlDbType.Int, Value = productdemand.DemandID };
            var par2 = new SqlParameter() { ParameterName = "@DemandName", SqlDbType = System.Data.SqlDbType.VarChar, Value = productdemand.DemandName };
            var par3 = new SqlParameter() { ParameterName = "@DemandModify", SqlDbType = System.Data.SqlDbType.Text, Value = productdemand.DemandModify };
            var par4 = new SqlParameter() { ParameterName = "@DemandPurpose", SqlDbType = System.Data.SqlDbType.VarChar, Value = productdemand.DemandPurpose };
            var par5 = new SqlParameter() { ParameterName = "@Priority", SqlDbType = System.Data.SqlDbType.VarChar, Value = productdemand.Priority };
            var par6 = new SqlParameter() { ParameterName = "@ProductID", SqlDbType = System.Data.SqlDbType.Int, Value = productdemand.ProductID };
            var par7 = new SqlParameter() { ParameterName = "@ChangeTime", SqlDbType = System.Data.SqlDbType.DateTime, Value = productdemand.ChangeTime };
            var par8 = new SqlParameter() { ParameterName = "@VersionNumber", SqlDbType = System.Data.SqlDbType.VarChar, Value = productdemand.VersionNumber };
            return _dbHelper.SavaData(sql, par, par2, par3, par4, par5, par6, par7, par8) > 0 ? true : false;
        }
        #endregion

        #region 查询产品需求全部信息
        /// <summary>
        /// 查询产品需求全部信息
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> SelectProductDemand()
        {
            var sql = $@"select 
DemandID,
DemandName,
DemandModify,
DemandPurpose,
Priority,
b.Name,
ChangeTime,
VersionNumber
 from ProductDemandTable a
 JOIN Developers b
 ON a.ProductID=b.ID";
            return _dbHelper.GetList(sql);
        }
        #endregion

        #region  修改需求
        /// <summary>
        /// 修改需求
        /// </summary>
        /// <param name="requestform"></param>
        /// <returns></returns>
        public bool UpdateProductDemand(ProductDemandTable productdemand)
        {
            var sql = @"UPDATE ProductDemandTable SET 
DemandID=@DemandID,
DemandName=@DemandName,
DemandModify=@DemandModify,
DemandPurpose=@DemandPurpose,
Priority=@Priority,
ProductID=@ProductID,
ChangeTime=@ChangeTime,
VersionNumber=@VersionNumber
WHERE ID=@id";
            var par1 = new SqlParameter() { ParameterName = "@DemandID", SqlDbType = System.Data.SqlDbType.VarChar, Value = productdemand.DemandID };
            var par2 = new SqlParameter() { ParameterName = "@DemandName", SqlDbType = System.Data.SqlDbType.Text, Value = productdemand.DemandName };
            var par3 = new SqlParameter() { ParameterName = "@DemandModify", SqlDbType = System.Data.SqlDbType.VarChar, Value = productdemand.DemandModify };
            var par4 = new SqlParameter() { ParameterName = "@DemandPurpose", SqlDbType = System.Data.SqlDbType.VarChar, Value = productdemand.DemandPurpose };
            var par5 = new SqlParameter() { ParameterName = "@Priority", SqlDbType = System.Data.SqlDbType.VarChar, Value = productdemand.Priority };
            var par6 = new SqlParameter() { ParameterName = "@ProductID", SqlDbType = System.Data.SqlDbType.Text, Value = productdemand.ProductID };
            var par7 = new SqlParameter() { ParameterName = "@ChangeTime", SqlDbType = System.Data.SqlDbType.Int, Value = productdemand.ChangeTime };
            var par8 = new SqlParameter() { ParameterName = "@VersionNumber", SqlDbType = System.Data.SqlDbType.VarChar, Value = productdemand.VersionNumber };
            var par = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = productdemand.ID };

            return _dbHelper.SavaData(sql, par1, par2, par3, par4, par5, par6, par7, par8, par) > 0 ? true : false;
        }
        #endregion

        #region 删除需求
        /// <summary>
        /// 删除需求
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteProductDemand(int ID)
        {
            var sql = @"delete ProductDemandTable where ID=@id";
            var par = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = ID };
            return _dbHelper.SavaData(sql, par) > 0 ? true : false;
        }
        #endregion


        #region 查询部门名称
        /// <summary>
        /// 查询产品需求全部信息
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> SelectDepartments()
        {
            var sql = $@"select 
ID,DepartmentName
 from Departments ";
            return _dbHelper.GetList(sql);
        }
        #endregion

        #region 查询产品名称
        /// <summary>
        /// 查询产品全部信息
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> Products()
        {
            var sql = $@"select 
ID,ProductName
 from Products ";
            return _dbHelper.GetList(sql);
        }
        #endregion

        #region 通过姓名查询用户ID
        /// <summary>
        /// 通过姓名查询用户ID
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> InsertUsers(string name)
        {
            var sql = @"SELECT  b.ID ,
        UserName ,
        PassWord ,
        Character
FROM    dbo.Users a
JOIN Developers b
on a.UserName = b.Name
 where a.UserName=@username";
            var par1 = new SqlParameter() { ParameterName = "@username", SqlDbType = System.Data.SqlDbType.VarChar, Value = name };
            return _dbHelper.GetList(sql, par1);
        }
        #endregion

        #region 查询所有产品
        /// <summary>
        /// 查询所有产品
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string,dynamic>> QueryProducts()
        {
            var sql = @"select Id,ProductName from Products";
            return _dbHelper.GetList(sql);
        }
        #endregion

        #region 根据产品ID查询所有审核中以及审核通过的需求
        /// <summary>
        /// 根据根据产品ID查询所有审核中以及审核通过的需求
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        public List<Dictionary<string,dynamic>> QueryRequestByProductId(int id)
        {
            var sql = @"select 
a.Id,
a.DemandNname,
a.Priority,
c.Name,
a.status,
a.MakeTime from RequestForm a 
 join Products b on a.ProductID=b.ID 
 JOIN Developers c on a.ImplementerID=c.ID
 where ProductID=@id and Status!='已完成'";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetList(sql, par);
        }
        #endregion

        #region 查询产品共有多少条审核中及审核通过的需求
        /// <summary>
        /// 查询产品共有多少条审核中及审核通过的需求
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        public object QueryRequestCount(int id)
        {
            var sql = @"select count(*) from RequestForm where ProductID=@ID and Status!='已完成'";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetValue(sql, par);
        }
        #endregion

        #region 查询详细需求
        /// <summary>
        /// 查询详细需求
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> QueryDetailedRequirements(int id)
        {
            var sql = @"select a.DemandNname AS 标题,
a.RequirementsDescription AS 详细需求,
a.Priority AS 优先级,
a.UserName AS 医院名称,
a.Address AS 医院地址,
a.Producer AS 提出者姓名,
a.ContactInformation AS 医院联系方式,
b.Name AS 发布人姓名,
b.QQ AS QQ,
b.Email AS Email,
b.Phone AS 电话,
c.DepartmentName AS 交付部门,
a.MakeTime AS 发布时间,
a.VersionNumber AS 版本号
 from
RequestForm a inner join Developers b on a.ImplementerID=b.ID inner join Departments c on a.DeliveryDepartment=c.id where a.ID=@ID";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetList(sql, par);
        }
        #endregion
    }
}
