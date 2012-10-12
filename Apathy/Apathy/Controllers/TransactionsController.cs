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
        private TransactionService transactionService;
        private EnvelopeService envelopeService;

        public TransactionsController()
        {
            UnitOfWork uow = new UnitOfWork();
            this.transactionService = new TransactionService(uow);
            this.envelopeService = new EnvelopeService(uow);
        }

        //
        // GET: /Transactions/

        public ViewResult Index()
        {
            return View(transactionService.GetUserTransactions(User.Identity.Name));
        }

        //
        // GET: /Transactions/Details/5

        public ViewResult Details(int id)
        {
            return View(transactionService.GetTransactionByID(id));
        }

        //
        // GET: /Transactions/Create

        public ActionResult Create()
        {
            ViewBag.EnvelopeID = new SelectList(envelopeService.GetUserEnvelopes(User.Identity.Name), "EnvelopeID", "Title");
            return View();
        }

        //
        // POST: /Transactions/Create

        [HttpPost]
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transactionService.InsertTransaction(transaction);
                return RedirectToAction("Index");  
            }

            ViewBag.EnvelopeID = new SelectList(envelopeService.GetUserEnvelopes(User.Identity.Name), "EnvelopeID", "Title", transaction.EnvelopeID);
            return View(transaction);
        }
        
        //
        // GET: /Transactions/Edit/5
 
        public ActionResult Edit(int id)
        {
            Transaction transaction = transactionService.GetTransactionByID(id);
            ViewBag.EnvelopeID = new SelectList(envelopeService.GetUserEnvelopes(User.Identity.Name), "EnvelopeID", "Title", transaction.EnvelopeID);
            return View(transaction);
        }

        //
        // POST: /Transactions/Edit/5

        [HttpPost]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transactionService.UpdateTransaction(transaction);
                return RedirectToAction("Index");
            }

            ViewBag.EnvelopeID = new SelectList(envelopeService.GetUserEnvelopes(User.Identity.Name), "EnvelopeID", "Title", transaction.EnvelopeID);
            return View(transaction);
        }

        //
        // GET: /Transactions/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(transactionService.GetTransactionByID(id));
        }

        //
        // POST: /Transactions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            transactionService.DeleteTransaction(id);
            return RedirectToAction("Index");
        }
    }
}