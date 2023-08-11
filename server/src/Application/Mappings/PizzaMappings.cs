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
         Toppings: model.Toppings.Select(t => new PizzaToppingDto(Id: t.Id, Name: t.Name, Calories: t.Calories)).ToList()
      );
}