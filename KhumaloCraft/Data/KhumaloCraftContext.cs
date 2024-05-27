using Microsoft.EntityFrameworkCore;
using KhumaloCraft.Models;
using System.ComponentModel.DataAnnotations;

namespace KhumaloCraft.Data
{
    public class KhumaloCraftContext : DbContext
    {
        public KhumaloCraftContext(DbContextOptions<KhumaloCraftContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Transactions> Transactions { get; set; }

    }
}
