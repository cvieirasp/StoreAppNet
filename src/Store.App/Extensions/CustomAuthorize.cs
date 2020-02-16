﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using System.Security.Claims;

namespace Store.App.Extensions
{
    public class CustomAuthorization
    {
        public static bool IsValidUserClaims(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated && context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }
    }

    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue)
            :base(typeof(ClaimFilterRequirement))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }

    public class ClaimFilterRequirement : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public ClaimFilterRequirement(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { 
                    area = "Identity", 
                    page = "/Account/Login",
                    ReturnUrl = context.HttpContext.Request.Path.ToString()
                }));
                return;
            }

            if (!CustomAuthorization.IsValidUserClaims(context.HttpContext, _claim.Type, _claim.Value))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}
