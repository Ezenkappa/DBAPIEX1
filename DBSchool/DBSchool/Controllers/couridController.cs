using DBSchool.DataSource;
using DBSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebSockets;

namespace DBSchool.Controllers
{
    public class couridController : ApiController
    {
        [HttpGet]
        public object Getcour(int cid)
        {
            DSCourid listcl = new DSCourid();
            var Viewc = listcl.GetCour(cid);
            //var Viewcount = Viewsid.Count(); 可以省略Run DB次數跟簡略GetSum fonction
            //var Viewsum = View.Select(x => x.credit).Sum();  可以省略Run DB次數跟簡略GetSum fonction
            //還有Where 
            DSSlist lists = new DSSlist();
            var Viewsl = lists.GetSlist(cid);
            Viewc.lists = new List<StudentList>();
            Viewc.lists = Viewsl;
            return Viewc;
        }
    }
}
