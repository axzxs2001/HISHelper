using System;
using Working.Model.Repository;
using Xunit;
using Moq;
using Working.Models.DataModel;
using MoqEFCoreExtension;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Working.XTest
{
    [Trait("Working项目", "DepartmentRepositoryTest测试")]
    public class DepartmentRepositoryTest
    {
        /// <summary>
        /// 部门仓储类
        /// </summary>
        IDepartmentRepository _departmentRepository;
        /// <summary>
        /// db mock
        /// </summary>
        Mock<WorkingDbContext> _dbMock;
        public DepartmentRepositoryTest()
        {
            var dbContextOptions = new DbContextOptions<WorkingDbContext>();
            _dbMock = new Mock<WorkingDbContext>(dbContextOptions);
            var list = new List<Department>() {
                new Department { ID = 1, DepartmentName = "公司",PDepartmentID=0 } ,
                new Department { ID = 2, DepartmentName = "开发一部",PDepartmentID=1 } ,
                new Department { ID = 3, DepartmentName = "开发二部",PDepartmentID=1 } };
            var dbSet = new Mock<DbSet<Department>>();
            dbSet.SetupList(list);
            _dbMock.Setup(db => db.Departments).Returns(dbSet.Object);

            _departmentRepository = new DepartmentRepository(_dbMock.Object);
        }      
    }
}
