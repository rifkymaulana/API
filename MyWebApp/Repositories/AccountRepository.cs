using System.Text;
using API.DTOs.Accounts;
using API.Utilities;
using MyWebApp.Contracts;
using Newtonsoft.Json;

namespace MyWebApp.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly HttpClient httpClient;
    private readonly string request;

    public AccountRepository(string request = "accounts/")
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7000/api/")
        };
        this.request = request;
    }

    public async Task<ResponseHandler<string>> Login(LoginAccountDto entity)
    {
            ResponseHandler<string> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request + "Login", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseHandler<string>>(apiResponse);
            }
            return entityVM;
    }

    public Task<ResponseHandler<RegisterAccountDto>> Register(RegisterAccountDto entity)
    {
        throw new NotImplementedException();
    }
}
