using System.ComponentModel;
using System.Data.Common;
using Domain.Dto;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Application.Mappings;

public static class SauceMappings
{
   public static SauceDto SauceDtoMapping(this Sauce model)
      => new (
         Id: model.Id,
         Name: model.Name,
         IsVegan: model.IsVegan
      );

   public static void UpdateSauceDtoMapping(this UpdateSauceDto dto, Sauce model)
   {
      model.Name = dto.Name;
      model.IsVegan = dto.IsVegan;
   }

   public static Sauce CreateSauceDtoMapping(this CreateSauceDto dto)
      => new Sauce()
      {
         Id = dto.Id,
         Name = dto.Name,
         IsVegan = dto.IsVegan
      };
}