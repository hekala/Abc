using Abc.Data.Quantity;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Quantity
{
    public class QuantityDbContext: DbContext
    {
        public QuantityDbContext(DbContextOptions<QuantityDbContext> options)
            : base(options)
        {
        }
        public DbSet<MeasureData> Measures { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) //loob tabelid
        {
            base.OnModelCreating(builder);
            InitializeTables(builder);            
        }

        public static void InitializeTables(ModelBuilder builder)
        {
            builder.Entity<MeasureData>().ToTable(nameof(Measures));
        }
    }
}
