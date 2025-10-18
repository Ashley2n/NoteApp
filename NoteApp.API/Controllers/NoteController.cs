using Microsoft.AspNetCore.Mvc;
using NoteApp.Logic.Domain.Entity.Note.Models;
using NoteApp.Logic.Domain.Entity.Note.Repository.Interface;
using NoteApp.Logic.Domain.Entity.Note.Service.Interface;

namespace NoteApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoteController : Controller
{
    private readonly INoteService _note;
    public NoteController(INoteService note) => _note = note;

    public record CreateNoteDTO(string Title, string Content);
    public  record UpdateNoteDTO(int Id, string Title, string Content, DateTime Created);
    
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        return Ok(await _note.GetAllAsync(ct));
    }

    [HttpPost]
    public async Task<IActionResult> AddNote([FromBody] CreateNoteDTO noteDto, CancellationToken ct = default)
    {
        var created = await _note.CreateNoteAsync(noteDto.Title, noteDto.Content, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken ct)
    {
        var entity = await _note.GetByIdAsync(id, ct);
        return entity is null ? NotFound() : Ok(entity);
    }
    
    [HttpPost("{id:int}")]
    public async Task<IActionResult> UpdateNote(UpdateNoteDTO newNote, [FromRoute]int id, CancellationToken ct)
    {
        var oldEntity = await _note.GetByIdAsync(id,ct);

        if (oldEntity is null)
        {
            return NotFound();
        }

        var regularEntity = new NoteEntity
        {
            Id = id,
            Title = newNote.Title,
            Content = newNote.Content,
            Created = newNote.Created,
        };

        var updatedNote = await _note.UpdateNote(regularEntity, ct);
        
        return Ok(updatedNote);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteById([FromRoute]int id, CancellationToken ct)
    {
        var entity = await _note.GetByIdAsync(id, ct);
        if (entity is null)
        {
            return NotFound();
        }
        
        await _note.DeleteNote(id, ct);
        
        var checkedEntity = await _note.GetByIdAsync(id, ct);
        return checkedEntity != null ? throw new ArgumentException(message:"The Note was not deleted"): Ok();
    }
}