using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBSchool.Models
{
    public class ModelCourid
    {
        public String t_name { get; set; }
        public String dept_name { get; set; }
        public int Credit { get; set; }
        public List<StudentList> lists { get; set; }


    }
}