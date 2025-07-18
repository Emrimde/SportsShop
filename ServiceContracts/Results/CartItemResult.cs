namespace ServiceContracts.Results;
    public class CartItemResult
    {
        public bool Success { get; set; }
        public bool EnoughProduct { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Message { get; set; }
        public static CartItemResult Ok(string? message) => new() { Success = true, Message = message, EnoughProduct = true };
        public static CartItemResult Fail(string message) => new() { Success = false, ErrorMessage = message};
        public static CartItemResult NotEnoughProductQuantity(string message) => new() { Success = false, EnoughProduct = false, ErrorMessage = message };
}

