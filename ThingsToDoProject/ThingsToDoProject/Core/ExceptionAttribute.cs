using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThingsToDoProject.Core.Provider.DatabaseProviders;
using ThingsToDoProject.Model;

namespace ThingsToDoProject.Core
{
    public class ExceptionAttribute : ExceptionFilterAttribute
    {
        Log logObject = new Log();
        LoggingThroughCassandra loggingObject = new LoggingThroughCassandra();

        public override void OnException(ExceptionContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is Exception)
            {
                logObject.Request = actionExecutedContext.RouteData.Values["action"].ToString() + " " + actionExecutedContext.RouteData.Values["action"].ToString();
                logObject.Exception = actionExecutedContext.Exception.ToString();
                var index = logObject.Exception.IndexOf("\r");
                logObject.Exception = logObject.Exception.Substring(0, index);
                logObject.Response = "Failure";
                logObject.TimeStamp = DateTime.Now;
                loggingObject.Add(logObject);
            }
        }
    }
}
