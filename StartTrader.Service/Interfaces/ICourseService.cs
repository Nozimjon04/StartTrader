using StartTrader.Domain.Entities;
using StartTrader.Service.DTOs;
using StartTrader.Service.Helpers;

namespace StartTrader.Service.Interfaces;

public interface ICourseService
{
    public Task<GenericResponce<Course>> CreateAsync(CourseCreationDTO courseCreation);
    public Task<GenericResponce<Course>> UpdateAsync(CourseCreationDTO courseCreation);
    public Task<GenericResponce<bool>> DelateAsync(long id);
    public Task<GenericResponce<Course>> GetByIdAsync(long id);
    public Task<GenericResponce<List<Course>>> GetAllAsync();

}
