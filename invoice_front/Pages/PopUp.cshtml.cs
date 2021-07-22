using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using invoice_front.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace invoice_front.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class PopUpModel : PageModel
    {
        public string RequestId { get; set; }
        public List<Purchase> ListOfPurchase { get; set; }
        private readonly ILogger<PopUpModel> _logger;
        private readonly IConfiguration _config;
        public PopUpModel(ILogger<PopUpModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public void OnGet()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_config.GetSection("WebAPIBaseAddress").Value);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/GetPurchase").Result;

                if (response.IsSuccessStatusCode)
                {
                    string a = response.Content.ReadAsStringAsync().Result;
                    ListOfPurchase = JsonConvert.DeserializeObject<List<Purchase>>(a);
                }
            }
            catch { }

            #region Data Dummy
            if (ListOfPurchase == null || ListOfPurchase.Count == 0)
            {
                ListOfPurchase = new List<Purchase>();
                ListOfPurchase.Add(new Purchase { Amount = 2, PurchaseNo = "PO-123" });
                ListOfPurchase.Add(new Purchase { Amount = 3, PurchaseNo = "PO-124" });
                ListOfPurchase.Add(new Purchase { Amount = 4, PurchaseNo = "PO-125" });
            }
            #endregion
        }
    }
}
