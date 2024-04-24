namespace IdentityAuth.DTOs
{
    public class AuthDTO
    {
        public string? Token { get; set; }
        public string? Message { get; set; }
        public string StatusCode { get; set; }
        public bool isSuccess { get; set; }
    }
}
