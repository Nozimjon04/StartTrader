using StartTrader.Domain.Commons;
using StartTrader.Domain.Enums;

namespace StartTrader.Domain.Entities;

public class Course:Auditable
{
    
    public bool CheckPayment { set; get; }
    public string Description { get; set; }
    public long UserId { get; set;  }
    public CourseType Type { get; set; }
    public decimal Cost { get; set; }

}
