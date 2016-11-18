using FinanceAssist.mvc.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FinanceAssist.mvc.Controllers
{
    public class ExpensesController : Controller
    {
        // GET: Expenses
        public async Task<ActionResult> Index()
        {
            var client = Helpers.ExpenseTrackerHttpClient.GetClient();
            var response = await client.GetAsync("expenses/");
            

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<IEnumerable<Expense>>(content);
                return View(model);
            }

            return Content("An error occurred");
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Expenses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Expenses/Create
        [HttpPost]
        public async Task<ActionResult> Create(Expense expense)
        {
            try
            {
                var client = Helpers.ExpenseTrackerHttpClient.GetClient();
                expense.UserID = @"https://expensetrackeridsrv3/embedded_1";

                // serialize & POST
                var serializedItemToCreate = JsonConvert.SerializeObject(expense);

                var response = await client.PostAsync("expenses",
                    new StringContent(serializedItemToCreate, System.Text.Encoding.Unicode, "application/json"));

                
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Details", "ExpenseGroups", new { id = expense.Id });
                }
                else
                {
                    return Content("An error occurred");
                }

            }
            catch
            {
                return Content("An error occurred");
            }
        }

        // GET: Expenses/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Expenses/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Expenses/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Expenses/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
