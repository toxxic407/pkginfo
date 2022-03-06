using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pkginfo
{
    public class jsonentry
    {
        public string titleid { get; set; }
        public string version { get; set; }
        public string contentid { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public string[] links { get; set; }
        public string[] description { get; set; }
        public string[] devs { get; set; }
        public string size { get; set; }
        public string[] categories { get; set; }
    }
}
