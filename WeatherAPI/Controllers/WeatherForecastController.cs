using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WeatherAPI.Models;
using WeatherAPI.OpenWeatherMap_Model;
using WeatherAPI.Repositories;

namespace WeatherAPI.Controllers
{
    public class WeatherForecastController : Controller
    {
        private readonly IWeatherForecastRepository _WeatherForecastRepository;

        public WeatherForecastController(IWeatherForecastRepository WeatherForecastRepository)
        {
             _WeatherForecastRepository = WeatherForecastRepository;
        }

        [HttpGet]
        public IActionResult SearchByCity()
        {
            var viewModel = new SearchByCity();
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult SearchByCity(SearchByCity model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "WeatherForecast", new { city = model.CityName });
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult City(string city)
        {
            WeatherResponse weatherResponse = _WeatherForecastRepository.GetForecast(city);
            City vievModel = new City();
            if(weatherResponse != null)
            {
                if (weatherResponse.Main != null)
                {
                    if (weatherResponse.Weathers?.Any() == true)
                    {
                        vievModel.Weather = weatherResponse.Weathers[0].Main;
                    }

                    vievModel.Name = weatherResponse.Name;
                    vievModel.Temperature = weatherResponse.Main.Temp;
                    vievModel.Humidity = weatherResponse.Main.Humidity;
                    vievModel.Pressure = weatherResponse.Main.Pressure;
                    vievModel.Wind = weatherResponse.Wind?.Speed;
                }
            }
            return View(vievModel);
        }
    }
}