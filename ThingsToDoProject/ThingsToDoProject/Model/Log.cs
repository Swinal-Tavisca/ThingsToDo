using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThingsToDoProject.Model
{
    public class Log
    {
        public string Request { get; set; }
        public string Response { get; set; }
        public string Exception { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
