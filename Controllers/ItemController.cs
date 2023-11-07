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
        // Base url of API
        private readonly string BaseUrl = "http://ec2-3-91-153-6.compute-1.amazonaws.com/";

        public async Task<ActionResult> Items()
        {
            // Items method is used to get Items from api, first it requests for the data and gets list of items as a response 
            List<Item> itemsList = new List<Item>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl); // Set the base URL for the API.
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Send an HTTP GET request to the 'api/Item' endpoint of the API.
                HttpResponseMessage response = await client.GetAsync("api/Item");

                // Check if the response from the API is successful.
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string and deserialize it to a list of 'Item' objects.
                    var itemResponse = response.Content.ReadAsStringAsync().Result;
                    itemsList = JsonConvert.DeserializeObject<List<Item>>(itemResponse);
                }

                // Return a view with the list of items retrieved from the API.
                return View(itemsList);
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            Item item = new Item();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);  // set BaseUrl for the API
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Send request to the api 
                HttpResponseMessage response = await client.GetAsync($"api/Item/{id}");

                if (response.IsSuccessStatusCode)
                {
                    // if response is success  Read the response content as a string and deserialize it to a single 'Item' object.
                    var itemResponse = response.Content.ReadAsStringAsync().Result;
                    item = JsonConvert.DeserializeObject<Item>(itemResponse);
                }

                // Return an item view retrieved from the API.
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
                client.BaseAddress = new Uri(BaseUrl); // set base url to send a post request to the API
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

                // sends a request to get a single item 

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
            // sends a request to change the specified item
            {
                client.BaseAddress = new Uri(BaseUrl); // set base url
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

            // sends a request to the api to get a single item to delete
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
                // sends a request to delete an item by the give id
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
