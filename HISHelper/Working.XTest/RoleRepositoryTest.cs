using System;
using Working.Model.Repository;
using Xunit;
using Moq;
using Working.Models.DataModel;
using MoqEFCoreExtension;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Working.XUnitTest
{
    [Trait("Working项目", "RoleRepository测试")]
    public class RoleRepositoryTest
    {
        /// <summary>
        /// 角色仓储
        /// </summary>
        IRoleRepository _roleRepository;
        /// <summary>
        /// DB Mock
        /// </summary>
        Mock<WorkingDbContext> _dbMock;
        public RoleRepositoryTest()
        {
            _dbMock = new Mock<WorkingDbContext>();
            var list = new List<Role>() { new Role { ID = 1, RoleName = "admin" } };
            var dbSet = new Mock<DbSet<Role>>();
            dbSet.SetupList(list);
            _dbMock.Setup(db => db.Roles).Returns(dbSet.Object);

            _roleRepository = new RoleRepository(_dbMock.Object);
        }
        /// <summary>
        /// 测试查询角色返回空异常
        /// </summary>
        /// <param name="roleID">角色ID</param>
        [Theory]
        [InlineData(110)]
        [InlineData(0)]
        public void GetRole_Null_ThrowException(int roleID)
        {
            var exc = Assert.Throws<Exception>(() => { _roleRepository.GetRole(roleID); });
            Assert.Contains($"按照roleid={roleID}查询不到角色", exc.Message);
        }

        /// <summary>
        /// 测试查询角色返回空异常
        /// </summary>
        [Fact]
        public void GetRole_Default_ReturnRole()
        {
            var role = _roleRepository.GetRole(1);
            Assert.NotNull(role);
        }

        /// <summary>
        /// 测试查询全部角色抛出异常
        /// </summary>
        [Fact]
        public void GetAllRole_Null_ThrowException()
        {
            var message = "查询全部role异常";
            _dbMock.Setup(db => db.Roles).Throws(new Exception(message));
            var exc = Assert.Throws<Exception>(() => { _roleRepository.GetAllRole(); });
            Assert.Contains(message, exc.Message);
        }

        /// <summary>
        /// 测试查询全部角色正常返回
        /// </summary>
        [Fact]
        public void GetAllRole_Default_ReturnList()
        {
            var roles = _roleRepository.GetAllRole();
            Assert.Single(roles);
        }
    }
}
