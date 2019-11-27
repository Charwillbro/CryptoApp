using Microsoft.AspNetCore.Identity;

namespace CryptoApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public decimal TotalAccountValue { get; set; }
        public decimal USD{ get; set; }
        public decimal BTC { get; set; }
        public decimal ETH { get; set; }
        public decimal LTC { get; set; }
        public decimal DOGE { get; set; }

    }
}
