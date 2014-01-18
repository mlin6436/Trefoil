using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trefoil.Web.Helpers;
using Trefoil.Web.Models;

namespace Trefoil.Web.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class AuthorisedUserAttribute : AuthorizeAttribute
    {
        public AuthorisedUserAttribute()
        {
            Roles = String.Join(",", Enum.GetValues(typeof(Role)).Cast<Role>());
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            LoginModel login = (LoginModel)Trefoil.Web.Helpers.Extensions.GetUserCredential();

            if (login != null && Roles.Split(',').Any(r => r == Enum.Parse(typeof(Role), login.roletypeid.ToString()).ToString()))
            {
                return true;
            }

            return false;
        }
    }
}