using Domain.Models;
using Domain.Dto;

namespace Application.Interfaces;

public interface IPizzaService
   {
      Task<IEnumerable<Pizza>> GetAll();
      Task<PizzaDto> GetById(int id);
      Task<Pizza> Create(Pizza newPizza);
      void AddTopping(int PizzaId, int ToppingId);
      void UpdateSauce(int PizzaId, int SauceId);
      void DeleteById(int id);
   }