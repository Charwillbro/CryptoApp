using CryptoApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoApp.Services
{
   
    public class CryptoTransactionService
    {
        public readonly GetCryptoInfoService _getCryptoInfoService;

        public CryptoTransactionService(GetCryptoInfoService getCryptoInfoService)
        {
            _getCryptoInfoService = getCryptoInfoService;
          

        }

        public TransactionModel BuyCryptoPerPiece(TransactionModel transactionModel)//user wants to buy one bitcoin
        {

            decimal cryptoPrice = _getCryptoInfoService.GetCryptoPrice(transactionModel.CryptoSymbol);
            decimal saleValue = transactionModel.AmountToBuyOrSell * cryptoPrice;
            // run validation to see if user has enough to sell
            decimal outCashOnHand = transactionModel.USD - saleValue;

            Console.WriteLine("######################## BUYING CRYPTO ########################");
            Console.WriteLine("########################" + outCashOnHand + "########################");

            transactionModel = bindTransactionModel(transactionModel, outCashOnHand, transactionModel.CryptoSymbol, transactionModel.AmountToBuyOrSell);

            return transactionModel;
        }

        public TransactionModel SellCryptoPerPiece(TransactionModel transactionModel)//user wants to sell one bitcoin
        {

            decimal cryptoPrice = _getCryptoInfoService.GetCryptoPrice(transactionModel.CryptoSymbol);
            decimal saleValue = transactionModel.AmountToBuyOrSell * cryptoPrice;
            // run validation to see if user has enough to sell
            decimal outCashOnHand = transactionModel.USD + saleValue;

            Console.WriteLine("######################## SELLING CRYPTO ########################");
            Console.WriteLine("########################" + outCashOnHand + "########################");

            transactionModel = bindTransactionModel(transactionModel, outCashOnHand, transactionModel.CryptoSymbol, 0 - transactionModel.AmountToBuyOrSell);

            return transactionModel;
        }



        public TransactionModel bindTransactionModel(TransactionModel transactionModel, decimal inCashOnHand, string cryptoSymbol, decimal amountToSell)//user wants to buy one bitcoin
        {
            Console.WriteLine("####################### BEFORE #######################");
            Console.WriteLine("Cash on hand: $" + transactionModel.USD + "########################");
            Console.WriteLine("Num of BTC: $" + transactionModel.BTC + "########################");
            Console.WriteLine("Num of ETH: $" + transactionModel.ETH + "########################");
            Console.WriteLine("Num of LTC: $" + transactionModel.LTC + "########################");
            Console.WriteLine("Num of DOGE: $" + transactionModel.DOGE + "########################");
            if (transactionModel.CryptoSymbol == "ETH")
            {
                transactionModel.ETH += amountToSell;
            }
            else if (transactionModel.CryptoSymbol == "BTC")
            {
                transactionModel.BTC += amountToSell;
            }
            else if (transactionModel.CryptoSymbol == "DOGE")
            {
                transactionModel.DOGE += amountToSell;
            }
            else if (transactionModel.CryptoSymbol == "LTC")
            {
                transactionModel.LTC += amountToSell;
            }
            else
            {
                //there is not a valid crypto symbol
            }
            transactionModel.USD = inCashOnHand;

            Console.WriteLine("####################### AFTER #######################");
            Console.WriteLine("Cash on hand: $" + transactionModel.USD + "########################");
            Console.WriteLine("Num of BTC: $" + transactionModel.BTC + "########################");
            Console.WriteLine("Num of ETH: $" + transactionModel.ETH + "########################");
            Console.WriteLine("Num of LTC: $" + transactionModel.LTC + "########################");
            Console.WriteLine("Num of DOGE: $" + transactionModel.DOGE + "########################");

            return transactionModel;
        }

    }
}
