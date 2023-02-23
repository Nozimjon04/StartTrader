using StartTrader.Data.IRepositories;
using StartTrader.Data.Repositories;
using StartTrader.Domain.Entities;
using StartTrader.Domain.Enums;
using StartTrader.Service.DTOs;
using StartTrader.Service.Helpers;
using StartTrader.Service.Interfaces;

namespace StartTrader.Service.Services;
public class PaymentService : IPaymentService
{
    private readonly IGenericRepository<Payment> paymentRepository;
    public PaymentService()
    {
        paymentRepository=new GenericRepository<Payment>();
    }

    public async Task<GenericResponce<Payment>> CreateAsync(PaymentCreationDTO paymentCreation)
    {
       
        var models = await paymentRepository.GetAllAsync();

        if (models is not null)
        {
            var model = models.FirstOrDefault(m => m.UserID == paymentCreation.UserID);
            if (model is not null)
            {
                return new GenericResponce<Payment>()
                {
                    StatusCode = 405,
                    Message = "This User is exist",
                    Result = null
                };
            }
        }

        var newUser = new Payment
        {

            Cost = paymentCreation.Cost,
            UserID = paymentCreation.UserID,
            CourseID = paymentCreation.CourseID,
            IsPaid = paymentCreation.IsPaid,
            type = paymentCreation.type,
            CreateAt = DateTime.UtcNow,
           
        };

         await paymentRepository.CreateAsync(newUser);
        return new GenericResponce<Payment>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = newUser
        };
    }

  
    public async Task<GenericResponce<bool>> DelateAsync(long id)
    {
        var model = await paymentRepository.DeleteAsync(id);
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
            Message = "NO DATA",
            Result = false
        };
        
    }

    public async Task<GenericResponce<List<Payment>>> GetAllAsync()
    {
        var models=await paymentRepository.GetAllAsync();
        return new GenericResponce<List<Payment>>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = models
        };
    }

    public async Task<GenericResponce<Payment>> GetByIdAsync(long id)
    {
        var model=await paymentRepository.GetByIdAsync(id);
        if (model is not null)
        {
            return new GenericResponce<Payment>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = model
            };
        }
        return new GenericResponce<Payment>()
        {
            StatusCode = 404,
            Message = "NO ANY DATA",
            Result = null
        };
      
    }

    public async Task<GenericResponce<Payment>> UpdateAsync(PaymentCreationDTO paymentCreation)
    {
        var models = await paymentRepository.GetAllAsync();
        var model=models.FirstOrDefault(m=>m.UserID==paymentCreation.UserID);
        if (model is not null)
        {
            var mappedPayment = new Payment()
            {
                Cost = paymentCreation.Cost,
                UserID = paymentCreation.UserID,
                CourseID = paymentCreation.CourseID,
                IsPaid = paymentCreation.IsPaid,
                type = paymentCreation.type,
                UpdateAt=DateTime.UtcNow,
                ID=model.ID,
                
            };
            await paymentRepository.UpdateAsync(mappedPayment);
            return new GenericResponce<Payment>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = model
            };
        }
        return new GenericResponce<Payment>()
        {
            StatusCode = 404,
            Message = "NO ANY DATA",
            Result = null
        };    
    }
}
  