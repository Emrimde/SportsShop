namespace SportsShop.Core.ServiceContracts.Results;
public class Result
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public string? Message { get; set; }
    public static Result Ok() => new() {Success = true};
    public static Result Fail(string message) => new() { Success = false, ErrorMessage = message };
}
    

