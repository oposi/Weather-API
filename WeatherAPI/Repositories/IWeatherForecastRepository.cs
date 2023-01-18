using WeatherAPI.OpenWeatherMap_Model;

namespace WeatherAPI.Repositories
{
    public  interface IWeatherForecastRepository
    {
        WeatherResponse GetForecast(string city);
    }
}
