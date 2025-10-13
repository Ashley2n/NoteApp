namespace NoteApp.Domain.Enities.Models;

public class NoteEntity
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }
    
    public DateTime Created { get; set; }
    
    public DateTime Modified { get; set; }
    
}