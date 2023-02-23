using StartTrader.Data.IRepositories;
using StartTrader.Data.Repositories;
using StartTrader.Domain.Entities;
using StartTrader.Domain.Enums;
using StartTrader.Service.DTOs;
using StartTrader.Service.Helpers;
using StartTrader.Service.Interfaces;
namespace StartTrader.Service.Services;
public class UserService : IUserService
{
    private readonly IGenericRepository<User> userRepository;
    public UserService()
    {
        userRepository = new GenericRepository<User>();
    }

    public async Task<GenericResponce<User>> CreateAsync(UserCreationDTO userCreation)
    {

        var models = await userRepository.GetAllAsync();
        
        if (models is not null)
        {
            var model = models.FirstOrDefault(m => m.Email == userCreation.Email);
            if (model is not null)
            {
                return new GenericResponce<User>()
                {
                    StatusCode = 405,
                    Message = "This User is exist",
                    Result = null
                };
            }
        }
        
        var newUser = new User
        {
            
            FirstName =userCreation.FirstName,
            LastName = userCreation.LastName,
            Password = userCreation.Password,
            Address=userCreation.Address,
            Email=userCreation.Email,
            Phone = userCreation.Phone,
            CreateAt=DateTime.UtcNow,
            role = UserRole.User
        };
       
        var result = await userRepository.CreateAsync(newUser);
        return new GenericResponce<User>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = result
        };

    }

    public async Task<GenericResponce<bool>> DeleteAsync(long id)
    {
      
         var model=await userRepository.DeleteAsync(id);

        if (model)
        {
            return new GenericResponce<bool>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = true
            };
        }
        return new GenericResponce<bool>()
        {
            StatusCode = 404,
            Message = "User is not found",
            Result = false

        };
    }

    public async Task<GenericResponce<List<User>>> GetAllAsync()
    {
        var models = await userRepository.GetAllAsync();
        return new GenericResponce<List<User>>()
        {
            StatusCode=200,
            Message="Success",
            Result=models
        };
    }


    public async Task<GenericResponce<User>> GetByIdAsync(long id)
    {
        var model = await userRepository.GetByIdAsync(id);
       
        if (model is not null)
        {
      
            return new GenericResponce<User>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = model
            };
        }
        return new GenericResponce<User>()
        {
            StatusCode = 404,
            Message = "This user is not exist",
            Result = model
        };
    }

    public async Task<GenericResponce<User>> UpdateAsync(UserCreationDTO userCreation)
    {
        var models = await userRepository.GetAllAsync();
        var mappedModel1 = models.FirstOrDefault(m => m.Email == userCreation.Email);
        if(mappedModel1 is not null)
        {
            var mappedModel = new User();
            mappedModel.FirstName = userCreation.FirstName;
            mappedModel.LastName = userCreation.LastName;
            mappedModel.Address = userCreation.LastName;
            mappedModel.Phone = userCreation.Phone;
            mappedModel.Email = userCreation.Email;
            mappedModel.Password = userCreation.Password;
            mappedModel.role = userCreation.role;
            mappedModel.ID = mappedModel1.ID;
            mappedModel.UpdateAt = DateTime.UtcNow;
            await userRepository.UpdateAsync(mappedModel);
            return new GenericResponce<User>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = mappedModel
            };
        }
        return new GenericResponce<User>()
        {
            StatusCode = 404,
            Message = "This user is not exist",
            Result = null
        };

    }
}
