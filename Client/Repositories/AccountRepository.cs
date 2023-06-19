using API.Models;
using API.ViewModels;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Client.Repositories
{
    public class AccountRepository
    {
        private readonly string request;
        private HttpClient httpClient;

        public AccountRepository(string request = "Accounts/")
        {
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7009/api/")
            };
        }

        //Get All
        public async Task<ResponseDataVM<List<Accounts>>> Get()
        {
            ResponseDataVM<List<Accounts>> entityVM = null;
            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseDataVM<List<Accounts>>>(apiResponse);
            }
            return entityVM;
        }

        //Get by Id
        public async Task<ResponseDataVM<Accounts>> Get(int id)
        {
            ResponseDataVM<Accounts> entity = null;

            using (var response = await httpClient.GetAsync(request + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<ResponseDataVM<Accounts>>(apiResponse);
            }
            return entity;
        }

        //Post - Create
        public async Task<ResponseDataVM<string>> Post(Accounts accounts)
        {
            ResponseDataVM<string> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(accounts), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseDataVM<string>>(apiResponse);
            }
            return entityVM;
        }

        //Put - Edit
        public async Task<ResponseDataVM<string>> Put(int id, Accounts accounts)
        {
            ResponseDataVM<string> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(accounts), Encoding.UTF8, "application/json");
            using (var response = httpClient.PutAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseDataVM<string>>(apiResponse);
            }
            return entityVM;
        }

        //Delete
        public async Task<ResponseDataVM<Accounts>> Delete(int id)
        {
            ResponseDataVM<Accounts> entity = null;

            using (var response = await httpClient.DeleteAsync(request + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<ResponseDataVM<Accounts>>(apiResponse);
            }
            return entity;
        }
    }
}
