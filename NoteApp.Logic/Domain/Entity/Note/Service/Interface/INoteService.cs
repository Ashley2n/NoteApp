using NoteApp.Logic.Domain.Entity.Note.Models;

namespace NoteApp.Logic.Domain.Entity.Note.Service.Interface;

public interface INoteService
{
    Task<List<NoteEntity>> GetAllAsync(CancellationToken ct = default);
    Task<NoteEntity> CreateNote(string title, string content, CancellationToken ct = default);
    Task<NoteEntity?> GetByIdAsync(int id, CancellationToken ct = default);
    // void DeleteNote(int id, CancellationToken ct = default);
    // void UpdateNote(NoteEntity note, CancellationToken ct = default);
}