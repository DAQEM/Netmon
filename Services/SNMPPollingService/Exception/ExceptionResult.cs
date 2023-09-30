namespace SNMPPollingService.Exception;

public class ExceptionResult
{
    public string Message { get; private set; }
    public int StatusCode { get; }
    public bool IsSuccess => StatusCode == 200;
    
    public ExceptionResult(string message, int code)
    {
        Message = message;
        StatusCode = code;
    }
    
    public ExceptionResult()
    {
        Message = string.Empty;
        StatusCode = 200;
    }
}