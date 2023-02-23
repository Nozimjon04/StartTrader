using StartTrader.Domain.Enums;

namespace StartTrader.Service.DTOs;
public class CourseCreationDTO
{
    public bool CheckPayment { set; get; }
    public string Description { get; set; }
    public long UserId { get; set; }
    public CourseType Type { get; set; }
    public decimal Cost { get; set; }
}
