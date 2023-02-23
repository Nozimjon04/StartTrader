namespace StartTrader.Domain.Commons;

public class Auditable
{
     public long ID { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? UpdateAt { get; set; }

}
