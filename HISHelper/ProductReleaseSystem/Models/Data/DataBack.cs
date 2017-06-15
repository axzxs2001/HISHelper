using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noe1.Models
{
    public class DataBack
    {
        public int Result { get; set; }

        public string Message { get; set; }

        public List<Dictionary<string, dynamic>> Data { get; set; }
    }
}
