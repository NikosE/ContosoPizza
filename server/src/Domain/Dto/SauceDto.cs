namespace Domain.Dto;

public record SauceDto(
   int Id,
   string Name,
   bool IsVegan
);

public record UpdateSauceDto(
   string Name,
   bool IsVegan
);