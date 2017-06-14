using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace ProductReleaseSystem
{
    /// <summary>
    /// SQL操作对象
    /// </summary>
    public class SqlOperation
    {
        /// <summary>
        /// SQL语句
        /// </summary>
        public string Sql
        { get; set; }
        /// <summary>
        /// 参数列表
        /// </summary>
        public DbParameter[] parmeters
        {
            get; set;
        } = new DbParameter[0];

    }
}
