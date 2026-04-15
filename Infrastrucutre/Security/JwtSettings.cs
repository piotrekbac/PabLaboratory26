using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security;

public class JwtSettings(IConfiguration configuration)
{
    private static readonly string Section = "Jwt";
    public string Issuer => configuration.GetSection(Section)["Issuer"] ?? throw new InvalidOperationException("Issuer is not set.");
    public string Audience => configuration.GetSection(Section)["Audience"] ?? throw new InvalidOperationException("Audience is not set.");
    public string Secret => configuration.GetSection(Section)["SecretKey"] ?? throw new InvalidOperationException("Secret key is not set.");
    public int ExpirationInMinutes => configuration.GetSection(Section).GetSection("ExpiryInMinutes").Get<int>();
    public int RefreshTokenDays => configuration.GetSection(Section).GetSection("RefreshTokenDays").Get<int>();

    public SymmetricSecurityKey GetSymmetricKey() =>
        new(Encoding.UTF8.GetBytes(Secret));
}