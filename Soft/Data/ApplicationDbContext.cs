using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Abc.Infra.Quantity;

namespace Soft.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }       

        protected override void OnModelCreating(ModelBuilder builder) //loob tabelid
        {
            base.OnModelCreating(builder);
            QuantityDbContext.InitializeTables(builder);
        }
    }
}
