using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Apathy.Models;
using Apathy.DAL;

namespace Apathy.Controllers
{ 
    [Authorize]
    public class TransactionsController : Controller
    {
        private UnitOfWork uow = new UnitOfWork();

        //
        // GET: /Transactions/

        public ViewResult Index()
        {
            return View(uow.TransactionRepository.GetUserTransactions(User.Identity.Name));
        }

        //
        // GET: /Transactions/Details/5

        public ViewResult Details(int id)
        {
            return View(uow.TransactionRepository.GetById(id));
        }

        //
        // GET: /Transactions/Create

        public ActionResult Create()
        {
            ViewBag.EnvelopeID = new SelectList(uow.EnvelopeRepository.GetUserEnvelopes(User.Identity.Name), "EnvelopeID", "Title");
            return View();
        }

        //
        // POST: /Transactions/Create

        [HttpPost]
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                uow.TransactionRepository.Add(transaction);
                uow.Save();
                return RedirectToAction("Index");  
            }

            ViewBag.EnvelopeID = new SelectList(uow.EnvelopeRepository.GetUserEnvelopes(User.Identity.Name), "EnvelopeID", "Title", transaction.EnvelopeID);
            return View(transaction);
        }
        
        //
        // GET: /Transactions/Edit/5
 
        public ActionResult Edit(int id)
        {
            Transaction transaction = uow.TransactionRepository.GetById(id);
            ViewBag.EnvelopeID = new SelectList(uow.EnvelopeRepository.GetUserEnvelopes(User.Identity.Name), "EnvelopeID", "Title", transaction.EnvelopeID);
            return View(transaction);
        }

        //
        // POST: /Transactions/Edit/5

        [HttpPost]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                uow.TransactionRepository.Update(transaction);
                uow.Save();
                return RedirectToAction("Index");
            }

            ViewBag.EnvelopeID = new SelectList(uow.EnvelopeRepository.GetUserEnvelopes(User.Identity.Name), "EnvelopeID", "Title", transaction.EnvelopeID);
            return View(transaction);
        }

        //
        // GET: /Transactions/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(uow.TransactionRepository.GetById(id));
        }

        //
        // POST: /Transactions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            uow.TransactionRepository.Remove(id);
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