using System.ComponentModel.DataAnnotations;

namespace CryptoApp.Models
{
    public class SellViewModel
    {
        [Display(Name = "Cash on Hand: ")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal USD { get; set; }

        [Display(Name = "CryptoCurrency Denomination: ")]
        public string CryptoSymbol { get; set; }

        [Display(Name = "Current Price: ")]
        public decimal CryptoPrice { get; set; }

        [Display(Name = "Number you own: ")]
        public decimal AmountOnHand { get; set; }

        [Display(Name = "How many would you like to Buy/Sell? ")]
        public decimal AmountToSell { get; set; }
    }
}
