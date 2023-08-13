using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace WebApi.Controllers;

public class CouponController : BaseController
{
   private readonly PromotionsContext _context;
   public CouponController(PromotionsContext context)
   {
      _context = context;   
   }

   [HttpGet]
   public async Task<IEnumerable<Coupon>> Get()
   {
      return await _context.Coupons
         .AsNoTracking()
         .ToListAsync();
   }
}