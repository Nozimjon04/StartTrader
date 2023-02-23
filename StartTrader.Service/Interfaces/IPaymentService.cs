using StartTrader.Data.Repositories;
using StartTrader.Domain.Entities;
using StartTrader.Service.DTOs;
using StartTrader.Service.Helpers;

namespace StartTrader.Service.Interfaces;
public interface IPaymentService
{
    public Task<GenericResponce<Payment>> CreateAsync(PaymentCreationDTO paymentCreation);
    public Task<GenericResponce<bool>> DelateAsync(long id);
    public Task<GenericResponce<Payment>> GetByIdAsync(long id);
    public Task<GenericResponce<List<Payment>>> GetAllAsync();
    public Task<GenericResponce<Payment>> UpdateAsync(PaymentCreationDTO paymentCreation);
}
