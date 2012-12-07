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
        public HttpStatusCodeResult Login(LogOnModel model)
        {
            if (!Membership.ValidateUser(model.UserName, model.Password))
                return new HttpStatusCodeResult(200);

            return new HttpStatusCodeResult(401);
        }
    }
}
