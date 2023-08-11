namespace WebApi;

public partial class Coupon
   {
      public long Id { get; set; }
      public string Description { get; set; } = null!;
      public string? Expiration { get; set; }
   }