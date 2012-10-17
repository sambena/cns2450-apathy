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
        private ServiceContainer services;

        public TransactionsController()
        {
            this.services = new ServiceContainer();
        }

        //
        // GET: /Transactions/

        public ViewResult Index()
        {
            return View(services.TransactionService.GetUserTransactions(User.Identity.Name));
        }

        //
        // GET: /Transactions/Details/5

        public ViewResult Details(int id)
        {
            return View(services.TransactionService.GetTransactionByID(id));
        }

        //
        // GET: /Transactions/Create

        public ActionResult Create()
        {
            ViewBag.EnvelopeID = new SelectList(services.EnvelopeService.GetUserEnvelopes(User.Identity.Name), "EnvelopeID", "Title");
            return View();
        }

        //
        // POST: /Transactions/Create

        [HttpPost]
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                services.TransactionService.InsertTransaction(transaction);
                return RedirectToAction("Index");  
            }

            ViewBag.EnvelopeID = new SelectList(services.EnvelopeService.GetUserEnvelopes(User.Identity.Name), "EnvelopeID", "Title", transaction.EnvelopeID);
            return View(transaction);
        }
        
        //
        // GET: /Transactions/Edit/5
 
        public ActionResult Edit(int id)
        {
            Transaction transaction = services.TransactionService.GetTransactionByID(id);
            ViewBag.EnvelopeID = new SelectList(services.EnvelopeService.GetUserEnvelopes(User.Identity.Name), "EnvelopeID", "Title", transaction.EnvelopeID);
            return View(transaction);
        }

        //
        // POST: /Transactions/Edit/5

        [HttpPost]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                services.TransactionService.UpdateTransaction(transaction);
                return RedirectToAction("Index");
            }

            ViewBag.EnvelopeID = new SelectList(services.EnvelopeService.GetUserEnvelopes(User.Identity.Name), "EnvelopeID", "Title", transaction.EnvelopeID);
            return View(transaction);
        }

        //
        // GET: /Transactions/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(services.TransactionService.GetTransactionByID(id));
        }

        //
        // POST: /Transactions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            services.TransactionService.DeleteTransaction(id);
            return RedirectToAction("Index");
        }
    }
}