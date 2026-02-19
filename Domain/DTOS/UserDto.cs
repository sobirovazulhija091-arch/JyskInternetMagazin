using Microsoft.AspNetCore.Identity;

public class UserDto
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
