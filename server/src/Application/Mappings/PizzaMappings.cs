using Domain.Dto;
using Domain.Models;

namespace Application.Mappings;

public static class PizzaMappings
{
   public static PizzaDto PizzaDtoMapping(this Pizza model) => 
   new (
      Id: model.Id,
      Name: model.Name,
      Sauce: model.Sauce.Name,
      Toppings: model.Toppings.Select(t => new ToppingDto(Id: t.Id, Name: t.Name, Calories: t.Calories)).ToList()
   );


   public static void UpdatePizzaModelMapping(this PizzaSauceDto dto,
      Pizza modelPizza, Sauce modelSauce)
      {
         modelPizza.Id = dto.Id;
         modelPizza.Name = dto.Name;
         modelPizza.Sauce = modelSauce;
      }

   public static Pizza CreatePizzaModelMapping(this CreatePizzaDto dto, Sauce model)
      => new Pizza()
      {
         Id = dto.Id,
         Name = dto.Name,
         Sauce = model
      };
}