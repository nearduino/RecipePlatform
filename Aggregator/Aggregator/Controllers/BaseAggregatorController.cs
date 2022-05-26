using System.Net.Http;
using Aggregator.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Aggregator.Controllers
{
    public class BaseAggregatorController : ControllerBase
    {
        protected static HttpClient _httpClient = new HttpClient();
        protected void LogIn()
        {
            var json = new LogIn
            {
                Username = "dave",
                Password = "hamburgeri98/A"
            };
            using (var content = new StringContent(JsonConvert.SerializeObject(json), System.Text.Encoding.UTF8, "application/json"))
            {
                HttpResponseMessage result = _httpClient.PostAsync("https://internship-auth.azurewebsites.net/api/users/authenticate", content).Result;
                string returnValue = result.Content.ReadAsStringAsync().Result;
                LogInResponse? response = System.Text.Json.JsonSerializer.Deserialize<LogInResponse>(returnValue);
                _httpClient.DefaultRequestHeaders.Add("token", response.token);
            }
        }
        
    }
}
