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

    public async Task AddAsync(NoteEntity note, CancellationToken ct = default) {
        await _context.Notes.AddAsync(note, ct);
        await _context.SaveChangesAsync(ct);   
    }
        

    public async Task DeleteAsync(NoteEntity noteEntity)
    {
        var note = await _context.Notes.FindAsync(noteEntity.Id);
        if (note != null)
        {
           _context.Notes.Remove(note);
        }
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(NoteEntity newNote)
    {
        var existingNote = await _context.Notes.FindAsync( newNote.Id );
        // Will i need the Attach()? 
        if (existingNote == null)  throw new Exception("Note not found");
        
            // _context.Entry(note).State = EntityState.Modified;
            // _context.SaveChanges();
            
        existingNote.Title = newNote.Title;
        existingNote.Content = newNote.Content;
        existingNote.Modified = DateTime.Now;
        

        await _context.SaveChangesAsync();
    }

    // public NoteEntity? GetANote(int noteId, CancellationToken ct = default)
    // {
    //     return _context.Notes.FirstOrDefault(n => n.Id == noteId);
    // }
    
}