using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apathy.Models;
using Apathy.DAL;

namespace Apathy.Controllers
{ 
    public class UserController : Controller
    {
        private BudgetContext db = new BudgetContext();

        //
        // GET: /User/

        public ViewResult Index()
        {
            var users = Services.UserService.GetBudgetUsers(User.Identity.Name);

            return View(users);
        }

        //
        // GET: /User/Details/5

        public ViewResult Details(string id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(Services.UserService.GetBudgetUsers(User.Identity.Name), "UserID", "Title");
            return View();
        } 

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                Services.UserService.CreateUser(user, User.Identity.Name, );
                return RedirectToAction("Index");
            }

            ViewBag.EnvelopeID = new SelectList(Services.UserService.GetBudgetUsers(User.Identity.Name), "UserID", "Title");
            return View(user);
        }
        
        //
        // GET: /User/Edit/5
 
        public ActionResult Edit(string id)
        {
            User user = db.Users.Find(id);
            ViewBag.BudgetID = new SelectList(db.Budgets, "BudgetID", "BudgetID", user.BudgetID);
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BudgetID = new SelectList(db.Budgets, "BudgetID", "BudgetID", user.BudgetID);
            return View(user);
        }

        //
        // GET: /User/Delete/5
 
        public ActionResult Delete(string id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {            
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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