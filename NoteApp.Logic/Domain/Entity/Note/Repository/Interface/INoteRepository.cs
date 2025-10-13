namespace NoteApp.Domain.Entity.Note.Repository.Interface;

public interface INoteEntityRepository
{
    IEnumerable<NoteEntity> GetAllNotes();
    
    NoteEntity GetNoteByID(int noteID);
    
    void InsertNote(NoteEntity note);
    
    void DeleteNote(int noteID);
    
    void UpdateNote(NoteEntity note);
    
    void Save();
}