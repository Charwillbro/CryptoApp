using CryptoApp.Models;
using CryptoApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CryptoApp.Controllers
{
    public class CryptoController : Controller
    {
        public readonly CryptoTransactionService _cryptoTransactionService;
        public readonly GetCryptoInfoService _getCryptoInfoService;
        private readonly UserManager<ApplicationUser> _userManager;
        public CryptoController(GetCryptoInfoService getCryptoInfoService, UserManager<ApplicationUser> userManager, CryptoTransactionService cryptoTransactionService)
        {
            _cryptoTransactionService = cryptoTransactionService;
            _getCryptoInfoService = getCryptoInfoService;
            _userManager = userManager;

        }

        public async System.Threading.Tasks.Task<IActionResult> Index()
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var transactionModel = new TransactionModel
            {
                USD = user.USD,
                BTC = user.BTC,
                ETH = user.ETH,
                LTC = user.LTC,
                DOGE = user.DOGE
            };

            // testing values //
            string cryptoSymbol = "ETH";
            decimal amountToBuyOrSell = 1;

            transactionModel.AmountToBuyOrSell = amountToBuyOrSell;
            transactionModel.CryptoSymbol = cryptoSymbol;
            // user.USD += 500;
            // testing values //

            CurrencyModel cryptoObject = _getCryptoInfoService.GetCryptoObject(cryptoSymbol).Result;
           // _getCryptoInfoService.BuyCryptoPerPiece(user.USD, cryptoSymbol, amountToBuyOrSell);
            // _getCryptoInfoService.DEPRICATEDSellCryptoPerPiece(user.USD, cryptoSymbol, amountToBuyOrSell);
            _cryptoTransactionService.BuyCryptoPerPiece(transactionModel);
            _cryptoTransactionService.SellCryptoPerPiece(transactionModel);

            return View(cryptoObject);
        }
        public IActionResult BuyCurrency()
        {

            string cryptoSymbol = "BTC";

            CurrencyModel cryptoObject = _getCryptoInfoService.GetCryptoObject(cryptoSymbol).Result;

            return View(cryptoObject);
        }

    }
}