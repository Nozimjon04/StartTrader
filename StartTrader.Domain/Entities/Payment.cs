using StartTrader.Domain.Commons;
using StartTrader.Domain.Enums;

namespace StartTrader.Domain.Entities;
public class Payment:Auditable
{
    public decimal Cost { get; set; }
    public long UserID { get; set; }
    public long CourseID { get; set; }
    public bool IsPaid { get; set; }
    public PaymentType type { get; set; }


}
