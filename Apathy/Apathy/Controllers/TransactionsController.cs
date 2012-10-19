﻿using System;
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
            return View(Services.TransactionService.GetTransaction(id));
        }

        //
        // GET: /Transactions/Create

        public ActionResult Create()
        {
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
            Transaction transaction = Services.TransactionService.GetTransaction(id);
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
            return View(Services.TransactionService.GetTransaction(id));
        }

        //
        // POST: /Transactions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Services.TransactionService.DeleteTransaction(id);
            return RedirectToAction("Index");
        }
    }
}