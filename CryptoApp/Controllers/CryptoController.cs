using CryptoApp.Data;
using CryptoApp.Models;
using CryptoApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CryptoApp.Controllers
{
    public class CryptoController : Controller
    {
        public readonly CryptoTransactionService _cryptoTransactionService;
        public readonly GetCryptoInfoService _getCryptoInfoService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public CryptoController(GetCryptoInfoService getCryptoInfoService, UserManager<ApplicationUser> userManager, CryptoTransactionService cryptoTransactionService)
        {
            _cryptoTransactionService = cryptoTransactionService;
            _getCryptoInfoService = getCryptoInfoService;
            _userManager = userManager;

        }

        //public async System.Threading.Tasks.Task<IActionResult> Index()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //    }

        //    var transactionModel = new TransactionModel
        //    {
        //        USD = user.USD, //the amount of cash available to spend
        //        BTC = user.BTC, //Number of crypto owned
        //        ETH = user.ETH, //Number of crypto owned
        //        LTC = user.LTC, //Number of crypto owned
        //        DOGE = user.DOGE//Number of crypto owned
        //    };

        //    // testing values //
        //    string cryptoSymbol = "ETH"; //which crypto you want to see/buy/sell
        //    decimal amountToBuyOrSell = 1; //how much of that crypto you want to buy or sell

        //    transactionModel.AmountToBuyOrSell = amountToBuyOrSell;
        //    transactionModel.CryptoSymbol = cryptoSymbol;
        //    // user.USD += 500;
        //    // testing values //

        //    CurrencyModel cryptoObject = _getCryptoInfoService.GetCryptoObject(cryptoSymbol).Result;

        //    _cryptoTransactionService.BuyCryptoPerPiece(transactionModel);
        //    _cryptoTransactionService.SellCryptoPerPiece(transactionModel);

        //    return View(cryptoObject);
        //}

        public async System.Threading.Tasks.Task<IActionResult> Index(string CryptoSymbol)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (CryptoSymbol == null)
            {
                CryptoSymbol = "LTC";
            }

            var transactionModel = new TransactionModel
            {
                USD = user.USD, //the amount of cash available to spend
                BTC = user.BTC, //Number of crypto owned
                ETH = user.ETH, //Number of crypto owned
                LTC = user.LTC, //Number of crypto owned
                DOGE = user.DOGE//Number of crypto owned
            };

            // testing values //
            //string cryptoSymbol = "ETH"; //which crypto you want to see/buy/sell
            decimal amountToBuyOrSell = 1; //how much of that crypto you want to buy or sell

            transactionModel.AmountToBuyOrSell = amountToBuyOrSell;
            transactionModel.CryptoSymbol = CryptoSymbol;

            _cryptoTransactionService.BuyCryptoPerPiece(transactionModel);
          //  _cryptoTransactionService.GetTotalAccountValue(transactionModel);
          //  _cryptoTransactionService.SellCryptoPerPiece(transactionModel);
            // user.USD += 500;
            // testing values //

            CurrencyModel cryptoObject = _getCryptoInfoService.GetCryptoObject(CryptoSymbol).Result;

            return View(cryptoObject);
        }
        public async System.Threading.Tasks.Task<IActionResult> Wallet(string CryptoSymbol)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (CryptoSymbol == null)
            {
                CryptoSymbol = "LTC";
            }

            var transactionModel = new TransactionModel
            {
                USD = user.USD, //the amount of cash available to spend
                BTC = user.BTC, //Number of crypto owned
                ETH = user.ETH, //Number of crypto owned
                LTC = user.LTC, //Number of crypto owned
                DOGE = user.DOGE//Number of crypto owned
            };

            // testing values //
            //string cryptoSymbol = "ETH"; //which crypto you want to see/buy/sell
            decimal amountToBuyOrSell = 1; //how much of that crypto you want to buy or sell

            transactionModel.AmountToBuyOrSell = amountToBuyOrSell;
            transactionModel.CryptoSymbol = CryptoSymbol;

          //  _cryptoTransactionService.BuyCryptoPerPiece(transactionModel);
          //  _cryptoTransactionService.GetTotalAccountValue(transactionModel);
           // _cryptoTransactionService.SellCryptoPerPiece(transactionModel);
            // user.USD += 500;
            // testing values //
 
            WalletModel wallet = _cryptoTransactionService.GetWalletContents(transactionModel);

            return View(wallet);
        }

        public IActionResult BuyCurrency()
        {

            string cryptoSymbol = "BTC";

            CurrencyModel cryptoObject = _getCryptoInfoService.GetCryptoObject(cryptoSymbol).Result;

            return View(cryptoObject);
        }



        public async Task<IActionResult> Transactions(string FROMSYMBOL,[Bind("USD,BTC,ETH,DOGE,CryptoSymbol,AmountToBuyOrSell")] TransactionViewModel transactionModel)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            ///TEST
           //FROMSYMBOL = transactionModel.CryptoSymbol;
            transactionModel.CryptoSymbol = FROMSYMBOL;
            //_cryptoTransactionService.BuyCryptoPerPiece(transactionModel);
            ///Test

            CurrencyModel cryptoObject = _getCryptoInfoService.GetCryptoObject(FROMSYMBOL).Result;
            

            var transactionModel2 = new TransactionViewModel
            {
                CryptoSymbol = cryptoObject.FROMSYMBOL,
                AmountToBuyOrSell = cryptoObject.AmountToBuyOrSell, //Number of crypto owned
                USD = user.USD, //the amount of cash available to spend
                BTC = user.BTC,
                ETH = user.ETH, //Number of crypto owned
                LTC = user.LTC, //Number of crypto owned
                DOGE = user.DOGE,//Number of crypto owned
                
            };

            return View(transactionModel2);
        }
        //public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Birthdate,City,State")] Person person)
        //{
        //    // if (ModelState.IsValid)
        //    //{
        //    _context.Add(person);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //    // }
        //    // return View(person);
        //}
       
    }
}