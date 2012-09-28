using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apathy.Models;

namespace Apathy.Controllers
{
    public class EnvelopeController : Controller
    {
        //
        // GET: /Envelope/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

    }
}
