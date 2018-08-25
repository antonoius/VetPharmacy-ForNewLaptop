using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using VetPharmacy.Models;

namespace VetPharmacy.Models
{
    public class verify:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
          if(  filterContext.HttpContext.Session.Contents["UserEmail"]==null|| filterContext.HttpContext.Session.Contents["UserEmail"].ToString() == string.Empty)
            {
                filterContext.Result = new RedirectResult("http://localhost:64304/Login/Login");

            }

        }
    }
}