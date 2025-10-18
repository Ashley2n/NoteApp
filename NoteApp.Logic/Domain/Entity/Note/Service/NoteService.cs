using NoteApp.Logic.Domain.Entity.Note.Models;
using NoteApp.Logic.Domain.Entity.Note.Repository.Interface;
using NoteApp.Logic.Domain.Entity.Note.Service.Interface;

namespace NoteApp.Logic.Domain.Entity.Note.Service;

public class NoteService : INoteService
{

    private readonly INoteEntityRepository _note;

    public NoteService(INoteEntityRepository note)
    {
        _note = note;
    }
    public Task<List<NoteEntity>> GetAllAsync(CancellationToken ct = default)
    {
        return _note.GetAllNotes(ct);
    }

    public async Task<NoteEntity> CreateNoteAsync(string title, string content, CancellationToken ct = default)
    {
        var entity = new NoteEntity
        {
            Title = title,
            Content = content,
        };
        
        await _note.AddAsync(entity, ct);
        return entity;
    }

    public async Task<NoteEntity?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _note.GetNoteById(id, ct);
    }

    public async Task DeleteNote(int id, CancellationToken ct = default)
    {
        var data = await _note.GetNoteById(id, ct);

        if(data != null){ 
           await _note.DeleteAsync(data);
        }

        if ( await _note.GetNoteById(id, ct) != null)
        {
            throw new Exception("Note Not deleted");
        }
        
    }

    public async Task<NoteEntity> UpdateNote(NoteEntity newNote, CancellationToken ct = default)
    {
        var data = await  _note.GetNoteById(newNote.Id, ct);
        
        if (data is null)
        {
            throw new Exception("Note not found");
        }
        
        await _note.UpdateAsync(newNote);
        
        return newNote;
    }
    
    private record NoteDto(int Id, string Title, string Content, DateTime Created, DateTime Modified);

    private NoteEntity CreateANoteEntity(NoteDto noteDto) => new NoteEntity
    {
        Id = noteDto.Id,
        Title = noteDto.Title,
        Content = noteDto.Content,
        Created = noteDto.Created,
        Modified = noteDto.Modified,
    };
}