using Microsoft.EntityFrameworkCore;
using NoteApp.Logic.Domain.Entity.Note.Models;
using NoteApp.Logic.Domain.Entity.Note.Repository.Interface;

namespace NoteApp.Logic.Domain.Entity.Note.Repository.Base;

public class NoteEntityRepository : INoteEntityRepository
{
    //Dependency Injection
    
    private readonly NoteDbContext _context;
    public NoteEntityRepository(NoteDbContext context)
    {
        this._context =  context;
    }
    public Task<List<NoteEntity>> GetAllNotes(CancellationToken ct)
    {
        return _context.Notes.ToListAsync(ct);
    }

    public Task<NoteEntity?> GetNoteById(int noteId, CancellationToken ct = default)
    {
        return  _context.Notes
            .FirstOrDefaultAsync(n => n.Id == noteId, ct);
    }

    public Task AddNote(NoteEntity note, CancellationToken ct = default)
    {
        return _context.AddAsync(note)
            .AsTask();
    }

    public void DeleteNote(NoteEntity noteEntity)
    {
        var  note = _context.Notes.Find(noteEntity);
        if (note != null)
        {
            _context.Notes.Remove(note);
        }
        _context.SaveChanges();
    }

    public void UpdateNote(NoteEntity note)
    {
        var noteEntity = _context.Notes.FirstOrDefault( n => n.Id == note.Id);
        // Will i need the Attach()? 
        if (noteEntity != null)
        {
            _context.Entry(note).State = EntityState.Modified;
            _context.SaveChanges();
        }
        
    }

    // public NoteEntity? GetANote(int noteId, CancellationToken ct = default)
    // {
    //     return _context.Notes.FirstOrDefault(n => n.Id == noteId);
    // }
    
}