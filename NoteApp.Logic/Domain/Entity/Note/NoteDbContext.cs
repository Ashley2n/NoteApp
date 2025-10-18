using Microsoft.EntityFrameworkCore;
using NoteApp.Logic.Domain.Entity.Note.Models;

namespace NoteApp.Logic.Domain.Entity.Note;

public class NoteDbContext : DbContext
{
    public NoteDbContext(DbContextOptions<NoteDbContext> options) : base(options) {}
    
    public DbSet<NoteEntity> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<NoteEntity>(n =>
        {
            n.HasKey(k => k.Id);
            n.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            n.Property(k => k.Title)
                .IsRequired();
        });

    }
}