using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apathy.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            return View();
        }

        [Authorize]
        public ActionResult Envelope()
        {
            return View();
        }

        [Authorize]
        public ActionResult Transactions()
        {
            return View();
        }
    }
}
