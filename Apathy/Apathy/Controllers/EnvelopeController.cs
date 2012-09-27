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
    public class EnvelopeController : Controller
    {
        private BudgetContext db = new BudgetContext();

        //
        // GET: /Envelope/

        public ViewResult Index()
        {
            var envelopes = db.Envelopes.Include(e => e.Budget);
            return View(envelopes.ToList());
        }

        //
        // GET: /Envelope/Details/5

        public ViewResult Details(int id)
        {
            Envelope envelope = db.Envelopes.Find(id);
            return View(envelope);
        }

        //
        // GET: /Envelope/Create

        public ActionResult Create()
        {
            ViewBag.BudgetID = new SelectList(db.Budgets, "BudgetID", "Owner");
            return View();
        } 

        //
        // POST: /Envelope/Create

        [HttpPost]
        public ActionResult Create(Envelope envelope)
        {
            if (ModelState.IsValid)
            {
                db.Envelopes.Add(envelope);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.BudgetID = new SelectList(db.Budgets, "BudgetID", "Owner", envelope.BudgetID);
            return View(envelope);
        }
        
        //
        // GET: /Envelope/Edit/5
 
        public ActionResult Edit(int id)
        {
            Envelope envelope = db.Envelopes.Find(id);
            ViewBag.BudgetID = new SelectList(db.Budgets, "BudgetID", "Owner", envelope.BudgetID);
            return View(envelope);
        }

        //
        // POST: /Envelope/Edit/5

        [HttpPost]
        public ActionResult Edit(Envelope envelope)
        {
            if (ModelState.IsValid)
            {
                db.Entry(envelope).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BudgetID = new SelectList(db.Budgets, "BudgetID", "Owner", envelope.BudgetID);
            return View(envelope);
        }

        //
        // GET: /Envelope/Delete/5
 
        public ActionResult Delete(int id)
        {
            Envelope envelope = db.Envelopes.Find(id);
            return View(envelope);
        }

        //
        // POST: /Envelope/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Envelope envelope = db.Envelopes.Find(id);
            db.Envelopes.Remove(envelope);
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