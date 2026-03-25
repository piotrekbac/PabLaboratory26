namespace AppCore.DTOs;

// Piotr Bacior - WSEI Kraków

public record CreateNoteDto(string Content);
public record NoteDto(Guid Id, string Content, DateTime CreatedAt, string CreatedBy);