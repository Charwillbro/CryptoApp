using System.ComponentModel.DataAnnotations;

namespace CryptoApp.Models
{
    public class WalletModel
    {
        [Display(Name = "Total Value of Assets: ")]
        [DisplayFormat(DataFormatString = "{0:C6}")]
        public decimal TotalAccountValue { get; set; }

        [Display(Name = "Cash on Hand: ")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal USD { get; set; }

        [Display(Name = "Number of Bitcoin Owned: ")]
        public decimal BTC { get; set; }

        [Display(Name = "Value of Bitcoin Owned: ")]
        [DisplayFormat(DataFormatString = "{0:C4}")]
        public decimal BTCValue { get; set; }

        [Display(Name = "Number of Ethereum Owned: ")]
        public decimal ETH { get; set; }

        [Display(Name = "Value of Ethereum Owned:")]
        [DisplayFormat(DataFormatString = "{0:C3}")]
        public decimal ETHValue { get; set; }

        [Display(Name = "Number of Litecoin Owned: ")]
        public decimal LTC { get; set; }

        [Display(Name = "Value of Litecoin Owned:")]
        [DisplayFormat(DataFormatString = "{0:C4}")]
        public decimal LTCValue { get; set; }

        [Display(Name = "Number of Dogecoin Owned: ")]
        public decimal DOGE { get; set; }

        [Display(Name = "Value of Dogecoin Owned: ")]
        [DisplayFormat(DataFormatString = "{0:C6}")]
        public decimal DOGEValue { get; set; }

        [Display(Name = "How many would you like to purchase? ")]
        public decimal AmountToBuyOrSell { get; set; }

    }
}
