using HearthDiseasePrediction.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace HearthDiseasePrediction.Controllers
{
    public class DiabetesPredictionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Submit(DiabetesDiagnosis model)
        {
            if (ModelState.IsValid)
            {
                int enteredAge = model.Age;
                model.Age = enteredAge >= 80 ? 13 : (enteredAge - 18) / 5 + 1;
                var data = new
                {
                    BMI = model.BMI,
                    Age = model.Age,
                    GenHlth = (int)model.GenHlth,
                    Income = (int)model.Income,
                    HighChol = (int)model.HighChol,
                    Sex = (int)model.Sex,
                    HearthDiseaseorAttack = (int)model.HearthDiseaseorAttack,
                    HvyAlcoholConsump = (int)model.HvyAlcoholConsump,
                    CholCheck = (int)model.CholCheck,
                    PhsylHlth = model.PhsylHlth
                };


                using (var client = new HttpClient())
                {
                    var apiUrl = "http://127.0.0.1:5000/predict/lightgbm";

                    // Serialize data to JSON
                    var json = JsonConvert.SerializeObject(data);

                    // Make a POST request to the Python API
                    var response = await client.PostAsync(apiUrl, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var prediction = JsonConvert.DeserializeObject<PredictionResult>(result);

                        if (prediction.Prediction == 0 || prediction.Prediction == 1)
                        {
                            model.Result = prediction.Prediction;
                            model.Age = enteredAge;
                            return View("../Results/DiabetesResult", model);
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
            return View("../Forms/DiabetesForm", model);
        }

    }
}
