using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWorker
{
    public class Task
    {
        public string height { get; set; }
        public string width { get; set; }
        public string degrees { get; set; }
        public string blackwhite { get; set; }
        public string path { get; set; }
        public string name { get; set; }
    }
}
