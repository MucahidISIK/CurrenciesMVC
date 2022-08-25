using CurrenciesMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CurrenciesMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using (CurrencyService.CurrencyServiceClient serviceClient_ = new CurrencyService.CurrencyServiceClient())
            {

                var apiData = serviceClient_.getCurrencyInfoAsync().Result;
                List<CurrencyDetails> model = new List<CurrencyDetails>();
                for(int i=0; i<apiData.Length; i++)
                {
                    CurrencyDetails tempData = new CurrencyDetails();
                    tempData.Currency_ = apiData[i].Currency_;
                    tempData.Buying_ = apiData[i].Buying_;
                    tempData.Selling_ = apiData[i].Selling_;
                    model.Add(tempData);
                }
                return View(model);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}