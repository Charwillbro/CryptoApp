using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoApp.Models
{
    public class TransactionModel
    {
        public decimal TotalAccountValue { get; set; }
        public decimal USD { get; set; }
        public decimal BTC { get; set; }
        public decimal ETH { get; set; }
        public decimal LTC { get; set; }
        public decimal DOGE { get; set; }
        public decimal AmountToBuyOrSell { get; set; }
        public string CryptoSymbol { get; set; }
    }
}
