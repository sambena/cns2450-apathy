using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apathy.DAL;
using Apathy.Models;

namespace Apathy.Areas.Api.Controllers
{
    public class TransactionsController : Controller
    {
        public JsonResult Index(string username)
        {
            IEnumerable<Transaction> transactions = Services.TransactionService.GetTransactions(username);

            if (transactions == null)
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            var jsonTransactions = from t in transactions
                                   select new { t.TransactionID, t.Type, t.EnvelopeID, t.TransactionDate, t.Amount, t.UserName, t.Payee, t.Notes };

            return Json(new { success = true, data = jsonTransactions }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get(string username, int id)
        {
            Transaction t = Services.TransactionService.GetTransaction(id, username);

            if (t == null)
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true, data = new { t.TransactionID, t.Type, t.EnvelopeID, t.TransactionDate, t.Amount, t.UserName, t.Payee, t.Notes } }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(string username, Transaction t)
        {
            Services.TransactionService.InsertTransaction(t, username);

            return Json(new { success = true, data = new { t.TransactionID, t.Type, t.EnvelopeID, t.Amount, t.TransactionDate, t.UserName, t.Payee, t.Notes } });
        }

        [HttpPost]
        public JsonResult Edit(string username, Transaction t)
        {
            Services.TransactionService.UpdateTransaction(t, username);

            return Json(new { success = true, data = new { t.TransactionID, t.Type, t.EnvelopeID, t.Amount, t.TransactionDate, t.UserName, t.Payee, t.Notes } });
        }

        [HttpPost]
        public JsonResult Delete(string username, int id)
        {
            Services.TransactionService.DeleteTransaction(id, username);

            return Json(new { success = true });
        }
    }
}
