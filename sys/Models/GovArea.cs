using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sys.Models
{
    public class GovArea
    {
        public bool success { get; set; }
        public Result result { get; set; }
    }

    public class Result
    {
        public string resource_id { get; set; }
        public int limit { get; set; }
        public int total { get; set; }

        public Fields[] fields { get; set; }
        public Records[] records { get; set; }

    }

    public class Fields
    {
        public string type { get; set; }
        public string id { get; set; }
    }

    public class Records
    {
        public string County { get; set; }
        public string Town { get; set; }
    }
}