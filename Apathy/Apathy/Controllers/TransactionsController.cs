using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Apathy.Models;
using Apathy.DAL;
using PagedList;

namespace Apathy.Controllers
{ 
    [Authorize]
    public class TransactionsController : Controller
    {
        //
        // GET: /Transactions/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";

            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;

            var transactions = from t in Services.TransactionService.GetTransactions(User.Identity.Name)
                               select t;
            if (!String.IsNullOrEmpty(searchString))
            {
                transactions = transactions.Where(t => (t.Notes != null ? t.Payee.ToUpper().Contains(searchString.ToUpper()) : false)
                    || t.Notes != null ? t.Notes.ToUpper().Contains(searchString.ToUpper()): false);
            }
            switch (sortOrder)
            {
                case "Envelope":
                    transactions = transactions.OrderByDescending(t => t.Envelope);
                    break;
                case "Date":
                    transactions = transactions.OrderBy(t => t.TransactionDate);
                    break;
                case "Type":
                    transactions = transactions.OrderByDescending(t => t.Type);
                    break;
                default:
                    transactions = transactions.OrderBy(t => t.TransactionDate);
                    break;
            }

            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(transactions.ToPagedList(pageNumber, pageSize));
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
            PopulateDropDownLists();
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

            PopulateDropDownLists(transaction);
            return View(transaction);
        }
        
        //
        // GET: /Transactions/Edit/5
 
        public ActionResult Edit(int id)
        {
            Transaction transaction = Services.TransactionService.GetTransaction(id, User.Identity.Name);
            PopulateDropDownLists(transaction);
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

            PopulateDropDownLists(transaction);
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

        private void PopulateDropDownLists(Transaction transaction = null)
        {
            int envelopeID = (transaction != null) ? transaction.EnvelopeID : 0;
            TransactionType transactionType = (transaction != null) ? transaction.Type : TransactionType.Expense;

            PopulateEnvelopeDropDownList(envelopeID);
            PopulateTransactionTypeDropDownList(transactionType);
        }

        private void PopulateEnvelopeDropDownList(int selectedEnvelopeID)
        {
            var envelopes = Services.EnvelopeService.GetEnvelopes(User.Identity.Name);
            ViewBag.EnvelopeID = new SelectList(envelopes, "EnvelopeID", "Title", selectedEnvelopeID);
        }

        private void PopulateTransactionTypeDropDownList(TransactionType selectedType)
        {
            var transactionTypes = from TransactionType t in Enum.GetValues(typeof(TransactionType))
                                   select new { ID = t, Name = t.ToString() };
            ViewBag.Type = new SelectList(transactionTypes, "ID", "Name", selectedType);
        }
    }
}