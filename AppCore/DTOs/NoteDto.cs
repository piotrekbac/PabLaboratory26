namespace AppCore.DTOs;

// Piotr Bacior - WSEI Kraków

// DTO używane przy tworzeniu notatki
public record CreateNoteDto(string Content);

// DTO reprezentujący notatkę zwracaną na zewnątrz
public record NoteDto(Guid Id, string Content, DateTime CreatedAt, string CreatedBy);