using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem
{
    public class AppSettings
    {
        /// <summary>
        /// 血常规
        /// </summary>
        public string bloodRoutineName
        { get; set; }

        /// <summary>
        /// 尿常规
        /// </summary>
        public string urineRoutineName
        { get; set; }
        /// <summary>
        /// 中心静脉导管
        /// </summary>
        public string centralVenousCatheter
        {
            get;set;
        }
    }
}
