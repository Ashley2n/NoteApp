namespace NoteApp.Logic.Domain.Entity.Note.Models;

public class NoteEntity
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }
    
    public DateTime Created { get; set; } = DateTime.Now;
    
    public DateTime Modified { get; set; } =  DateTime.Now;
    
}