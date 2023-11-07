using Frontend_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics.Tracing;
using System.Net.Http.Headers;

namespace Frontend_MVC.Controllers
{
    public class TransactionController : Controller
    {

        private readonly string BaseUrl = "http://ec2-3-91-153-6.compute-1.amazonaws.com/";
        // GET: TransactionController
        public async Task<ActionResult> Index()
        {
            List<Transaction> transactionList = new List<Transaction>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Transaction");

                if (response.IsSuccessStatusCode)
                {
                    var itemResponse = response.Content.ReadAsStringAsync().Result;
                    transactionList = JsonConvert.DeserializeObject<List<Transaction>>(itemResponse);
                }

                return View(transactionList);
            }

        }

        // GET: TransactionController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Transaction trn = new Transaction();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"api/Transaction/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var itemResponse = response.Content.ReadAsStringAsync().Result;
                    trn = JsonConvert.DeserializeObject<Transaction>(itemResponse);
                }

                return View(trn);
            }
        }

        // GET: TransactionController/Create
        public ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        // POST: TransactionController/Create
        public async Task<ActionResult> Create(Transaction trn)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("api/Transaction", trn);

                if (response.IsSuccessStatusCode)
                {
                    // if response is success then redirect to Transactions list page
                    return RedirectToAction("Index");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Error: {errorContent}");
                    return View(trn);
                }
            }
        }

        // GET: TransactionController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Transaction trn = new Transaction();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"api/Transaction/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var trnResponse = response.Content.ReadAsStringAsync().Result;
                    trn = JsonConvert.DeserializeObject<Transaction>(trnResponse);
                }

                return View(trn);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Transaction trn)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PutAsJsonAsync($"api/Transaction/{id}", trn);

                if (response.IsSuccessStatusCode)
                {
                    // Handle the successful update and redirection as needed
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact the administrator.");
                    return View(trn);
                }
            }
        }

        // GET: TransactionController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Transaction trn = new Transaction();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"api/Transaction/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var trnResponse = response.Content.ReadAsStringAsync().Result;
                    trn = JsonConvert.DeserializeObject<Transaction>(trnResponse);
                }

                return View(trn);
            }
        }

        // POST: TransactionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Transaction trn)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync($"api/Transaction/{id}");

                if (response.IsSuccessStatusCode)
                {
                    // Handle the successful deletion and redirection as needed
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact the administrator.");
                    return View();
                }
            }
        }
    }
}
