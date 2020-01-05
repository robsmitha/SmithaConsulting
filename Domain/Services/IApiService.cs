using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IApiService
    {
        Task<T> GetAsync<T>(string function);
        T Get<T>(string function);
        T Post<T>(string function, T data);
        Task<T> PostAsync<T>(string function, T data);
        Task<T> PutAsync<T>(string function, T data);
        T Put<T>(string function, T data);
        void Delete(string function);
    }
}
