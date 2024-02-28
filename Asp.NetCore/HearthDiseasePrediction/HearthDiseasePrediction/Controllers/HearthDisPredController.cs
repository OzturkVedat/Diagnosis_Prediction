using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using HearthDiseasePrediction.Models;

namespace HearthDiseasePrediction.Controllers
{
    public class HearthDisPredController : Controller
    {
        private readonly ILogger<HearthDisPredController> _logger;

        public HearthDisPredController(ILogger<HearthDisPredController> logger)
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
                    var apiUrl = "http://127.0.0.1:5000/predict/logistic";
                    var json = JsonConvert.SerializeObject(data);

                    var response = await client.PostAsync(apiUrl, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var prediction = JsonConvert.DeserializeObject<PredictionResult>(result);

                        // Ensure prediction value is valid
                        if (prediction.Prediction == 0 || prediction.Prediction == 1)
                        {
                            model.Result = prediction.Prediction;
                            _logger.LogInformation("Prediction successful. Result: {Result}", model.Result);
                            return View("../Results/HearthDiseaseResult", model);
                        }
                        else
                        {
                            _logger.LogError("Unexpected prediction value: {Prediction}", prediction.Prediction);
                            return View("Error");
                        }
                    }
                    else
                    {
                        _logger.LogError("API call failed with status code: {StatusCode}", response.StatusCode);
                        return View("Error");
                    }
                }
            }

            _logger.LogInformation("Model state is not valid");
            return View("../Forms/HearthDisForm", model);
        }

    }
}
