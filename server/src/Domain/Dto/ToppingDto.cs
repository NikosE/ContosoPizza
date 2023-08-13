namespace Domain.Dto;

public record ToppingDto(
   int Id, 
   string Name, 
   decimal Calories
);

public record UpdateToppingDto(
   string Name,
   decimal Calories
);

public record CreateToppingDto(
   int Id,
   string Name, 
   decimal Calories
);