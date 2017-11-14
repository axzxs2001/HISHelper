using System;
using Working.Model.Repository;
using Xunit;
using Moq;
using Working.Models.DataModel;
using MoqEFCoreExtension;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Working.XTest
{
    public class RoleRepositoryTest
    {
        IRoleRepository _roleRepository;
        Mock<WorkingDbContext> _mockDB;
        public RoleRepositoryTest()
        {
            _mockDB = new Mock<WorkingDbContext>();
            var list = new List<Role>() { new Role { ID = 1, RoleName = "admin" } };
            var dbSet = new Mock<DbSet<Role>>();
            dbSet.SetupList(list);
            _mockDB.Setup(db => db.Roles).Returns(dbSet.Object);

            _roleRepository = new RoleRepository(_mockDB.Object);
        }
        /// <summary>
        /// ���Բ�ѯ��ɫ���ؿ��쳣
        /// </summary>
        [Theory]
        [InlineData(110)]
        [InlineData(0)]
        public void GetRole_Null_ThrowException(int roleID)
        { 
            var exc = Assert.Throws<Exception>(() => { _roleRepository.GetRole(roleID); });
            Assert.Contains($"����roleid={roleID}��ѯ������ɫ", exc.Message);
        }

        /// <summary>
        /// ���Բ�ѯ��ɫ���ؿ��쳣
        /// </summary>
        [Fact]
        public void GetRole_Default_ReturnRole()
        {
            var role = _roleRepository.GetRole(1);
            Assert.NotNull(role);
        }
    }
}
