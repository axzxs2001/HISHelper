using Microsoft.Extensions.Options;
using ProductReleaseSystem.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem
{
    /// <summary>
    /// 下载类
    /// </summary>
    public class DownLoadFile: IDownLoadFile
    {
        /// <summary>
        /// 数据库操作对象
        /// </summary>
        DBHelper _dbHelper;

        /// <summary>
        /// 初始化赋值链接字符串
        /// </summary>
        /// <param name="connections">链接字符串</param>
        public DownLoadFile(IOptions<ConnectionSetting> connections)
        {
           
            _dbHelper = new DBHelper(connections.Value.prConnectionStrings);

        }

        
    }
}
