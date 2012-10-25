using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apathy.DAL;

namespace Apathy.Controllers
{
    public class AndroidController : Controller
    {
        //
        // GET: /Android/GetEnvelopes/username
        public ActionResult GetEnvelopes(string id)
        {
            var envelopes = from e in Services.EnvelopeService.GetEnvelopes(id)
                            select new { e.EnvelopeID, e.Title, e.StartingBalance, e.CurrentBalance };

            return Json(envelopes, JsonRequestBehavior.AllowGet);
        }
    }
}
