using NoteApp.Logic.Domain.Entity.Note.Models;

namespace NoteApp.Logic.Domain.Entity.Note.Repository.Interface;

public interface INoteEntityRepository
{
    Task<List<NoteEntity>> GetAllNotes(CancellationToken ct = default);
    
    Task<NoteEntity?> GetNoteById(int noteId, CancellationToken ct = default);
    
    Task AddAsync(NoteEntity note, CancellationToken ct = default);
    
    Task DeleteAsync(NoteEntity note);
    
    Task UpdateAsync(NoteEntity note);
    
    // void GetANote( int id,  CancellationToken ct = default);
    
}