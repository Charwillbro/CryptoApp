using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CryptoApp.Models
{
    public class TransactionViewModel
    {
        [Display(Name = "Total Value of Assets: ")]
        public decimal TotalAccountValue { get; set; }


        [Display(Name = "Cash on Hand: ")]
        public decimal USD { get; set; }


        [Display(Name = "Number of Bitcoin Owned: ")]
        public decimal BTC { get; set; }


        [Display(Name = "Number of Ethereum Owned: ")]
        public decimal ETH { get; set; }


        [Display(Name = "Number of Litecoin Owned: ")]
        public decimal LTC { get; set; }


        [Display(Name = "Number of Dogecoin Owned: ")]
        public decimal DOGE { get; set; }


        [Display(Name = "How many would you like to purchase? ")]
        public decimal AmountToBuyOrSell { get; set; }


        [Display(Name = "Select a Cryptocurrency")]
        public string CryptoSymbol { get; set; }

        [Display(Name = "Select a Cryptocurrency")]
        public List<SelectListItem> CryptoSymbols { get; } = new List<SelectListItem>
    {
        new SelectListItem { Value = "LTC", Text = "Litecoin" },
        new SelectListItem { Value = "BTC", Text = "Bitcoin" },
        new SelectListItem { Value = "DOGE", Text = "Dogecoin"  },
        new SelectListItem { Value = "ETH", Text = "Ethereum"  },
    };

    }
}
