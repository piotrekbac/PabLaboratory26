using AppCore.Interfaces;

namespace AppCore.DTOs;

public record LoginDto(string Email, string Password);

public record AuthResponseDto
{
    public string AccessToken { get; init; } = string.Empty;
    public string RefreshToken { get; init; } = string.Empty;
    public DateTime ExpiresAt { get; init; }
    public UserDto User { get; init; } = null!;
}

public record RefreshTokenDto(string AccessToken, string RefreshToken);

// Potrzebny Ci też UserDto, jeśli go nie masz:
public record UserDto
{
    public string Id { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Department { get; init; } = string.Empty;
    public IEnumerable<string> Roles { get; init; } = new List<string>();
    public SystemUserStatus Status { get; init; }
}