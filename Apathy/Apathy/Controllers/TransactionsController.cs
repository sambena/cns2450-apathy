using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apathy.Models;

namespace Apathy.Controllers
{ 
    public class TransactionsController : Controller
    {
        private BudgetContext db = new BudgetContext();

        //
        // GET: /Transactions/

        public ViewResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Envelope);
            return View(transactions.ToList());
        }

        //
        // GET: /Transactions/Details/5

        public ViewResult Details(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            return View(transaction);
        }

        //
        // GET: /Transactions/Create

        public ActionResult Create()
        {
            ViewBag.EnvelopeID = new SelectList(db.Envelopes, "EnvelopeID", "Title");
            return View();
        } 

        //
        // POST: /Transactions/Create

        [HttpPost]
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.EnvelopeID = new SelectList(db.Envelopes, "EnvelopeID", "Title", transaction.EnvelopeID);
            return View(transaction);
        }
        
        //
        // GET: /Transactions/Edit/5
 
        public ActionResult Edit(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            ViewBag.EnvelopeID = new SelectList(db.Envelopes, "EnvelopeID", "Title", transaction.EnvelopeID);
            return View(transaction);
        }

        //
        // POST: /Transactions/Edit/5

        [HttpPost]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnvelopeID = new SelectList(db.Envelopes, "EnvelopeID", "Title", transaction.EnvelopeID);
            return View(transaction);
        }

        //
        // GET: /Transactions/Delete/5
 
        public ActionResult Delete(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            return View(transaction);
        }

        //
        // POST: /Transactions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}