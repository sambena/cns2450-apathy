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
        private UnitOfWork uow = new UnitOfWork();

        //
        // GET: /Envelope/

        public ViewResult Index()
        {
            EnvelopeIndexViewModel viewModel = new EnvelopeIndexViewModel
            {
                Envelopes = uow.EnvelopeRepository.GetUserEnvelopes(User.Identity.Name),
                RecentTransactions = uow.TransactionRepository.GetRecentTransactions(User.Identity.Name, 14, 5)
            };

            return View(viewModel);
        }

        //
        // GET: /Envelope/Details/5

        public ViewResult Details(int id)
        {
            return View(uow.EnvelopeRepository.GetById(id));
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
                envelope.BudgetID = uow.BudgetRepository.GetByOwner(User.Identity.Name).BudgetID;
                uow.EnvelopeRepository.Add(envelope);
                uow.Save();
                return RedirectToAction("Index");
            }

            return View(envelope);
        }
        
        //
        // GET: /Envelope/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View(uow.EnvelopeRepository.GetById(id));
        }

        //
        // POST: /Envelope/Edit/5

        [HttpPost]
        public ActionResult Edit(Envelope envelope)
        {
            if (ModelState.IsValid)
            {
                uow.EnvelopeRepository.Update(envelope);
                uow.Save();
                return RedirectToAction("Index");
            }
            return View(envelope);
        }

        //
        // GET: /Envelope/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(uow.EnvelopeRepository.GetById(id));
        }

        //
        // POST: /Envelope/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            uow.EnvelopeRepository.Remove(id);
            uow.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            uow.Dispose();
            base.Dispose(disposing);
        }
    }
}