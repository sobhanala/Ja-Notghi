namespace API.DTOs.ResponseTemplate;

public class Response
{
    public ResponseStatus StatusCode { get; set; }
    public string Message { get; set; } = String.Empty;
    public object? Data { get; set; }
}