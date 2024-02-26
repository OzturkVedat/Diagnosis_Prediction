using HearthDiseasePrediction.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace HearthDiseasePrediction.Controllers
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(HearthDiseaseDiagnosis model)
        {
            if (ModelState.IsValid)
            {
                // Convert HearthDiseaseDiagnosisMetrics to a dictionary or anonymous object
                var data = new
                {
                    Age = model.Age,
                    Gender = model.Gender,
                    ChestPainType = model.ChestPainType,
                    MaxHearthRate = model.MaxHearthRate,
                    RestingBloodPressure = model.RestingBloodPressure,
                    SerumCholesterol = model.SerumCholesterol,
                    FastingBloodSugar = model.FastingBloodSugar,
                    RestingElectrocardio = model.RestingElectrocardio,
                    ExerciseAngina = model.ExerciseAngina,
                    OldPeak = model.OldPeak,
                    SlopeST = model.SlopeST,
                    MajorVessels = model.MajorVessels
                };

                using (var client = new HttpClient())
                {
                    var apiUrl = "http://127.0.0.1:5000/predict";

                    // Serialize data to JSON
                    var json = JsonConvert.SerializeObject(data);

                    // Make a POST request to the Python API
                    var response = await client.PostAsync(apiUrl, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var prediction = JsonConvert.DeserializeObject<PredictionResult>(result);

                        if (prediction.Prediction != null)
                        {
                            model.Result = prediction.Prediction;
                            return View("Views/Results/HearthDiseaseResult.cshtml", model);
                        }
                        else
                        {
                            // Handle unexpected prediction value
                            return View("Error");
                        }
                    }
                    else
                    {
                        // Handle API error
                        return View("Error");
                    }
                }
            }
            // If the model state is not valid, return the view with validation errors
            return View(model);
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