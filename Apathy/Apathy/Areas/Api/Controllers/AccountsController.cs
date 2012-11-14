using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Apathy.Models;

namespace Apathy.Areas.Api.Controllers
{
    public class AccountsController : Controller
    {
        [HttpPost]
        public JsonResult Login(LogOnModel model)
        {
            bool success = true;

            if (!Membership.ValidateUser(model.UserName, model.Password))
                success = false;

            return Json(new { success = success });
        }
    }
}
