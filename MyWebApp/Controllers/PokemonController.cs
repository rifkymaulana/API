using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyWebApp.Controllers;

public class PokemonController : Controller
{
    protected string Baseurl = "https://pokeapi.co/";
    public async Task<ActionResult> Index()
    {
        List<Pokemon> EmpInfo = new();

        using var client = new HttpClient();
        //Passing service base url  
        client.BaseAddress = new Uri(Baseurl);

        client.DefaultRequestHeaders.Clear();
        //Define request data format  
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //Sending request to find web api REST service resource GetAllPokemons using HttpClient  
        HttpResponseMessage Res = await client.GetAsync("api/v2/pokemon/");

        //Checking the response is successful or not which is sent using HttpClient  
        if (Res.IsSuccessStatusCode)
        {
            // Storing the response details received from web API
            var Response = Res.Content.ReadAsStringAsync().Result;

            // Parsing the response JSON
            var responseObject = JsonConvert.DeserializeObject<JObject>(Response);

            // Extracting the "results" array from the response
            var resultsArray = responseObject["results"].ToObject<JArray>();

            // Deserializing the "results" array into a list of Pokemon
            EmpInfo = resultsArray.ToObject<List<Pokemon>>();
        }
        //returning the employee list to view  
        return View(EmpInfo);
    }

}