using Microsoft.EntityFrameworkCore;
using CryptoAPI.Models;

namespace CryptoAPI.Data
{
    public class CryptoContext : DbContext
    {
        public CryptoContext(DbContextOptions<CryptoContext> options) : base(options) { }

        public DbSet<CryptoTransaction> Transactions { get; set; }
    }
}
