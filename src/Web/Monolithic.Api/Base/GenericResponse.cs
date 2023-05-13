namespace Monolithiv.Api.Base;
public class GenericResponse
{
    public GenericResponse() { }
    public GenericResponse(bool success, string message, int statusCode,object dyata = null)
    {
        Success = success;
        Message = message;
        StatusCode = statusCode;
        Data = Data;
    }
    public bool Success { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public object Data { get; set; }
}