using System.Globalization;
using Domain.Dto;
using Domain.Models;

namespace Application.Mappings;

public static class ToppingMappings
{
   public static ToppingDto ToppingDtoMapping(this Topping model)
      => new(
         Id: model.Id,
         Name: model.Name,
         Calories: model.Calories
      );

   public static void UpdateToppingDtoMapping(this UpdateToppingDto dto, Topping model)
   {
      model.Name = dto.Name;
      model.Calories = dto.Calories;
   }

   public static Topping CreateToppingDtoMapping(this CreateToppingDto dto)
      => new Topping()
      {
         Id = dto.Id,
         Name = dto.Name,
         Calories = dto.Calories
      };
}