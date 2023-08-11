using Microsoft.EntityFrameworkCore;
using WebApi;

namespace Persistence.Context;

public partial class PromotionsContext : DbContext
{
   public PromotionsContext()
   {
   }

   public PromotionsContext(DbContextOptions<PromotionsContext> options)
      : base(options)
   {
   }

   public virtual DbSet<Coupon> Coupons { get; set; } = null!;



   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      OnModelCreatingPartial(modelBuilder);
   }

   partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}