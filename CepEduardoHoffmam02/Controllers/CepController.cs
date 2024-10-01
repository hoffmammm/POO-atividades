using System.Text.Json;
using CepEduardoHoffmam.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace CepApi.Controllers
{
    public class CepController : Controller
    {
        private readonly HttpClient _httpClient;
        public CepController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<ActionResult> Search(string cep)
        {
            if(string.IsNullOrEmpty(cep))
            {
                ModelState.AddModelError("", "Insira um CEP");
                return View("Index");
            }
            
            var response = await _httpClient.GetStringAsync($"https://viacep.com.br/ws/{cep}/json/");
            var cepModel = JsonConvert.DeserializeObject<CepModel>(response);

            return View("Index", cepModel);
        }
    }
}