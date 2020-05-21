namespace Engenharia.Domain.DTOs
{
    public class LoginResponseDto
    {
        public string token { get; set; }
        public UserDto user { get; set; }
    }
}
