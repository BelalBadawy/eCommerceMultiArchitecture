namespace eStoreCA.Shared.Common;

public class MyAppResponse<T>
{
    public MyAppResponse()
    {
    }

    public MyAppResponse(T data, string message = null, string redirectTo = null)
    {
        Succeeded = true;
        Message = message;
        Data = data;
        RedirectTo = redirectTo;
    }

    public MyAppResponse(string message, string redirectTo = null)
    {
        Succeeded = false;
        Message = message;
        RedirectTo = redirectTo;
    }

    public MyAppResponse(List<string> errors, string redirectTo = null)
    {
        Succeeded = false;
        Errors = errors;
        RedirectTo = redirectTo;
    }

    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }
    public T Data { get; set; }
    public string RedirectTo { get; set; }
}