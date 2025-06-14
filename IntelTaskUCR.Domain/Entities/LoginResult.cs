namespace IntelTaskUCR.Domain.Entities
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public EUsuarios? Usuario { get; set; }
    }
}
