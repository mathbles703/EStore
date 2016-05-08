using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace eStore.Models
{

    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CarClass>().ForSqlServerToTable("CarClass")
                .Property(c => c.Id).UseSqlServerIdentityColumn();
            builder.Entity<Car>().ForSqlServerToTable("Car");
            builder.Entity<Cart>().ForSqlServerToTable("Carts")
                .Property(t => t.Id).UseSqlServerIdentityColumn();
            builder.Entity<CartItem>().ForSqlServerToTable("CartItems")
                .Property(ti => ti.Id).UseSqlServerIdentityColumn();
            builder.Entity<Store>().ForSqlServerToTable("Stores")
                .Property(s => s.Id).UseSqlServerIdentityColumn();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; AttachDbFilename = D:\\College\\Term 4\\INFO 3067\\$Workspace 3067\\eStoreDB\\estoreDb.mdf; Integrated Security = True; Connect Timeout = 10");
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarClass> CarClasses { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
    }
}
