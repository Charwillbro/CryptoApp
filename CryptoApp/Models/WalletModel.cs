using System.ComponentModel.DataAnnotations;

namespace CryptoApp.Models
{
    public class WalletModel
    {

        [Display(Name = "Total Value of Assets: ")]
        [DataType(DataType.Currency)]
        public decimal TotalAccountValue { get; set; }


        [Display(Name = "Cash on Hand: ")]
        [DataType(DataType.Currency)]
        public decimal USD { get; set; }


        [Display(Name = "Number of Bitcoin Owned: ")]
        public decimal BTC { get; set; }

        [Display(Name = "Bitcoin Cash Value ")]
        [DataType(DataType.Currency)]
        public decimal BTCValue { get; set; }


        [Display(Name = "Number of Ethereum Owned: ")]
        public decimal ETH { get; set; }

        [Display(Name = "Ethereum Cash Value ")]
        [DataType(DataType.Currency)]
        public decimal ETHValue { get; set; }


        [Display(Name = "Number of Litecoin Owned: ")]
        public decimal LTC { get; set; }

        [Display(Name = "Litecoin Cash Value ")]
        [DataType(DataType.Currency)]
        public decimal LTCValue { get; set; }


        [Display(Name = "Number of Dogecoin Owned: ")]
        public decimal DOGE { get; set; }

        [Display(Name = "Doge Cash Value ")]
        [DataType(DataType.Currency)]
        public decimal DOGEValue { get; set; }

        [Display(Name = "How many would you like to purchase? ")]
        public decimal AmountToBuyOrSell { get; set; }

    }
}
