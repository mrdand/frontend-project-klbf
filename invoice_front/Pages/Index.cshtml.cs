using invoice_front.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace invoice_front.Pages
{
    public class IndexModel : PageModel
    {
        public AllData Data { get; set; }
        public List<InvDue> ListOfInvDue { get; set; }
        public string Number { get; set; }

        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;
        public IndexModel(ILogger<IndexModel> logger, IConfiguration config)
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

                HttpResponseMessage response = client.GetAsync("api/GetAllData").Result;

                if (response.IsSuccessStatusCode)
                {
                    string a = response.Content.ReadAsStringAsync().Result;
                    Data = JsonConvert.DeserializeObject<AllData>(a);
                }
            }
            catch { }

            #region InvNo
            int nomor = Data != null ? Data.Number : 0;
            if (nomor == 0)
            {
                this.Number = "001";
            }
            else
            {
                this.Number = nomor.ToString();
                if (this.Number.Length == 1)
                {
                    this.Number = "00" + this.Number;
                }
                if (this.Number.Length == 2)
                {
                    this.Number = "0" + this.Number;
                }
            }
            #endregion

            #region Data Dummy
            ListOfInvDue = new List<InvDue>();
            ListOfInvDue.Add(new InvDue { Desc = "Immediately", ID = 1 });
            ListOfInvDue.Add(new InvDue { Desc = "Later", ID = 2 });

            if (Data == null)
            {
                Data = new AllData();
            }

            if (Data.ListOfCustomer == null || Data.ListOfCustomer.Count == 0)
            {
                Data.ListOfCustomer = new List<Customer>();
                Data.ListOfCustomer.Add(new Customer { Address = "Jakarta", Name = "Joe", ID = 1, LogoURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/67/Kalbe_Farma.svg/220px-Kalbe_Farma.svg.png" });
                Data.ListOfCustomer.Add(new Customer { Address = "Bandung", Name = "Asep", ID = 2, LogoURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2f/Google_2015_logo.svg/251px-Google_2015_logo.svg.png" });
            }

            if (Data.ListOfLanguage == null || Data.ListOfLanguage.Count == 0)
            {
                Data.ListOfLanguage = new List<Language>();
                Data.ListOfLanguage.Add(new Language { Code = "English", Name = "English(US)", ID = 1 });
                Data.ListOfLanguage.Add(new Language { Code = "Indonesia", Name = "Indonesia(IDN)", ID = 1 });
            }

            if (Data.ListOfCurrency == null || Data.ListOfCurrency.Count == 0)
            {
                Data.ListOfCurrency = new List<Currency>();
                Data.ListOfCurrency.Add(new Currency { Code = "USD", Name = "United State(USD)", ID = 1 });
                Data.ListOfCurrency.Add(new Currency { Code = "IDR", Name = "Indonesia(IDR)", ID = 1 });
            }

            if (Data.ListOfUOM == null || Data.ListOfUOM.Count == 0)
            {
                Data.ListOfUOM = new List<UOM>();
                Data.ListOfUOM.Add(new UOM { Code = "hour", ID = 1 });
                Data.ListOfUOM.Add(new UOM { Code = "min", ID = 2 });
            }
            #endregion
        }
    }
}
