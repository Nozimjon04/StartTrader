using StartTrader.Domain.Commons;

namespace StartTrader.Domain.Entities;

public class Communication:Auditable
{
    public long UserID { get; set; }
    public string Message { get; set; }
    public bool IsAdmin { get; set; }

}
