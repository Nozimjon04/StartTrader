using StartTrader.Domain.Entities;
using StartTrader.Service.DTOs;
using StartTrader.Service.Helpers;

namespace StartTrader.Service.Interfaces;

public interface ICommunicationService
{
    Task<GenericResponce<Communication>> CreateAsync(CommunicationCreationDTO communicationCreation);
    Task<GenericResponce<bool>> DelateAsync(long id);
    Task<GenericResponce<Communication>> UpdateAsync(CommunicationCreationDTO communicationCreation);
    Task<GenericResponce<List<Communication>>> GetAllAsync();
    Task<GenericResponce<Communication>> AnswerAsync(long id, string answer, bool isAdmin);
    Task<GenericResponce<Communication>> GetByIdAsync(long id);


  
}
