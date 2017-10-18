﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Working.Models.DataModel
{
    /// <summary>
    /// 部门实体类
    /// </summary>
    public class Department
    {
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID
        { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string DepartMentName
        { get; set; }      
       
        /// <summary>
        /// 部门员工集合
        /// </summary>

        public List<User> Users
        { get; set; }

        /// <summary>
        /// 父部门ID
        /// </summary>
        public int PID
        { get; set; }
        
    }
}
