using CryptoApp.Filters;
using CryptoApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoApp.Services
{
    [HandleException]
    [ValidateModel]
    public class GetCryptoInfoService
    {
        public GetCryptoInfoService()
        {

        }

        public async Task<CurrencyModel> GetCryptoObject(string cryptoSymbol)
        {

            string getAPIString = $"https://min-api.cryptocompare.com/data/pricemultifull?fsyms={cryptoSymbol}&tsyms=USD,EUR&api_key=a9635f00f6b5c85f2579780156071dd8e0dea96246d246f076ff4153f470dbca";

            CurrencyModel CurrencyObject = new CurrencyModel();
            HttpClient client = new HttpClient();

            var apiresult = await client.GetAsync(getAPIString);
            dynamic JSONResult = await apiresult.Content.ReadAsStringAsync();

            //Takes the JSON from the api and turns it into a JObject so we can select where in the JSON we want to get data
            JObject currencyData = JObject.Parse(JSONResult);

            //Breaks away unnecessary data from the API response in this case RAW/BTC/USD...
            JToken onlyNecessaryData = currencyData["RAW"][cryptoSymbol]["USD"];

            var dataAsString = onlyNecessaryData.ToString();

            //Convert the JSON string data into our currency object
            JsonConvert.PopulateObject(dataAsString, CurrencyObject);

            return CurrencyObject;

        }
        public decimal GetCryptoPrice(string cryptoSymbol)
        {
            CurrencyModel currencyObject = new CurrencyModel();

            currencyObject = GetCryptoObject(cryptoSymbol).Result;

            return currencyObject.PRICE;
        }
    }
}
