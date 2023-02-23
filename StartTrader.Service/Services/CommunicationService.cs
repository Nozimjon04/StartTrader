using StartTrader.Data.IRepositories;
using StartTrader.Data.Repositories;
using StartTrader.Domain.Entities;
using StartTrader.Service.DTOs;
using StartTrader.Service.Helpers;
using StartTrader.Service.Interfaces;

namespace StartTrader.Service.Services;

public class CommunicationService : ICommunicationService
{
    private readonly IGenericRepository<Communication> communicationRepo;

    private readonly IUserService userService;
    public CommunicationService()
    {
      communicationRepo=new GenericRepository<Communication>();

      userService = new UserService();
    }
    public async Task<GenericResponce<Communication>> AnswerAsync(long id, string answer,bool isAdmin)
    {
        if(isAdmin)
        {
            var model = await communicationRepo.GetByIdAsync(id);

            if (model.Message!=" ")
            {
                var mappedCom = new Communication();
                model.Message= "|" + model.Message+ "|"+ "Replay:"+ answer;
                model.UserID = id;
                model.IsAdmin = isAdmin;
                model.ID= id;
                model.UpdateAt = DateTime.UtcNow;
                await communicationRepo.UpdateAsync(model);
                
                
                return new GenericResponce<Communication>()
                {
                    StatusCode= 200,
                    Message="Answered",
                    Result= mappedCom
                };
            }
            else
            {
                var mappedCom = new Communication();
                mappedCom.Message = "\n" + mappedCom.Message + "\n" + "Replay:" + answer;
                mappedCom.UserID = id;
                mappedCom.IsAdmin = isAdmin;
                return new GenericResponce<Communication>()
                {

                    StatusCode = 200,
                    Message = "No question",
                    Result = mappedCom
                };
            };
            
        }
        return new GenericResponce<Communication>()
        {
            StatusCode = 404,
            Message = "You are not Admin",
            Result = null
        };
       
    }

    public async Task<GenericResponce<Communication>> CreateAsync(CommunicationCreationDTO communicationCreation)
    {

        var model =  await userService.GetByIdAsync(communicationCreation.UserID);
         if (model is not null)
        {
            Communication com = new Communication()
            {
                UserID= communicationCreation.UserID,
                IsAdmin=communicationCreation.IsAdmin,
                Message=communicationCreation.Message,
            };

                var res= await communicationRepo.CreateAsync(com);
            return new GenericResponce<Communication>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = res
            };
        }
        return new GenericResponce<Communication>()
        {
            StatusCode = 404,
            Message = "You have to Register",
            Result = null

        };
    }

    public async Task<GenericResponce<bool>> DelateAsync(long id)
    {
        
       var model= await communicationRepo.DeleteAsync(id);

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
            Message = "Message is not found",
            Result = false

        };
        
    }

    public async Task<GenericResponce<List<Communication>>> GetAllAsync()
    {
        var models=await communicationRepo.GetAllAsync();
        return new GenericResponce<List<Communication>>()
        {
            StatusCode= 200,
            Message="Success",
            Result= models
        };
        
    }

    public async Task<GenericResponce<Communication>> GetByIdAsync(long id)
    {
        var model = await communicationRepo.GetByIdAsync(id);
        
        if (model is not null)
        {

            return new GenericResponce<Communication>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = model
            };
        }
        return new GenericResponce<Communication>()
        {
            StatusCode = 404,
            Message = "This user has not question",
            Result = model
        };
       
    }

    public async Task<GenericResponce<Communication>> UpdateAsync(CommunicationCreationDTO communicationCreation)
    {
        var model = await userService.GetByIdAsync(communicationCreation.UserID);
        if (model is not null)
        {
            Communication mappedCom = new Communication()
            {
                IsAdmin = communicationCreation.IsAdmin,
                Message = communicationCreation.Message,
                UserID = communicationCreation.UserID,
                UpdateAt = DateTime.UtcNow,
                ID=model.Result.ID
                
            };
            await communicationRepo.UpdateAsync(mappedCom);
            return new GenericResponce<Communication>()
            {
                StatusCode = 200,
                Message = "SUCCESS",
                Result = mappedCom
            };

        }
        return new GenericResponce<Communication>()
        {
            StatusCode = 404,
            Message = "NO DATA ",
            Result = null
        };
        
    }
}
