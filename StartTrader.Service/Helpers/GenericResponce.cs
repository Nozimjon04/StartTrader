namespace StartTrader.Service.Helpers;

public class GenericResponce<TResult>
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public TResult Result { get; set; }
}
