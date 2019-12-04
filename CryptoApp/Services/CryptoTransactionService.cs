using CryptoApp.Data;
using CryptoApp.Models;
using System;

namespace CryptoApp.Services
{
    public class CryptoTransactionService
    {
        public readonly GetCryptoInfoService _getCryptoInfoService;
        private readonly ApplicationDbContext _context;

        public CryptoTransactionService(GetCryptoInfoService getCryptoInfoService, ApplicationDbContext context)
        {
            _getCryptoInfoService = getCryptoInfoService;
            _context = context;
        }

        public TransactionModel BuyCryptoPerPiece(TransactionModel transactionModel)//user wants to buy one bitcoin
        {

            decimal cryptoPrice = _getCryptoInfoService.GetCryptoPrice(transactionModel.CryptoSymbol);
            decimal saleValue = transactionModel.AmountToBuyOrSell * cryptoPrice;
           
            decimal outCashOnHand = transactionModel.USD - saleValue;

            transactionModel = bindTransactionModel(transactionModel, outCashOnHand, transactionModel.CryptoSymbol, transactionModel.AmountToBuyOrSell);

            _context.SaveChanges();

            return transactionModel;
        }

        public TransactionModel SellCryptoPerPiece(TransactionModel transactionModel)//user wants to sell one bitcoin
        {

            decimal cryptoPrice = _getCryptoInfoService.GetCryptoPrice(transactionModel.CryptoSymbol);
            decimal saleValue = transactionModel.AmountToBuyOrSell * cryptoPrice;
            // run validation to see if user has enough to sell
            decimal outCashOnHand = transactionModel.USD + saleValue; 

            transactionModel = bindTransactionModel(transactionModel, outCashOnHand, transactionModel.CryptoSymbol, 0 - transactionModel.AmountToBuyOrSell);
            _context.SaveChanges();
            return transactionModel;
        }

        public TransactionModel bindTransactionModel(TransactionModel transactionModel, decimal inCashOnHand, string cryptoSymbol, decimal amountToSell)//user wants to buy one bitcoin
        {
        
            if (transactionModel.CryptoSymbol == "ETH")
            {
                transactionModel.ETH += amountToSell;
                transactionModel.AmountOnHand = transactionModel.ETH;
            }
            else if (transactionModel.CryptoSymbol == "BTC")
            {
                transactionModel.BTC += amountToSell;
                transactionModel.AmountOnHand = transactionModel.BTC;
            }
            else if (transactionModel.CryptoSymbol == "DOGE")
            {
                transactionModel.DOGE += amountToSell;
                transactionModel.AmountOnHand = transactionModel.DOGE;
            }
            else if (transactionModel.CryptoSymbol == "LTC")
            {
                transactionModel.LTC += amountToSell;
                transactionModel.AmountOnHand = transactionModel.LTC;
            }
            else
            {
                //there is not a valid crypto symbol
            }

            transactionModel.USD = inCashOnHand;
            transactionModel.TotalAccountValue = GetTotalAccountValue(transactionModel);

            _context.SaveChanges();
            return transactionModel;
        }

        public decimal GetTotalAccountValue(TransactionModel transactionModel)//Get the total value of the users account
        {
          
            decimal bTCValue = _getCryptoInfoService.GetCryptoPrice("BTC") * transactionModel.BTC;
            decimal eTHValue = _getCryptoInfoService.GetCryptoPrice("ETH") * transactionModel.ETH;
            decimal lTCValue = _getCryptoInfoService.GetCryptoPrice("LTC") * transactionModel.LTC;
            decimal dOGEValue = _getCryptoInfoService.GetCryptoPrice("DOGE") * transactionModel.DOGE;

            decimal totalValue = bTCValue + eTHValue + lTCValue + dOGEValue + transactionModel.USD;

            return totalValue;
        }

        public WalletModel GetWalletContents(TransactionModel transactionModel)//Get the total value of the users account
        {
            WalletModel wallet = new WalletModel
            {
                USD = transactionModel.USD,
                BTC = transactionModel.BTC,
                ETH = transactionModel.ETH,
                LTC = transactionModel.LTC,
                DOGE = transactionModel.DOGE,

                BTCValue = _getCryptoInfoService.GetCryptoPrice("BTC") * transactionModel.BTC,
                ETHValue = _getCryptoInfoService.GetCryptoPrice("ETH") * transactionModel.ETH,
                LTCValue = _getCryptoInfoService.GetCryptoPrice("LTC") * transactionModel.LTC,
                DOGEValue = _getCryptoInfoService.GetCryptoPrice("DOGE") * transactionModel.DOGE
            };

            wallet.TotalAccountValue = wallet.BTCValue + wallet.ETHValue + wallet.LTCValue + wallet.DOGEValue + wallet.USD;

            _context.SaveChanges();
            return wallet;
        }

        public Boolean CanSellCrypto(TransactionModel transactionModel)
        {
            decimal aboveZero =0;

            if (transactionModel.CryptoSymbol == "ETH")
            {
                aboveZero = transactionModel.ETH - transactionModel.AmountToBuyOrSell;
                if (aboveZero >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (transactionModel.CryptoSymbol == "BTC")
            {
                aboveZero = transactionModel.BTC - transactionModel.AmountToBuyOrSell;
                if (aboveZero >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (transactionModel.CryptoSymbol == "DOGE")
            {
                aboveZero = transactionModel.DOGE - transactionModel.AmountToBuyOrSell;
                if (aboveZero >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (transactionModel.CryptoSymbol == "LTC")
            {
                aboveZero = transactionModel.LTC - transactionModel.AmountToBuyOrSell;
                if (aboveZero >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception("No Currency Available");
            }
        }

        public Boolean CanBuyCrypto(TransactionModel transactionModel)
        {
            decimal aboveZero = transactionModel.USD - (transactionModel.AmountToBuyOrSell * _getCryptoInfoService.GetCryptoPrice(transactionModel.CryptoSymbol));
      
            if(transactionModel.CryptoSymbol == null)
            {
                throw new Exception("No Currency Specified");
            }

            if(aboveZero >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
