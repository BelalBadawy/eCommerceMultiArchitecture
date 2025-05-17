
using Microsoft.AspNetCore.Http;
using eStoreCA.Shared.Interfaces;
using Newtonsoft.Json;


namespace eStoreCA.Infrastructure.Common
{
    public class InMemorySessionWrapper : ISessionWrapper
    {

        private IHttpContextAccessor _httpContextAccessor;
        public InMemorySessionWrapper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public T GetFromSession<T>(string key)
        {
            var value = _httpContextAccessor?.HttpContext?.Session?.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public void RemoveFromSession(string key)
        {
            _httpContextAccessor?.HttpContext?.Session?.Remove(key);
        }

        public void SetInSession<T>(string key, T value)
        {
            _httpContextAccessor?.HttpContext?.Session?.SetString(key, JsonConvert.SerializeObject(value));
        }



        #region Custom
        #endregion Custom

    }
}
