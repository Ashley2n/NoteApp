namespace DefaultNamespace;

public class NoteDbContextFactory :IDesignTimeDbContextFactory<NoteEntity>
{
    public NoteDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<NoteDbContext>();
        builder.UseSqlite("Data Source=note.db");
        return new NoteDbContext(builder.Options);
    }
}