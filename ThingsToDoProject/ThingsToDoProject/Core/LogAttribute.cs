using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThingsToDoProject.Core.Provider.DatabaseProviders;
using ThingsToDoProject.Model;

namespace ThingsToDoProject.Core
{
    public class LogAttribute : ResultFilterAttribute, IActionFilter
    {
        Log logValue = new Log();
        LoggingThroughCassandra loggingObject = new LoggingThroughCassandra();
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                logValue.Request = context.RouteData.Values["action"].ToString() + " " + context.RouteData.Values["controller"].ToString();
                logValue.Response = "Success";
                logValue.Exception = "NULL";
                loggingObject.Add(logValue);
            }

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            logValue.Request = context.RouteData.Values["action"].ToString() + " " + context.RouteData.Values["controller"].ToString();
            logValue.Response = "NULL";
            logValue.Exception = "NULL";
            logValue.TimeStamp = DateTime.Now;
            loggingObject.Add(logValue);

        }
    }
}
