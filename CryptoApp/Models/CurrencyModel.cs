using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CryptoApp.Models
{
    public class CurrencyModel
    {
        //Currency Symbol we are converting from. in this case BTC
        [JsonProperty]
        [Display(Name = "CryptoCurrency Denomination: ")]
        public string FROMSYMBOL { get; set; }

        //Currency Symbol we are converting to. in this case USD
        [JsonProperty]
        [Display(Name = "Currency Denomination: ")]
        public string TOSYMBOL { get; set; }

        //Price of cryptocurrency in USD
        [JsonProperty]
        [Display(Name = "Current Price: ")]
        [DataType(DataType.Currency)]
        public decimal PRICE { get; set; }

        //Highest Price of cryptocurrency today
        [JsonProperty]
        [Display(Name = "Highest Price Today: ")]
        [DataType(DataType.Currency)]
        public decimal HIGHDAY { get; set; }

        //Lowest Price of cryptocurrency today
        [JsonProperty]
        [Display(Name = "Lowest Price Today: ")]
        [DataType(DataType.Currency)]
        public decimal LOWDAY { get; set; }

        //Amount the price has changed today in dollars
        [JsonProperty]
        [Display(Name = "Price Change Today: ")]
        [DataType(DataType.Currency)]
        public decimal CHANGEDAY { get; set; }

        //Amount the price has changed today as a percentage
        [JsonProperty]
        [Display(Name = "Percent Change Today: ")]
        public decimal CHANGEPCTDAY { get; set; }

        //Amount the price has changed in the past hour in dollars
        [JsonProperty]
        [Display(Name = "Price Change in the Past Hour: ")]
        [DataType(DataType.Currency)]
        public decimal CHANGEHOUR { get; set; }

        //Amount the price has changed in the past hour as a percentage
        [JsonProperty]
        [Display(Name = "Percent Change Past Hour: ")]
        public decimal CHANGEPCTHOUR { get; set; }

        //The total value of this crypto currency on the market AKA MarketCap
        [JsonProperty]
        [Display(Name = "Market Cap: ")]
        [DataType(DataType.Currency)]
        public decimal MKTCAP { get; set; }

        [JsonProperty]
        [Display(Name = "Amount: ")]
        [DataType(DataType.Currency)]
        public decimal AmountToBuyOrSell { get; set; }

        [Display(Name = "Select a Cryptocurrency")]
        public string CryptoSymbol { get; set; }

        [Display(Name = "Select a Cryptocurrency")]
        public List<SelectListItem> CryptoSymbols { get; } = new List<SelectListItem>
    {
        new SelectListItem { Value = "LTC", Text = "Litecoin" },
        new SelectListItem { Value = "BTC", Text = "Bitcoin" },
        new SelectListItem { Value = "DOGE", Text = "Dogecoin"  }, //DOGE COin would throw errors despite the api giving correct info for DOGE
        new SelectListItem { Value = "ETH", Text = "Ethereum"  },
    };

    }
}
