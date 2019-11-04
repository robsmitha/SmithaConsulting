using Architecture.Services.API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Utilities
{
    public class APIExtensions
    {
        public APIService Service;
        public APIExtensions(string endpoint, string apiKey)
        {
            Service = new APIService(endpoint, apiKey);
        }
        public T Get<T>(string function)
        {
            var response = Service.Get(!function.StartsWith("/") ? $"/{function}" : function);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<T> GetAsync<T>(string function)
        {
            var response = await Service.GetAsync(!function.StartsWith("/") ? $"/{function}" : function);
            return JsonConvert.DeserializeObject<T>(response);
        }
        public T Add<T>(string function, T data)
        {
            var response = Service.Post(!function.StartsWith("/") ? $"/{function}" : function, JsonConvert.SerializeObject(data));
            return JsonConvert.DeserializeObject<T>(response);
        }
        public T Update<T>(string function, T data)
        {
            var response = Service.Put(!function.StartsWith("/") ? $"/{function}" : function, JsonConvert.SerializeObject(data));
            return JsonConvert.DeserializeObject<T>(response);
        }
        public IEnumerable<T> GetAll<T>(string function)
        {
            var response = Service.Get(!function.StartsWith("/") ? $"/{function}" : function);
            return JsonConvert.DeserializeObject<IEnumerable<T>>(response);
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>(string function)
        {
            var response = await Service.GetAsync(!function.StartsWith("/") ? $"/{function}" : function);
            return JsonConvert.DeserializeObject<IEnumerable<T>>(response);
        }
        public void Delete(string function)
        {
            Service.Delete(!function.StartsWith("/") ? $"/{function}" : function);
        }
    }
}
