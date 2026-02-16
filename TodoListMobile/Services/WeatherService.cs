using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TodoListMobile.Models;

namespace TodoListMobile.Services
{
    public class WeatherService
    {
        private const string BaseUrl = "https://api.open-meteo.com/v1/forecast";
        private readonly HttpClient _httpClient;

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherData?> GetWeatherAsync(string city = "Kyiv")
        {
            try
            {
                var (lat, lon) = GetCityCoordinates(city);
                
                var url = $"{BaseUrl}?latitude={lat}&longitude={lon}&current=temperature_2m";
                
                var response = await _httpClient.GetFromJsonAsync<OpenMeteoResponse>(url);

                if (response?.Current != null)
                {
                    return new WeatherData
                    {
                        City = city,
                        Temperature = response.Current.Temperature2m,
                        Description = "Current weather",
                        Icon = "🌤️"
                    };
                }
            }
            catch
            {
            }

            return null;
        }

        private static (double lat, double lon) GetCityCoordinates(string city)
        {
            return city.ToLower() switch
            {
                "kyiv" or "київ" => (50.4501, 30.5234),
                "lviv" or "львів" => (49.8397, 24.0297),
                "odesa" or "одеса" => (46.4825, 30.7233),
                _ => (50.4501, 30.5234) // Kyiv by default
            };
        }

        private class OpenMeteoResponse
        {
            [JsonPropertyName("current")]
            public CurrentData? Current { get; set; }
        }

        private class CurrentData
        {
            [JsonPropertyName("temperature_2m")]
            public double Temperature2m { get; set; }
        }
    }
}
