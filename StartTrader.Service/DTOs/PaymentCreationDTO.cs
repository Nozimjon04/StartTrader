using StartTrader.Domain.Enums;

namespace StartTrader.Service.DTOs; 
public class PaymentCreationDTO 
{
    public decimal Cost { get; set; }
    public long UserID { get; set; }
    public long CourseID { get; set; }
    public bool IsPaid { get; set; }
    public PaymentType type { get; set; }
}
