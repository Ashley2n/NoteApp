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
    
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        return Ok(await _note.GetAllAsync(ct));
    }

    [HttpPost]
    public async Task<IActionResult> AddNote([FromBody] CreateNoteDTO noteDto, CancellationToken ct = default)
    {
        var created = await _note.CreateNote(noteDto.Title, noteDto.Content, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var entity = await _note.GetByIdAsync(id, ct);
        return entity is null ? NotFound() : Ok(entity);
    }
}