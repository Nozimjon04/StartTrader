using StartTrader.Domain.Commons;
using StartTrader.Domain.Enums;

namespace StartTrader.Domain.Entities;

public class User:Auditable
{
   
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public UserRole role { get; set; }

}
