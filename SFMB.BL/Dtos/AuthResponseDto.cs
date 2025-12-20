namespace SFMB.BL.Dtos
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// Override ToString to prevent sensitive data from being exposed in logs or error messages
        /// </summary>
        public override string ToString()
        {
            return "AuthResponse [sensitive data hidden]";
        }
    }
}
