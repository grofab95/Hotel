using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hotel.Sql.ContextFactory;

public class DbContextFactory<TContext>
    : IContextFactory<TContext> where TContext : DbContext
{
    private readonly IServiceProvider provider;

    public DbContextFactory(IServiceProvider provider)
    {
        this.provider = provider;
    }

    public TContext CreateDbContext()
    {
        if (provider == null)
        {
            throw new InvalidOperationException(
                $"You must configure an instance of IServiceProvider");
        }

        return ActivatorUtilities.CreateInstance<TContext>(provider);
    }
}