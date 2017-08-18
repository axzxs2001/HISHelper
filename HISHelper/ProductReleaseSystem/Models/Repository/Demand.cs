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

        #region 实施上传需求信息页面
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
            var sql = "insert into RequestForm (DemandNname,RequirementsDescription,Priority,UserName,Producer,ContactInformation,ImplementerID,MakeTime,VersionNumber,DeliveryDepartment,Status,ProductID,Address,DeleteStatus) values(@DemandNname,@RequirementsDescription,@Priority,@UserName,@Producer,@ContactInformation,@ImplementerID,@MakeTime,@VersionNumber,@DeliveryDepartment,@Status,@ProductID,@Address,@DeleteStatus)";
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
            var par14 = new SqlParameter() { ParameterName = "@DeleteStatus", SqlDbType = System.Data.SqlDbType.Int, Value = requestform.DeleteStatus };
            return _dbHelper.SavaData(sql, par, par2, par3, par4, par5, par6, par7, par8, par9,par10,par11,par12, par13,par14) > 0 ? true : false;
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
Address,
DeleteStatus
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
Address=@Address,
DeleteStatus=@DeleteStatus
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
            var par14 = new SqlParameter() { ParameterName = "@DeleteStatus", SqlDbType = System.Data.SqlDbType.Int, Value = requestform.DeleteStatus };
            var par = new SqlParameter() { ParameterName = "@id",SqlDbType = System.Data.SqlDbType.Int,Value = requestform.ID };

            return _dbHelper.SavaData(sql, par1, par2, par3, par4, par5, par6, par7, par8, par9,par10,par11,par12, par13, par14,par) > 0 ? true : false;
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
        #endregion

        #region 产品发布需求页面

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
        #endregion

        #region 查询全部页面
        #region 查询所有产品
        /// <summary>
        /// 查询所有产品
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string,dynamic>> QueryProducts()
        {
            var sql = @"select distinct  a.ID AS Id,a.ProductName AS ProductName from Products a 
JOIN RequestForm b 
ON a.ID=b.ProductID and DeleteStatus=1";
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
 where ProductID=@id and Status!='已完成'and DeleteStatus=1";
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
            var sql = @"select count(*) AS COUNT from RequestForm where ProductID=@ID and Status!='已完成'and DeleteStatus=1";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetValue(sql, par);
        }
        #endregion
        #endregion

        #region 根据需求ID查询人员意见
        /// <summary>
        /// 根据需求ID查询人员意见
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns></returns>
        public List<Dictionary<string,dynamic>> QueryOpinion(int id)
        {
            var sql = @"select a.purpose AS 目的,
a.detailed AS 详细,
a.Initiatedtime AS 发起时间,
b.Name AS 发起人姓名,
b.Phone AS 电话,
b.QQ AS QQ
from Opinion a inner join Developers b on a.proposerID = b.ID inner join RequestForm c on a.DemandID = c.ID where a.DemandID = @ID";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetList(sql, par);
        }
        #endregion

        #region 添加人员意见
        /// <summary>
        /// 添加人员意见
        /// </summary>
        /// <param name="opinion">人员意见实体类</param>
        /// <returns></returns>
        public bool AddOpinion(Opinion opinion)
        {
            var sql = "insert into Opinion(DemandID,proposerID,purpose,detailed,Initiatedtime) values(@DemandID,@proposerID,@purpose,@detailed,@Initiatedtime)";
            var par1 = new SqlParameter() { ParameterName = "@DemandID", SqlDbType = System.Data.SqlDbType.Int, Value = opinion.DemandID };
            var par2 = new SqlParameter() { ParameterName = "@proposerID", SqlDbType = System.Data.SqlDbType.Int, Value = opinion.proposerID };
            var par3 = new SqlParameter() { ParameterName = "@purpose", SqlDbType = System.Data.SqlDbType.VarChar, Value = opinion.purpose };
            var par4 = new SqlParameter() { ParameterName = "@detailed", SqlDbType = System.Data.SqlDbType.Text, Value = opinion.detailed };
            var par5 = new SqlParameter() { ParameterName = "@Initiatedtime", SqlDbType = System.Data.SqlDbType.DateTime, Value = DateTime.Now };
            return _dbHelper.SavaData(sql,par1,par2,par3,par4,par5) > 0 ? true : false;
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
            var sql = @"select
a.DemandNname AS 标题,
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

        #region  我发布的页面

        #region 查询详细需求（我发布的）
        /// <summary>
        /// 查询详细需求(我发布的)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> SelectDemand(int id)
        {
            var sql = @"select distinct  a.ID,a.ProductName from Products a 
JOIN RequestForm b 
ON a.ID=b.ProductID
where b.ImplementerID=@ID and DeleteStatus=1";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetList(sql, par);
        }
        #endregion

        #region 根据产品ID查询所有审核中以及审核通过的需求(我发布的)
        /// <summary>
        /// 根据根据产品ID查询所有审核中以及审核通过的需求(我发布的)
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> QueryfbProductId(int id,int nameid)
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
 where ProductID=@id  and a.ImplementerID=@nameid and Status!='已完成'and DeleteStatus=1";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            var par1 = new SqlParameter() { ParameterName = "@nameid", SqlDbType = System.Data.SqlDbType.Int, Value = nameid };
            return _dbHelper.GetList(sql, par,par1);
        }
        #endregion

        #region 查询产品共有多少条审核中及审核通过的需求(我发布的)
        /// <summary>
        /// 查询产品共有多少条审核中及审核通过的需求(我发布的)
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        public object QueryfbCount(int id,int nameid)
        {
            var sql = @"select count(*) AS COUNT from RequestForm where ProductID=@ID and ImplementerID=@nameid and Status!='已完成'and DeleteStatus=1";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            var par1 = new SqlParameter() { ParameterName = "@nameid", SqlDbType = System.Data.SqlDbType.Int, Value = nameid };
            return _dbHelper.GetValue(sql, par, par1);
        }
        #endregion
        #endregion

        #region 已完成页面
        #region 查询已完成的所有产品
        /// <summary>
        /// 查询所有产品
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> CarryOutProducts()
        {
            var sql = @"select distinct  a.ID AS Id,a.ProductName AS ProductName from Products a 
JOIN RequestForm b 
ON a.ID=b.ProductID where b.Status='已完成'and DeleteStatus=1";
            return _dbHelper.GetList(sql);
        }
        #endregion
        #region 根据产品ID查询所有审核中以及审核通过的需求
        /// <summary>
        /// 根据根据产品ID查询所有审核中以及审核通过的需求
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> QueryCarryOutProductId(int id)
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
 where ProductID=@id and Status='已完成'and DeleteStatus=1";
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
        public object QueryCarryOutCount(int id)
        {
            var sql = @"select count(*) AS COUNT from RequestForm where ProductID=@ID and Status='已完成'and DeleteStatus=1";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetValue(sql, par);
        }
        #endregion
        #endregion

        #region  删除修改需求编号
        /// <summary>
        /// 删除修改需求编号
        /// </summary>
        /// <param name="requestform"></param>
        /// <returns></returns>
        public bool DeleteStatus(int deletestatus, int ID)
        {
            var sql = @"UPDATE RequestForm SET 
DeleteStatus=@DeleteStatus
WHERE ID=@id";
            
            var par1 = new SqlParameter() { ParameterName = "@DeleteStatus", SqlDbType = System.Data.SqlDbType.Int, Value = deletestatus };
            var par = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = ID };

            return _dbHelper.SavaData(sql,par1, par) > 0 ? true : false;
        }
        #endregion


        #region 垃圾箱页面

        #region 垃圾箱查询所有产品
        /// <summary>
        /// 查询所有产品
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> DeleteQueryProducts()
        {
            var sql = @"select distinct  a.ID AS Id,a.ProductName AS ProductName from Products a 
JOIN RequestForm b 
ON a.ID=b.ProductID and DeleteStatus=3";
            return _dbHelper.GetList(sql);
        }
        #endregion

        #region 垃圾箱根据产品ID查询所有审核中以及审核通过的需求
        /// <summary>
        /// 根据根据产品ID查询所有审核中以及审核通过的需求
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> DeleteQueryRequestByProductId(int id)
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
 where ProductID=@id and Status!='已完成'and DeleteStatus=3";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetList(sql, par);
        }
        #endregion

        #region 垃圾箱查询产品共有多少条审核中及审核通过的需求
        /// <summary>
        /// 查询产品共有多少条审核中及审核通过的需求
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns></returns>
        public object DeleteQueryRequestCount(int id)
        {
            var sql = @"select count(*) AS COUNT from RequestForm where ProductID=@ID and Status!='已完成'and DeleteStatus=3";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetValue(sql, par);
        }
        #endregion

        #region  还原垃圾箱需求
        /// <summary>
        /// 删除修改需求编号
        /// </summary>
        /// <param name="requestform"></param>
        /// <returns></returns>
        public bool Reduction(int deletestatus, int ID)
        {
            var sql = @"UPDATE RequestForm SET 
DeleteStatus=@DeleteStatus
WHERE ID=@id";

            var par1 = new SqlParameter() { ParameterName = "@DeleteStatus", SqlDbType = System.Data.SqlDbType.Int, Value = deletestatus };
            var par = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = ID };

            return _dbHelper.SavaData(sql, par1, par) > 0 ? true : false;
        }
        #endregion
        #endregion

        public bool Review(string Status, int ID)
        {
            var sql = @"UPDATE RequestForm SET Status=@Status where ID=@id";
            var par1 = new SqlParameter() { ParameterName = "@Status", SqlDbType = System.Data.SqlDbType.VarChar, Value = Status };
            var par = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = ID };

            return _dbHelper.SavaData(sql, par1, par) > 0 ? true : false;
        }

    }
}
