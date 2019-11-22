using CryptoApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoApp.Services
{
    public class GetCryptoInfoService
    {

        public GetCryptoInfoService()
        {

        }

        public async Task<string> GetApiData()
        {
            try
            {
                var client = new HttpClient();

                var result = await client.GetAsync("https://min-api.cryptocompare.com/data/price?fsym=BTC&tsyms=USD,JPY,EUR&api_key=a9635f00f6b5c85f2579780156071dd8e0dea96246d246f076ff4153f470dbca");
                // Use newtonsoft here to project the Json result into an object, then you can return an object instead of the string.

                return await result.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<CurrencyModel> GetCryptoObject()
        {
            try
            {

                string apiGetString = "https://min-api.cryptocompare.com/data/pricemultifull?fsyms=BTC&tsyms=USD,EUR&api_key=a9635f00f6b5c85f2579780156071dd8e0dea96246d246f076ff4153f470dbca";

                CurrencyModel CurrencyObject = new CurrencyModel();
                var client = new HttpClient();

                var apiresult = await client.GetAsync(apiGetString);
                dynamic JSONResult = await apiresult.Content.ReadAsStringAsync();

                //Takes the JSON from the api and turns it into a JObject so we can select where in the JSON we want to get data
                JObject currencyData = JObject.Parse(JSONResult);

                //Breaks away unnecessary data from the API response in this case RAW/BTC/USD...
                JToken onlyNecessaryData = currencyData["RAW"]["BTC"]["USD"];

                var dataAsString = onlyNecessaryData.ToString();

                //Convert the JSON string data into our currency object
                JsonConvert.PopulateObject(dataAsString, CurrencyObject);

                return CurrencyObject;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<CurrencyModel> GetCryptoObject(string cryptoSymbol)
        {
            try
            {
                string getAPIString = $"https://min-api.cryptocompare.com/data/pricemultifull?fsyms={cryptoSymbol}&tsyms=USD,EUR&api_key=a9635f00f6b5c85f2579780156071dd8e0dea96246d246f076ff4153f470dbca";

                CurrencyModel CurrencyObject = new CurrencyModel();
                var client = new HttpClient();

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
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public decimal GetCryptoPrice(string cryptoSymbol)
        {
            CurrencyModel currencyObject = new CurrencyModel();

            currencyObject = GetCryptoObject(cryptoSymbol).Result;

            return currencyObject.PRICE;
        }

        //public async void BuyCryptoPerPiece(string cryptoSymbol, decimal inCashOnHand, decimal amountToBuy
        //public void BuyCryptoPerPiece(decimal inCashOnHand, string cryptoSymbol, decimal amountToBuy)//user wants to buy one bitcoin
        //{
            
           
        //    decimal cryptoPrice = GetCryptoPrice(cryptoSymbol);
        //    decimal purchasePrice = amountToBuy * cryptoPrice;
        //    // run validation to see if user can afford it
        //    decimal accountValue = inCashOnHand - purchasePrice;

        //    Console.WriteLine("########################" + accountValue+"########################");
            


        //}
        //public void BuyCryptoByPrice(string cryptoSymbol, decimal inCashOnHand, decimal amountToSpend)//User wants to but $500 of a currency
        //{

        //}
        //public void DEPRICATEDSellCryptoPerPiece(decimal inCashOnHand, string cryptoSymbol, decimal amountToSell)//user wants to buy one bitcoin
        //{


        //    decimal cryptoPrice = GetCryptoPrice(cryptoSymbol);
        //    decimal saleValue = amountToSell * cryptoPrice;
        //    // run validation to see if user has enough to sell
        //    decimal accountValue = inCashOnHand + saleValue;

        //    Console.WriteLine("########################" + accountValue + "########################");

        //}
        //public TransactionModel SellCryptoPerPiece(TransactionModel transactionModel)//user wants to buy one bitcoin
        //{
           
        //    decimal cryptoPrice = GetCryptoPrice(transactionModel.CryptoSymbol);
        //    decimal saleValue = transactionModel.AmountToBuyOrSell * cryptoPrice;
        //    // run validation to see if user has enough to sell
        //    decimal outCashOnHand = transactionModel.USD + saleValue;

        //    Console.WriteLine("########################" + outCashOnHand + "########################");

        //    transactionModel = bindTransactionModel(transactionModel, outCashOnHand, transactionModel.CryptoSymbol, 0 - transactionModel.AmountToBuyOrSell);
           
        //    return transactionModel;
        //}



        //public TransactionModel bindTransactionModel(TransactionModel transactionModel, decimal inCashOnHand, string cryptoSymbol, decimal amountToSell)//user wants to buy one bitcoin
        //{
        //    Console.WriteLine("####################### BEFORE #######################");
        //    Console.WriteLine("Cash on hand: $" + transactionModel.USD + "########################");
        //    Console.WriteLine("Num of BTC: $" + transactionModel.BTC + "########################");
        //    Console.WriteLine("Num of ETH: $" + transactionModel.ETH + "########################");
        //    Console.WriteLine("Num of LTC: $" + transactionModel.LTC + "########################");
        //    Console.WriteLine("Num of DOGE: $" + transactionModel.DOGE + "########################");
        //    if (transactionModel.CryptoSymbol == "ETH")
        //    {
        //        transactionModel.ETH += amountToSell;
        //    }
        //    else if(transactionModel.CryptoSymbol == "BTC")
        //    {
        //        transactionModel.BTC += amountToSell;
        //    }
        //    else if (transactionModel.CryptoSymbol == "DOGE")
        //    {
        //        transactionModel.DOGE += amountToSell;
        //    }
        //    else if (transactionModel.CryptoSymbol == "LTC")
        //    {
        //        transactionModel.LTC += amountToSell;
        //    }
        //    else
        //    {
        //        //there is not a valid crypto symbol
        //    }
        //    transactionModel.USD = inCashOnHand;     

        //    Console.WriteLine("####################### AFTER #######################");
        //    Console.WriteLine("Cash on hand: $" + transactionModel.USD + "########################");
        //    Console.WriteLine("Num of BTC: $" + transactionModel.BTC + "########################");
        //    Console.WriteLine("Num of ETH: $" + transactionModel.ETH + "########################");
        //    Console.WriteLine("Num of LTC: $" + transactionModel.LTC + "########################");
        //    Console.WriteLine("Num of DOGE: $" + transactionModel.DOGE + "########################");

        //    return transactionModel;
        //}
    }
}
