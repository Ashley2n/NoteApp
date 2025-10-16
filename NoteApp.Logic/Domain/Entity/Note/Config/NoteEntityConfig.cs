
using NoteApp.Logic.Domain.Entity.Note.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NoteApp.Logic.Domain.Entity.Note.Config;
public sealed class NoteEntityConfig
{
    public void Configure(EntityTypeBuilder<NoteEntity> builder)
    {
        // Contain an ID
        builder.ToTable("Note");
        builder.HasKey(n => n.Id);

        // The Value is Automaticlly generated
        builder.Property(n => n.Id)
            .ValueGeneratedOnAdd();
        
        // Requirement for Title
        builder.Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(50);
    }
}