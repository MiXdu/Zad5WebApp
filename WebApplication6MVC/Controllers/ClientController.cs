using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication6Api.Database.Entities;

namespace WebApplication6MVC.Controllers
{
    public class ClientController : Controller
    {

        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _apiUrl;

        public ClientController(IConfiguration configuration)
        {
            _apiClient = new();
            _configuration = configuration;
            _apiUrl = $"{_configuration["ApiConfig:ApiUrl"]}/Parcel";

            _apiClient.DefaultRequestHeaders.Add("ApiKey", _configuration["ApiConfig:ApiKey"]);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Parcel> parcels = null;

            HttpResponseMessage apiResponse = await _apiClient.GetAsync(_apiUrl);
            if (apiResponse.IsSuccessStatusCode)
            {
                parcels = await apiResponse.Content.ReadAsAsync<List<Parcel>>();

            }
            return View(parcels);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Parcel parcel = null;

            HttpResponseMessage apiResponse = await _apiClient.GetAsync($"{_apiUrl}/{id}");
            if (apiResponse.IsSuccessStatusCode)
            {
                parcel = await apiResponse.Content.ReadAsAsync<Parcel>();
                return View(parcel);
            }

            return NotFound();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Parcel parcel)
        {

            if (ModelState.IsValid)
            {
                HttpResponseMessage apiResponse = await _apiClient.PostAsJsonAsync(_apiUrl, parcel);
                if (apiResponse.IsSuccessStatusCode)
                {
                    parcel = await apiResponse.Content.ReadAsAsync<Parcel>();
                    return RedirectToAction("Details", new { id = parcel.Id });
                }
            }

            return View(parcel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Parcel parcel = null;

            HttpResponseMessage apiResponse = await _apiClient.GetAsync($"{_apiUrl}/{id}");
            if (apiResponse.IsSuccessStatusCode)
            {
                parcel = await apiResponse.Content.ReadAsAsync<Parcel>();
                return View(parcel);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Parcel parcel)
        {

            if (ModelState.IsValid)
            {
                HttpResponseMessage apiResponse = await _apiClient.PutAsJsonAsync($"{_apiUrl}/{parcel.Id}", parcel);
                if (apiResponse.IsSuccessStatusCode)
                {
                    HttpResponseMessage response = await _apiClient.PutAsJsonAsync($"{_apiUrl}/{parcel.Id}", parcel);
                    response.EnsureSuccessStatusCode();
                    return RedirectToAction("Details", new { id = parcel.Id });
                }
            }

            return View(parcel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Parcel parcel = null;

            HttpResponseMessage apiResponse = await _apiClient.GetAsync($"{_apiUrl}/{id}");
            if (apiResponse.IsSuccessStatusCode)
            {
                parcel = await apiResponse.Content.ReadAsAsync<Parcel>();
                return View(parcel);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int notUsed = 0)
        {
            HttpResponseMessage apiResponse = await _apiClient.DeleteAsync($"{_apiUrl}/{id}");
            apiResponse.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

    }
}