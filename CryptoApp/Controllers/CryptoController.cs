using CryptoApp.Data;
using CryptoApp.Filters;
using CryptoApp.Models;
using CryptoApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CryptoApp.Controllers
{

    [HandleException]
    [ValidateModel]
    public class CryptoController : Controller
    {
        public readonly CryptoTransactionService _cryptoTransactionService;
        public readonly GetCryptoInfoService _getCryptoInfoService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public CryptoController(ApplicationDbContext context, GetCryptoInfoService getCryptoInfoService, UserManager<ApplicationUser> userManager, CryptoTransactionService cryptoTransactionService)
        {
            _cryptoTransactionService = cryptoTransactionService;
            _getCryptoInfoService = getCryptoInfoService;
            _userManager = userManager;
            _context = context;
        }

       
        public IActionResult Index(string CryptoSymbol)
        {
            if (CryptoSymbol == null)
            {
                CryptoSymbol = "LTC";
            }

            CurrencyModel cryptoObject = _getCryptoInfoService.GetCryptoObject(CryptoSymbol).Result;

            return View(cryptoObject);
        }

        [Authorize]
        public async System.Threading.Tasks.Task<IActionResult> Wallet()
        {
            var user = await _userManager.GetUserAsync(User);
          
            var transactionModel = new TransactionModel
            {
                USD = user.USD, //the amount of cash available to spend
                BTC = user.BTC, //Number of crypto owned
                ETH = user.ETH, //Number of crypto owned
                LTC = user.LTC, //Number of crypto owned
                DOGE = user.DOGE//Number of crypto owned
                
            };

            WalletModel wallet = _cryptoTransactionService.GetWalletContents(transactionModel);

            return View(wallet);
        }

        [Authorize]
        public async Task<IActionResult> SellCrypto(string cryptoSymbol, decimal amountOnHand)
        {
            var user = await _userManager.GetUserAsync(User);
          
            if (cryptoSymbol == null)
            {
                cryptoSymbol = "LTC";
            }

            var transaction = new TransactionModel
            {
                USD = user.USD, //the amount of cash available to spend
                BTC = user.BTC, //Number of crypto owned
                ETH = user.ETH, //Number of crypto owned
                LTC = user.LTC, //Number of crypto owned
                DOGE = user.DOGE,//Number of crypto owned
                CryptoSymbol = cryptoSymbol,
            };

            SellViewModel sell = new SellViewModel
            {
                CryptoSymbol = transaction.CryptoSymbol,
                CryptoPrice = _getCryptoInfoService.GetCryptoPrice(transaction.CryptoSymbol),
                AmountToSell = transaction.AmountToBuyOrSell,
                AmountOnHand = transaction.AmountOnHand,
                USD = transaction.USD
            };

            return View(sell);
        }

  [Authorize]
        public async Task<IActionResult> BuyCrypto(string cryptoSymbol, decimal amountOnHand)
        {
            var user = await _userManager.GetUserAsync(User);
            
            var transaction = new TransactionModel
            {
                USD = user.USD, //the amount of cash available to spend
                BTC = user.BTC, //Number of crypto owned
                ETH = user.ETH, //Number of crypto owned
                LTC = user.LTC, //Number of crypto owned
                DOGE = user.DOGE,//Number of crypto owned
                CryptoSymbol = cryptoSymbol,
                // AmountToBuyOrSell = amountToSell
            };

            SellViewModel sell = new SellViewModel
            {
                CryptoSymbol = transaction.CryptoSymbol,
                CryptoPrice = _getCryptoInfoService.GetCryptoPrice(transaction.CryptoSymbol),
                AmountToSell = transaction.AmountToBuyOrSell,
                AmountOnHand = transaction.AmountOnHand,
                USD = transaction.USD
            };

            return View(sell);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExecuteSale([Bind("USD,CryptoPrice,AmountToSell,CryptoSymbol")] SellViewModel inSell)
        {
            var user = await _userManager.GetUserAsync(User);

            var transaction = new TransactionModel
            {
                USD = user.USD, //the amount of cash available to spend
                BTC = user.BTC, //Number of crypto owned
                ETH = user.ETH, //Number of crypto owned
                LTC = user.LTC, //Number of crypto owned
                DOGE = user.DOGE,//Number of crypto owned
                CryptoSymbol = inSell.CryptoSymbol,
                AmountToBuyOrSell = inSell.AmountToSell
            };

            if (_cryptoTransactionService.CanSellCrypto(transaction))
            {
                transaction = _cryptoTransactionService.SellCryptoPerPiece(transaction);

                user.USD = transaction.USD;
                user.BTC = transaction.BTC;
                user.ETH = transaction.ETH;
                user.LTC = transaction.LTC;
                user.DOGE = transaction.DOGE;
                user.TotalAccountValue = transaction.TotalAccountValue;
                _context.SaveChanges();

                return RedirectToAction("Wallet", "Crypto");
            }
            else
            {
                return RedirectToAction("Wallet", "Crypto");
            }
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExecuteBuy([Bind("USD,CryptoPrice,AmountToSell,CryptoSymbol")] SellViewModel inSell)
        {
            var user = await _userManager.GetUserAsync(User);

            var transaction = new TransactionModel
            {
                USD = user.USD, //the amount of cash available to spend
                BTC = user.BTC, //Number of crypto owned
                ETH = user.ETH, //Number of crypto owned
                LTC = user.LTC, //Number of crypto owned
                DOGE = user.DOGE,//Number of crypto owned
                CryptoSymbol = inSell.CryptoSymbol,
                AmountToBuyOrSell = inSell.AmountToSell
            };

            if (_cryptoTransactionService.CanBuyCrypto(transaction))
            {
                transaction = _cryptoTransactionService.BuyCryptoPerPiece(transaction);

                user.USD = transaction.USD;
                user.BTC = transaction.BTC;
                user.ETH = transaction.ETH;
                user.LTC = transaction.LTC;
                user.DOGE = transaction.DOGE;
                user.TotalAccountValue = transaction.TotalAccountValue;
                _context.SaveChanges();

                return RedirectToAction("Wallet", "Crypto");
            }
            else
            {
                return RedirectToAction("Wallet", "Crypto");
            }
        }
    }
}