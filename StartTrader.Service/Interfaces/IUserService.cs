using StartTrader.Domain.Entities;
using StartTrader.Service.DTOs;
using StartTrader.Service.Helpers;

namespace StartTrader.Service.Interfaces;
 public interface IUserService
{
    public Task<GenericResponce<User>> CreateAsync(UserCreationDTO userCreation);
    public Task<GenericResponce<User>> UpdateAsync(UserCreationDTO userCreation);
    public Task<GenericResponce<List<User>>> GetAllAsync();
    public Task<GenericResponce<User>> GetByIdAsync(long id);
    public Task<GenericResponce<bool>> DeleteAsync(long id);
}
