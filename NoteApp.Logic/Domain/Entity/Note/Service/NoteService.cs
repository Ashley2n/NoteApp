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

    public async Task<NoteEntity> CreateNote(string title, string content, CancellationToken ct = default)
    {
        var entity = new NoteEntity
        {
            Title = title,
            Content = content,
            Created = DateTime.Now,
            Modified = DateTime.Now
        };
        
        await _note.AddNote(entity, ct);
        return entity;
    }

    public Task<NoteEntity?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return _note.GetNoteById(id, ct);
    }

    // public async void DeleteNote(int id, CancellationToken ct = default)
    // {
    //     var data = _note.GetNoteById(id, ct);
    //
    //     var entity = CreateANoteEntity(data);
    //     await _note.DeleteNote(entity);
    // }

    public void UpdateNote(NoteEntity note, CancellationToken ct = default)
    {
        throw new NotImplementedException();
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