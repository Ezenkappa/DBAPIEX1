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
    public class SidController : ApiController
    {
        [HttpGet]
        public object sid(int sid)
        {
            DSSid listsid = new DSSid();
            var Viewsid = listsid.GetSid(sid);
            var Viewcount = listsid.GetSum(sid , 1);
            var Viewsum = listsid.GetSum(sid, 2);
            ModelAll1 listall = new ModelAll1();
            listall.listSid = new List<ModelSid>();
            listall.listSid = Viewsid;
            listall.listCount = Viewcount;
            listall.listSum = Viewsum;
            return listall;
        }
    }
}
