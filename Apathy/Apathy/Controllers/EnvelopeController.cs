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
        private EnvelopeService envelopeService;
        private TransactionService transactionService;

        public EnvelopeController()
        {
            UnitOfWork uow = new UnitOfWork();
            this.envelopeService = new EnvelopeService(uow);
            this.transactionService = new TransactionService(uow);
        }

        //
        // GET: /Envelope/

        public ViewResult Index()
        {
            var envelopes = envelopeService.GetUserEnvelopes(User.Identity.Name);

            EnvelopeIndexViewModel viewModel = new EnvelopeIndexViewModel
            {
                Envelopes = envelopes,
                RecentTransactions = envelopes.SelectMany(e => e.Transactions)
                    .Where(t => t.Date > DateTime.Today.AddDays(-14))
                    .OrderByDescending(t => t.Date)
                    .Take(5)
            };

            return View(viewModel);
        }

        //
        // GET: /Envelope/Details/5

        public ViewResult Details(int id)
        {
            return View(envelopeService.GetEnvelopeByID(id));
        }

        //
        // GET: /Envelope/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Envelope/Create

        [HttpPost]
        public ActionResult Create(Envelope envelope)
        {
            if (ModelState.IsValid)
            {
                envelopeService.InsertEnvelope(User.Identity.Name, envelope);
                return RedirectToAction("Index");
            }

            return View(envelope);
        }
        
        //
        // GET: /Envelope/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View(envelopeService.GetEnvelopeByID(id));
        }

        //
        // POST: /Envelope/Edit/5

        [HttpPost]
        public ActionResult Edit(Envelope envelope)
        {
            if (ModelState.IsValid)
            {
                envelopeService.UpdateEnvelope(envelope);
                return RedirectToAction("Index");
            }
            return View(envelope);
        }

        //
        // GET: /Envelope/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(envelopeService.GetEnvelopeByID(id));
        }

        //
        // POST: /Envelope/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            envelopeService.DeleteEnvelope(id);
            return RedirectToAction("Index");
        }
    }
}