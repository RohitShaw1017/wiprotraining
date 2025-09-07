using Microsoft.AspNetCore.Routing;
using System;

namespace MvcRoutingApp.Constraints
{
    public class GuidConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter route,
                          string parameterName, RouteValueDictionary values,
                          RouteDirection routeDirection)
        {
            if (!values.ContainsKey(parameterName))
                return false;

            var value = values[parameterName]?.ToString();
            return Guid.TryParse(value, out _);
        }
    }
}
