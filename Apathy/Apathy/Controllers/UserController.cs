using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
            var test = Services.UserService.GetUser(id);
            return View(test);
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
        public ActionResult Create(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                string errorDescription;
                bool success = Services.UserService.CreateUser(registerModel, User.Identity.Name, out errorDescription);

                if (success)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", errorDescription);
            }

            return View(registerModel);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(string id)
        {
            User user = Services.UserService.GetUser(id);
            if (user == null)
                throw new HttpException(404, "Resource not found");

            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                Services.UserService.UpdateUser(user);
                return RedirectToAction("Index");
            }
            return View(User);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(string id)
        {
            User user = Services.UserService.GetUser(id);
            if (id == null)
                throw new HttpException(404, "Resource not found");

            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Services.UserService.DeleteUser(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}