using ProductReleaseSystem.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using ProductReleaseSystem.Data;
using ProductReleaseSystem.ProductRelease;

namespace ProductReleaseSystem.Models.Repository
{
    public class Research : IResearch
    {
        /// <summary>
        /// 数据库操作对象
        /// </summary> 
        DBHelper _dbHelper;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="connections"></param>
        public Research(IOptions<ConnectionSetting> connections)
        {
            _dbHelper = new DBHelper(connections.Value.prConnectionStrings);
        }

        #region 添加再研项目
        /// <summary>
        /// 添加再研项目
        /// </summary>
        /// <param name="researchprojects"></param>
        /// <returns></returns>
        public bool InsertResearch(ResearchProjects researchprojects)
        {
            var sql = "insert into ResearchProjects (ProjectName,ProjectIntroduction,StartingTime,EndTime,ProjectProgress,Projectcontent) values(@ProjectName,@ProjectIntroduction,@StartingTime,@EndTime,@ProjectProgress,@Projectcontent)";
            var par = new SqlParameter() { ParameterName = "@ProjectName", SqlDbType = System.Data.SqlDbType.VarChar, Value = researchprojects.ProjectName };
            var par2 = new SqlParameter() { ParameterName = "@ProjectIntroduction", SqlDbType = System.Data.SqlDbType.VarChar, Value = researchprojects.ProjectIntroduction };
            var par3 = new SqlParameter() { ParameterName = "@StartingTime", SqlDbType = System.Data.SqlDbType.VarChar, Value = researchprojects.StartingTime };
            var par4 = new SqlParameter() { ParameterName = "@EndTime", SqlDbType = System.Data.SqlDbType.VarChar, Value = researchprojects.EndTime };
            var par5 = new SqlParameter() { ParameterName = "@ProjectProgress", SqlDbType = System.Data.SqlDbType.Text, Value = researchprojects.ProjectProgress };
            var par6 = new SqlParameter() { ParameterName = "@Projectcontent", SqlDbType = System.Data.SqlDbType.Text, Value = researchprojects.Projectcontent };
            return _dbHelper.SavaData(sql, par, par2, par3, par4, par5,par6) > 0 ? true : false;
        }
        #endregion

        #region  删除在研项目
        /// <summary>
        /// 删除在研项目
        /// </summary>
        /// <param name="id">在研项目ID</param>
        /// <returns></returns>
        public bool DeleteResearch(int id)
        {
            var sql = @"delete ResearchProjects where ID=@id";
            var par = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.SavaData(sql, par) > 0 ? true : false;
        }
        #endregion

        #region 修改在研项目信息
        /// <summary>
        /// 修改在研项目信息
        /// </summary>
        /// <param name="id">在研项目ID</param>
        /// <returns></returns>
        public bool UpdateResearch(ResearchProjects upresearch)
        {
            var sql = @"UPDATE ResearchProjects SET 
ProjectName=@ProjectName,
ProjectIntroduction=@ProjectIntroduction,
StartingTime=@StartingTime,
EndTime=@EndTime,
ProjectProgress=@ProjectProgress,
Projectcontent=@Projectcontent
WHERE ID=@id";
            var par1 = new SqlParameter() { ParameterName = "@ProjectName", SqlDbType = System.Data.SqlDbType.VarChar, Value = upresearch.ProjectName };
            var par2 = new SqlParameter() { ParameterName = "@ProjectIntroduction", SqlDbType = System.Data.SqlDbType.VarChar, Value = upresearch.ProjectIntroduction };
            var par3 = new SqlParameter() { ParameterName = "@StartingTime", SqlDbType = System.Data.SqlDbType.VarChar, Value = upresearch.StartingTime };
            var par4 = new SqlParameter() { ParameterName = "@EndTime", SqlDbType = System.Data.SqlDbType.VarChar, Value = upresearch.EndTime };
            var par5 = new SqlParameter() { ParameterName = "@ProjectProgress", SqlDbType = System.Data.SqlDbType.VarChar, Value = upresearch.ProjectProgress };
            var par6 = new SqlParameter() { ParameterName = "@Projectcontent", SqlDbType = System.Data.SqlDbType.Text, Value = upresearch.Projectcontent };
            var par = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = upresearch.Id };

            return _dbHelper.SavaData(sql, par1, par2, par3, par4, par5,par6, par) > 0 ? true : false;
        }
        #endregion

        #region 添加在研人员
        /// <summary>
        /// 添加在研人员
        /// </summary>
        /// <param name="researchers"></param>
        /// <returns></returns>
        public bool InsertResearchers(Researchers researchers)
        {
            var sql = @"insert into Researchers(ResearchProjectsID,PersonID,Personneltype) values(@ResearchProjectsID,@PersonID,@Personneltype)";
            var par1 = new SqlParameter()
            {
                ParameterName = "@ResearchProjectsID",
                SqlDbType = System.Data.SqlDbType.Int

,
                Value = researchers.ResearchProjectsID
            };
            var par2 = new SqlParameter()
            {
                ParameterName = "@PersonID",
                SqlDbType = System.Data.SqlDbType.Int

,
                Value = researchers.PersonID
            };
            var par3 = new SqlParameter() { ParameterName = "@Personneltype", SqlDbType = System.Data.SqlDbType.VarChar, Value = researchers.Personneltype };
            return _dbHelper.SavaData(sql, par1, par2, par3) > 0 ? true : false;
        }
        #endregion

        #region 查询在研项目信息
        /// <summary>
        /// 查询在研项目信息
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> SelectResearch()
        {
            var sql = "select ID, ProjectName,ProjectIntroduction,StartingTime,EndTime,ProjectProgress,Projectcontent from ResearchProjects";
            return _dbHelper.GetList(sql);
        }
        #endregion

        #region 通过ID查询在研项目信息
        /// <summary>
        /// 查询在研项目信息
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> SelectResearch(int ID)
        {
            var sql = "select ID,ProjectName,ProjectIntroduction,StartingTime,EndTime,ProjectProgress,Projectcontent from ResearchProjects where ID = @ID";
            var par = new SqlParameter() { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.VarChar, Value=ID };
            return _dbHelper.GetList(sql,par);
        }
        #endregion

        #region 查询所有部门ID和名称
        /// <summary>
        /// 查询所有部门ID和名称
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> SelectDepartments()
        {
            var sql = "SELECT ID,DepartmentName FROM dbo.Departments";
            return _dbHelper.GetList(sql);
        }
        #endregion

        #region 通过部门查询开发人员
        /// <summary>
        /// 通过部门查询开发人员
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> SelectDevelopers(int departmentID)
        {
            var sql = (@"SELECT ID ,
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
                SqlDbType = System.Data.SqlDbType.Int,
                Value = departmentID
            };

            return _dbHelper.GetList(sql, par1);
        }
        #endregion

        #region 通过部门查询开发人员
        /// <summary>
        /// 通过部门查询开发人员
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> SelectRenYuan(int id)
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
            var par1 = new SqlParameter()
            {
                ParameterName = "@id",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = id
            };

            return _dbHelper.GetList(sql, par1);
        }
        #endregion

        #region  根据人员ID删除相关人员
        /// <summary>
        /// 根据人员ID删除相关人员
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns></returns>
        public bool DeleteResearchers(int ResearchProjectsID, int personID)
        {
            var sql = @"delete Researchers where ResearchProjectsID=@id and PersonID=@personID";
            var par = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = ResearchProjectsID };
            var par1 = new SqlParameter() { ParameterName = "@personID", SqlDbType = System.Data.SqlDbType.Int, Value = personID };
            return _dbHelper.SavaData(sql, par,par1) > 0 ? true : false;
        }
        #endregion

        #region  根据项目ID删除相关人员
        /// <summary>
        /// 根据人员ID删除相关人员
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns></returns>
        public bool DeleteAllResearchers(int id)
        {
            var sql = @"delete Researchers where ResearchProjectsID=@id";
            var par = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.SavaData(sql, par) > 0 ? true : false;
        }
        #endregion
        /// <summary>
        /// 修改人员为负责人
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns></returns>
        public bool UpdatePersonType(int? id, int ResearchProjectsID)
        {
            var sql = @"update Researchers set Personneltype='管理员' where ResearchProjectsID=@ResearchProjectsID and  PersonID=@id";
            var par = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            var par1 = new SqlParameter() { ParameterName = "@ResearchProjectsID", SqlDbType = System.Data.SqlDbType.Int, Value = ResearchProjectsID };
            return _dbHelper.SavaData(sql, par, par1) > 0 ? true : false;


        }
        /// <summary>
        /// 根据项目ID查询开发人员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> SelectInresearchers(int id)
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
join Researchers c 
on a.ID=c.PersonID 
where c.ResearchProjectsID=@id";
            var par1 = new SqlParameter() { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int, Value = id };
            return _dbHelper.GetList(sql, par1);
        }


        /// <summary>
        /// 根据项目ID查询开发人员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> ReverseInquiry()
        {
            var sql = @"select top 1 ID from ResearchProjects order by id desc" ;
            return _dbHelper.GetList(sql);
        }
    }
}




