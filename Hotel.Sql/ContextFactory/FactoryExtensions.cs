﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hotel.Sql.ContextFactory;

public static class FactoryExtensions
{
    /// <summary>
    /// Registers the <seealso cref="DbContextFactory{TContext}"/> with DI.
    /// </summary>
    /// <typeparam name="TContext">The instance of <see cref="DbContext"/> to register.</typeparam>
    /// <param name="collection">The instance of the <see cref="IServiceCollection"/>.</param>
    /// <param name="optionsAction">Optional access to the <see cref="DbContextOptions{TContext}"/>.</param>
    /// <param name="contextAndOptionsLifetime">Set the <see cref="ServiceLifetime"/> of the factory and options.</param>
    /// <returns>The registered <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddContextFactory<TContext>(
        this IServiceCollection collection,
        Action<DbContextOptionsBuilder> optionsAction = null,
        ServiceLifetime contextAndOptionsLifetime = ServiceLifetime.Scoped)
        where TContext : DbContext
    {
        // instantiate with the correctly scoped provider
        collection.Add(new ServiceDescriptor(
            typeof(IContextFactory<TContext>),
            sp => new DbContextFactory<TContext>(sp),
            contextAndOptionsLifetime));

        // dynamically run the builder on each request
        collection.Add(new ServiceDescriptor(
            typeof(DbContextOptions<TContext>),
            sp => GetOptions<TContext>(optionsAction, sp),
            contextAndOptionsLifetime));

        return collection;
    }

    /// <summary>
    /// Gets the options for a specific <seealso cref="TContext"/>.
    /// </summary>
    /// <param name="action">Option configuration action.</param>
    /// <param name="sp">The scoped <see cref="IServiceProvider"/>.</param>
    /// <returns>The newly configured <see cref="DbContextOptions{TContext}"/>.</returns>
    private static DbContextOptions<TContext> GetOptions<TContext>(
        Action<DbContextOptionsBuilder> action,
        IServiceProvider sp = null) where TContext : DbContext
    {
        var optionsBuilder = new DbContextOptionsBuilder<TContext>();
        if (sp != null)
        {
            optionsBuilder.UseApplicationServiceProvider(sp);
        }
        action?.Invoke(optionsBuilder);
        return optionsBuilder.Options;
    }
}