using AppCore.Models.Enums; // TO JEST KLUCZOWE

namespace AppCore.DTOs;

public record ContactSearchDto(
    string? Query,
    ContactStatus? Status,
    string? Tag,
    string? ContactType,
    int Page = 1,
    int PageSize = 20
);