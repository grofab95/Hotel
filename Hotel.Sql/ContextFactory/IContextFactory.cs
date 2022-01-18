using Microsoft.EntityFrameworkCore;

namespace Hotel.Sql.ContextFactory;

public interface IContextFactory<TContext> where TContext : DbContext
{
    /// <summary>
    /// Generate a new <see cref="DbContext"/>.
    /// </summary>
    /// <returns>A new instance of <see cref="TContext"/>.</returns>
    TContext CreateDbContext();
}