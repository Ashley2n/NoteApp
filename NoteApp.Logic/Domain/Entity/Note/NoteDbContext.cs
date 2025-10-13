namespace NoteApp.Domain.Entity.Note;

public sealed class NoteDbContext : DbContext
{
    public NoteDbContext(DbContextOptions<NoteEntity> options) : base(options) {}
    
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