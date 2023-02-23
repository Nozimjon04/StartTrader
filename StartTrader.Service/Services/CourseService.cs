using StartTrader.Data.IRepositories;
using StartTrader.Data.Repositories;
using StartTrader.Domain.Entities;
using StartTrader.Service.DTOs;
using StartTrader.Service.Helpers;
using StartTrader.Service.Interfaces;

namespace StartTrader.Service.Services;
public class CourseService : ICourseService
{
    private readonly IGenericRepository<Course> courseRepository;
    //private readonly IPaymentService paymentService ;
    public CourseService()
    {
        courseRepository= new GenericRepository<Course>();
        //paymentService = new PaymentService();
    }
    public async Task<GenericResponce<Course>> CreateAsync(CourseCreationDTO courseCreation)
    {
         var models = await courseRepository.GetAllAsync();
            if (models != null)
            {
                var model = models.FirstOrDefault(m => m.UserId == courseCreation.UserId);
                if (model != null)
                {
                    return new GenericResponce<Course>
                    {
                        StatusCode = 405,
                        Message = "You have already joined this course",
                        Result = model
                    };
                }
            }

            var mappedCourse = new Course
            {
                CheckPayment = courseCreation.CheckPayment,
                Cost = courseCreation.Cost,
                Description = courseCreation.Description,
                UserId = courseCreation.UserId,
                Type = courseCreation.Type
            };

            var res = await courseRepository?.CreateAsync(mappedCourse);


            return new GenericResponce<Course>
            {
                StatusCode = 200,
                Message = "Success",
                Result = res
            };
        
    }

    public async Task<GenericResponce<bool>> DelateAsync(long id)
    {
           var model= await courseRepository.DeleteAsync(id);
       
        if(model)
        {
            return new GenericResponce<bool>()
            {
                StatusCode=200,
                Message="Success",
                Result=true
            };
        }
        return new GenericResponce<bool>()
        {
            StatusCode = 404,
            Message = "No data",
            Result = false
        };
   
    }

    public async Task<GenericResponce<List<Course>>> GetAllAsync()
    {
        var models= await courseRepository.GetAllAsync();
        return new GenericResponce<List<Course>>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = models
        };
        
    }

    public async Task<GenericResponce<Course>> GetByIdAsync(long id)
    {
        var model= await courseRepository.GetByIdAsync(id);
        if (model is not null)
        {
            return new GenericResponce<Course>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = model
            };
        }
        return new GenericResponce<Course>()
        {
            StatusCode = 404,
            Message = "NO DATA",
            Result = null
        };

    }

    public async Task<GenericResponce<Course>> UpdateAsync(CourseCreationDTO courseCreation)
    {
        var models = await courseRepository.GetAllAsync();
        var model=models.FirstOrDefault(m=>m.UserId==courseCreation.UserId);
        if (model is not null)
        {
            var mappedCourse = new Course()
            {
                CheckPayment = courseCreation.CheckPayment,
                Description = courseCreation.Description,
                UserId = courseCreation.UserId,
                Type = courseCreation.Type,
                Cost = courseCreation.Cost,
                UpdateAt = DateTime.UtcNow,
                ID=model.ID
            };
            
            await courseRepository.UpdateAsync(mappedCourse);
            return new GenericResponce<Course>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = model
            };
        }
        return new GenericResponce<Course>()
        {
            StatusCode = 404,
            Message = "NO Any DATA",
            Result = null
        };
     
    }
}
    
  