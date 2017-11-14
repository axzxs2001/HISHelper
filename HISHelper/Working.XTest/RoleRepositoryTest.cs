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
    [Trait("Working��Ŀ", "RoleRepository����")]
    public class RoleRepositoryTest
    {
        /// <summary>
        /// ��ɫ�ִ�
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
        /// ���Բ�ѯ��ɫ���ؿ��쳣
        /// </summary>
        /// <param name="roleID">��ɫID</param>
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

        /// <summary>
        /// ���Բ�ѯȫ����ɫ�׳��쳣
        /// </summary>
        [Fact]
        public void GetAllRole_Null_ThrowException()
        {
            var message = "��ѯȫ��role�쳣";
            _dbMock.Setup(db => db.Roles).Throws(new Exception(message));
            var exc = Assert.Throws<Exception>(() => { _roleRepository.GetAllRole(); });
            Assert.Contains(message, exc.Message);
        }

        /// <summary>
        /// ���Բ�ѯȫ����ɫ��������
        /// </summary>
        [Fact]
        public void GetAllRole_Default_ReturnList()
        {
            var roles = _roleRepository.GetAllRole();
            Assert.Single(roles);
        }
    }
}
