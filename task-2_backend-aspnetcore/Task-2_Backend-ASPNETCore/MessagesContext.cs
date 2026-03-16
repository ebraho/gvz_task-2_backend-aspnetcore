using Microsoft.EntityFrameworkCore;

namespace GVZ.Task2BackendASPNETCore;

public class MessagesContext : DbContext
{
    public virtual DbSet<QuestionMessage> QuestionMessages { get; set; } = null!;

    public virtual DbSet<AdministrativeChangeMessage> AdministrativeChangeMessages { get; set; } = null!;

    public MessagesContext(DbContextOptions<MessagesContext> options) : base(options) { }

    protected MessagesContext() : base() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdministrativeChangeMessage>().OwnsOne(m => m.PostalAddress);
    }
}
