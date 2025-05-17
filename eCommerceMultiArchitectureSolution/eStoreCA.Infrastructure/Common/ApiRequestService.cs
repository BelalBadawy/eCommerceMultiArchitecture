
using eStoreCA.Shared.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace eStoreCA.Infrastructure.Common
{
    public class ApiRequestService : IApiRequest
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        public ApiRequestService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public enum ApiRequestType
        {
            Post,
            Patch,
            Delete,
            Get,
            Put
        }

        private async Task<T> ApiRequestAsync<T>(string url, object objToCreate, string token = "", ApiRequestType requestType = ApiRequestType.Get)
        {
            if (!url.StartsWith("http"))
            {
                url = _configuration["APIBaseUrl"] + url;
            }

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            switch (requestType)
            {
                case ApiRequestType.Post:
                    request.Method = HttpMethod.Post;
                    break;
                case ApiRequestType.Patch:
                    request.Method = HttpMethod.Patch;
                    break;
                case ApiRequestType.Delete:
                    request.Method = HttpMethod.Delete;
                    break;
                case ApiRequestType.Get:
                    request.Method = HttpMethod.Get;
                    break;
                case ApiRequestType.Put:
                    request.Method = HttpMethod.Put;
                    break;
            }

            if (objToCreate != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
            }

            var client = _clientFactory.CreateClient();

            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            HttpResponseMessage response = await client.SendAsync(request);

            var jsonString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var dataResponse = JsonConvert.DeserializeObject<T>(jsonString);
                return dataResponse;
            }

            throw new Exception(jsonString);
        }


        public async Task<T> PostAsync<T>(string url, object objToCreate, string token = "")
        {
            return await ApiRequestAsync<T>(url, objToCreate, token, ApiRequestType.Post);
        }

        public async Task<T> DeleteAsync<T>(string url, object id, string token = "")
        {
            return await ApiRequestAsync<T>(url, id, token, ApiRequestType.Delete);
        }

        public async Task<T> UpdateAsync<T>(string url, object objToUpdate, string token = "")
        {
            return await ApiRequestAsync<T>(url, objToUpdate, token, ApiRequestType.Put);
        }

        public async Task<T> GetAllAsync<T>(string url, string token = "")
        {
            return await ApiRequestAsync<T>(url, null, token, ApiRequestType.Get);
        }

        public async Task<T> GetAsync<T>(string url, object id, string token = "")
        {
            return await ApiRequestAsync<T>(url, id, token, ApiRequestType.Get);
        }




        #region Custom
        #endregion Custom

    }
}
