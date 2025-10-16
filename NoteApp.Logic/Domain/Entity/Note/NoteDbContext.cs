using Microsoft.EntityFrameworkCore;
using NoteApp.Logic.Domain.Entity.Note.Models;

namespace NoteApp.Logic.Domain.Entity.Note;

public class NoteDbContext : DbContext
{
    public NoteDbContext(DbContextOptions<NoteDbContext> options) : base(options) {}
    
    public DbSet<NoteEntity> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(NoteEntity).Assembly,
            n => n.Namespace != null &&
                 n.Namespace.StartsWith("NoteApp.Domain.Entity.Note")
            );

    }
}