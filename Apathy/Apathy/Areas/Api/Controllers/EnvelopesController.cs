using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apathy.Models;
using Apathy.DAL;

namespace Apathy.Areas.Api.Controllers
{
    public class EnvelopesController : Controller
    {
        public JsonResult Index(string username)
        {
            var envelopes = Services.EnvelopeService.GetEnvelopes(username);

            if (envelopes == null)
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            var jsonEnvelopes = from e in envelopes
                                select new { e.EnvelopeID, e.Title, e.CurrentBalance, e.StartingBalance };

            return Json(new { success = true, data = jsonEnvelopes }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get(string username, int id)
        {
            Envelope envelope = Services.EnvelopeService.GetEnvelope(id, username);

            if (envelope == null)
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true, data = new { envelope.EnvelopeID, envelope.Title, envelope.CurrentBalance, envelope.StartingBalance } }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(string username, Envelope envelope)
        {
            Services.EnvelopeService.InsertEnvelope(envelope, username);

            return Json(new { success = true, data = new { envelope.EnvelopeID, envelope.Title, envelope.CurrentBalance, envelope.StartingBalance } });
        }

        [HttpPost]
        public JsonResult Edit(Envelope envelope)
        {
            Services.EnvelopeService.UpdateEnvelope(envelope);

            return Json(new { success = true, data = new { envelope.EnvelopeID, envelope.Title, envelope.CurrentBalance, envelope.StartingBalance } });
        }

        [HttpPost]
        public JsonResult Delete(string username, int id)
        {
            Services.EnvelopeService.DeleteEnvelope(id, username);

            return Json(new { success = true });
        }
    }
}
