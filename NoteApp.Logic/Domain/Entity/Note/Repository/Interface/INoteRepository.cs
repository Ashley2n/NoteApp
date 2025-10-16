using NoteApp.Logic.Domain.Entity.Note.Models;

namespace NoteApp.Logic.Domain.Entity.Note.Repository.Interface;

public interface INoteEntityRepository
{
    Task<List<NoteEntity>> GetAllNotes(CancellationToken ct = default);
    
    Task<NoteEntity?> GetNoteById(int noteId, CancellationToken ct = default);
    
    Task AddNote(NoteEntity note, CancellationToken ct = default);
    
    void DeleteNote(NoteEntity note);
    
    void UpdateNote(NoteEntity note);
    
    // void GetANote( int id,  CancellationToken ct = default);
    
}