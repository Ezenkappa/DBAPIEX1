using DBSchool.DataSource;
using DBSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DBSchool.Controllers
{
    public class TidController : ApiController
    {
        [HttpGet]
        public object GetTea(int tid)
        {
            DSTid listtl = new DSTid();
            var Viewt = listtl.GetTea(tid);
            
            DSTSlist lists = new DSTSlist();
            var Viewsl = lists.GetTSlist(tid);
            ModelAll2 List = new ModelAll2();
            List.listt = new List<ModelTid>();
            List.listt = Viewt;
            List.lists = new List<TStudentList>();
            List.lists = Viewsl;
            return List;
        }
    }
}
