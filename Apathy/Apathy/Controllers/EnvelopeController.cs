using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apathy.Models;
using Apathy.DAL;
using Apathy.ViewModels;

namespace Apathy.Controllers
{ 
    [Authorize]
    public class EnvelopeController : Controller
    {
        //
        // GET: /Envelope/

        public ViewResult Index()
        {
            EnvelopeIndexViewModel viewModel = new EnvelopeIndexViewModel
            {
                Envelopes = Services.EnvelopeService.GetEnvelopes(User.Identity.Name),
                RecentTransactions = Services.TransactionService.GetRecentTransactions(User.Identity.Name)
            };

            return View(viewModel);
        }

        //
        // GET: /Envelope/Details/5

        public ViewResult Details(int id)
        {
            return View(Services.EnvelopeService.GetEnvelope(id, User.Identity.Name));
        }

        //
        // GET: /Envelope/Create

        public ActionResult Create()
        {
            IEnumerable<Envelope> envelopes = Services.EnvelopeService.GetEnvelopes(User.Identity.Name);
            ViewBag.Envelopes = envelopes;
            return View();
        }

        //
        // POST: /Envelope/Create

        [HttpPost]
        public ActionResult Create(Envelope envelope)
        {
            if (ModelState.IsValid)
            {
                Services.EnvelopeService.InsertEnvelope(envelope, User.Identity.Name);
                return RedirectToAction("Index");
            }

            return View(envelope);
        }
        
        //
        // GET: /Envelope/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View(Services.EnvelopeService.GetEnvelope(id, User.Identity.Name));
        }

        //
        // POST: /Envelope/Edit/5

        [HttpPost]
        public ActionResult Edit(Envelope envelope)
        {
            if (ModelState.IsValid)
            {
                Services.EnvelopeService.UpdateEnvelope(envelope);
                return RedirectToAction("Index");
            }
            return View(envelope);
        }

        //
        // GET: /Envelope/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(Services.EnvelopeService.GetEnvelope(id, User.Identity.Name));
        }

        //
        // POST: /Envelope/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Services.EnvelopeService.DeleteEnvelope(id, User.Identity.Name);
            return RedirectToAction("Index");
        }
        //
        // GET: /Envelope/Reset/5
 
        public ActionResult Reset(int id)
        {
            return View(Services.EnvelopeService.GetEnvelope(id, User.Identity.Name));
        }

        //
        // POST: /Envelope/Reset/5

        [HttpPost, ActionName("Reset")]
        public ActionResult ResetConfirmed(int id)
        {
            Services.EnvelopeService.ResetEnvelope(id, User.Identity.Name);
            return RedirectToAction("Index");
        }

    }
}