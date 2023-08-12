using Domain.Models;

namespace Domain.Dto;

public record PizzaDto(
      int Id,
      string Name,
      string Sauce,
      List<ToppingDto> Toppings
   );

public record CreatePizzaDto(
   int Id,
   string Name,
   Sauce Sauce
);

public record PizzaSauceDto(
   int Id,
   string Name,
   Sauce Sauce
);