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

        private readonly string BaseUrl = "http://ec2-3-91-153-6.compute-1.amazonaws.com/"; // set Base API Url 
        // GET: TransactionController
        public async Task<ActionResult> Index()
        {
            // sends a get request to the API to get list of Transactions 
            List<Transaction> transactionList = new List<Transaction>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl); // set base url
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Transaction");  // request to the api

                if (response.IsSuccessStatusCode)
                {
                    var itemResponse = response.Content.ReadAsStringAsync().Result; // getting Result from response
                    transactionList = JsonConvert.DeserializeObject<List<Transaction>>(itemResponse);  // Deserializing the result
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
                client.BaseAddress = new Uri(BaseUrl); // set base url
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"api/Transaction/{id}"); // send get request with specified id

                if (response.IsSuccessStatusCode)
                {
                    var itemResponse = response.Content.ReadAsStringAsync().Result; // get the result from response 
                    trn = JsonConvert.DeserializeObject<Transaction>(itemResponse); // deserialize the result
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
                
                //send post request to create new item
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

                // send request to get specified transaction by id to edit
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


                // send request to edit existing item in the db
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
