using JOBSEEKER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JOBSEEKER.Jobmodel
{
    public class tables
    {
        public IEnumerable<company> Companies { get; set; }
        public IEnumerable<job> Jobs { get; set; }
        public IEnumerable<category> Categories { get; set; }
        public IEnumerable<application> applications { get; set; }
        public IEnumerable<jobprofile> jobp { get; set; }
        public application app { get; set; }
        public job jo { get; set; }

    }
}