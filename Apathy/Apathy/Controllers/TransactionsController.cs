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
        //
        // GET: /Transactions/

        public ViewResult Index()
        {
            return View(Services.TransactionService.GetTransactions(User.Identity.Name));
        }

        //
        // GET: /Transactions/Details/5

        public ViewResult Details(int id)
        {
            return View(Services.TransactionService.GetTransaction(id, User.Identity.Name));
        }

        //
        // GET: /Transactions/Create

        public ActionResult Create()
        {
            var transactionTypes = from TransactionType t in Enum.GetValues(typeof(TransactionType))select new { ID = t, Name = t.ToString() };
            ViewBag.Type = new SelectList(transactionTypes, "ID", "Name", "");
            ViewBag.EnvelopeID = new SelectList(Services.EnvelopeService.GetEnvelopes(User.Identity.Name), "EnvelopeID", "Title");
            return View();
        }

        //
        // POST: /Transactions/Create

        [HttpPost]
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                Services.TransactionService.InsertTransaction(transaction, User.Identity.Name);
                return RedirectToAction("Index");  
            }

            ViewBag.EnvelopeID = new SelectList(Services.EnvelopeService.GetEnvelopes(User.Identity.Name), "EnvelopeID", "Title", transaction.EnvelopeID);
            return View(transaction);
        }
        
        //
        // GET: /Transactions/Edit/5
 
        public ActionResult Edit(int id)
        {
            var transactionTypes = from TransactionType t in Enum.GetValues(typeof(TransactionType)) select new { ID = t, Name = t.ToString() };
            ViewBag.Type = new SelectList(transactionTypes, "ID", "Name", "Expense");
            Transaction transaction = Services.TransactionService.GetTransaction(id, User.Identity.Name);
            ViewBag.EnvelopeID = new SelectList(Services.EnvelopeService.GetEnvelopes(User.Identity.Name), "EnvelopeID", "Title", transaction.EnvelopeID);
            return View(transaction);
        }

        //
        // POST: /Transactions/Edit/5

        [HttpPost]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                Services.TransactionService.UpdateTransaction(transaction, User.Identity.Name);
                return RedirectToAction("Index");
            }

            ViewBag.EnvelopeID = new SelectList(Services.EnvelopeService.GetEnvelopes(User.Identity.Name), "EnvelopeID", "Title", transaction.EnvelopeID);
            return View(transaction);
        }

        //
        // GET: /Transactions/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(Services.TransactionService.GetTransaction(id, User.Identity.Name));
        }

        //
        // POST: /Transactions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Services.TransactionService.DeleteTransaction(id, User.Identity.Name);
            return RedirectToAction("Index");
        }
    }
}