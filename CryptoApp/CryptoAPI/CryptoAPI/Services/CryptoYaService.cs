using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CryptoAPI.Services
{
    public class CryptoYaService
    {
        private readonly HttpClient _http;

        public CryptoYaService(HttpClient http)
        {
            _http = http;
            
        }

        public async Task<decimal?> GetCryptoPrice(string cryptoCode)
        {
            try
            {
                string url = $"https://criptoya.com/api/satoshitango/{cryptoCode}/ars";
                var data = await _http.GetFromJsonAsync<CriptoPriceResponse>(url);
                return data?.ask;
            }
            catch
            {
                return null;
            }
        }

        private class CriptoPriceResponse
        {
            public decimal ask { get; set; }
        }
    }
}
