namespace Hedgehog.DTOs.Common;

public class BaseResponse
{
    public bool Ok => Error == null;
    
    public string? Error { get; init; }
}