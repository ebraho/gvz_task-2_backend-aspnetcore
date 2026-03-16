using Microsoft.EntityFrameworkCore;

namespace GVZ.Task2BackendASPNETCore;

public class MessagesContext : DbContext
{
    public virtual DbSet<QuestionMessage> QuestionMessages { get; set; } = null!;

    public MessagesContext(DbContextOptions<MessagesContext> options) : base(options) { }

    protected MessagesContext() : base() { }
}
