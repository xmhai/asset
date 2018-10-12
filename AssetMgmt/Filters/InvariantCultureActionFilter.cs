using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetMgmt.Filters
{
    public class InvariantCultureActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //doesn't work
            //System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            //System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
        }
    }
}