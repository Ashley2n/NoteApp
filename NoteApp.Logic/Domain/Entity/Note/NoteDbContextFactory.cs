using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NoteApp.Logic.Domain.Entity.Note;
using NoteApp.Logic.Domain.Entity.Note.Models;

namespace NoteApp.Logic.Domain.Entity.Note;

public sealed class NoteDbContextFactory :IDesignTimeDbContextFactory<NoteDbContext>
{
    public NoteDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<NoteDbContext>();
        builder.UseSqlite("Data Source=note.db");
        return new NoteDbContext(builder.Options);
    }
}