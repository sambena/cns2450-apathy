﻿using System.Web.Mvc;

namespace Apathy.Areas.Api
{
    public class ApiAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Api";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Login",
                "Api/login",
                new { controller = "Accounts", action = "Login" }
            );

            context.MapRoute(
                "Api_default",
                "Api/{username}/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                null,
                new string[] { "Apathy.Areas.Api.Controllers" }
            );
        }
    }
}
