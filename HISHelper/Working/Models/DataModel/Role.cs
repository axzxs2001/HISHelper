using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Working.Models.DataModel
{
    /// <summary>
    /// 角色实体类
    /// </summary>
    public class Role
    {
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID
        { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName
        { get; set; }

        /// <summary>
        /// 用户集合
        /// </summary>
        public List<User> Users
        { get; set; }

    }
}
