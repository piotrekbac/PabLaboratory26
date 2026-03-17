using AppCore.Models;

namespace AppCore.DTOs;

public record CompanyDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? NIP { get; init; }
    public string? Industry { get; init; }
    public string? Website { get; init; }
}

public record CreateCompanyDto(string Name, string? NIP, string? Industry, string? Website)
{
    public Company ToEntity() => new()
    {
        Id = Guid.NewGuid(),
        Name = this.Name,
        NIP = this.NIP,
        Industry = this.Industry,
        Website = this.Website
    };
}