namespace Domain.Dto;

public record PizzaDto(
      int Id,
      string Name,
      string Sauce,
      List<PizzaToppingDto> Toppings
   );

   public record PizzaToppingDto(
      int Id, 
      string Name, 
      decimal Calories
   );