using Frontend_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Frontend_MVC.Controllers
{
    public class ItemController : Controller
    {
        private readonly string BaseUrl = "http://ec2-3-91-153-6.compute-1.amazonaws.com/";

        public async Task<ActionResult> Items()
        {
            List<Item> itemsList = new List<Item>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Item");

                if (response.IsSuccessStatusCode)
                {
                    var itemResponse = response.Content.ReadAsStringAsync().Result;
                    itemsList = JsonConvert.DeserializeObject<List<Item>>(itemResponse);
                }

                return View(itemsList);
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            Item item = new Item();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"api/Item/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var itemResponse = response.Content.ReadAsStringAsync().Result;
                    item = JsonConvert.DeserializeObject<Item>(itemResponse);
                }

                return View(item);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Item item)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("api/Item", item);

                if (response.IsSuccessStatusCode)
                {
                    // if response is success then redirect to Items list page
                    return RedirectToAction("Items");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact the administrator.");
                    return View(item);
                }
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            Item item = new Item();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"api/Item/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var itemResponse = response.Content.ReadAsStringAsync().Result;
                    item = JsonConvert.DeserializeObject<Item>(itemResponse);
                }

                return View(item);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Item item)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PutAsJsonAsync($"api/Item/{id}", item);

                if (response.IsSuccessStatusCode)
                {
                    // Handle the successful update and redirection as needed
                    return RedirectToAction("Items");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact the administrator.");
                    return View(item);
                }
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            Item item = new Item();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"api/Item/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var itemResponse = response.Content.ReadAsStringAsync().Result;
                    item = JsonConvert.DeserializeObject<Item>(itemResponse);
                }

                return View(item);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Delete(int id, Item item)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync($"api/Item/{id}");

                if (response.IsSuccessStatusCode)
                {
                    // Handle the successful deletion and redirection as needed
                    return RedirectToAction("Items");
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
