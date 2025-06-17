namespace CryptoAPI.Models
{
    public class CryptoTransaction
    {
        public int Id { get; set; }
        public string CryptoCode { get; set; } // "usdc", "btc", etc.
        public string Action { get; set; } // "purchase" o "sale"
        public decimal CryptoAmount { get; set; }
        public decimal Money { get; set; } // Total en ARS
        public DateTime DateTime { get; set; }
    }
}
