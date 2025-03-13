using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Testcontainers.PostgreSql;

namespace ReproNreJsonElement;

sealed class MyTest
{
    private readonly PostgreSqlContainer _pg = new PostgreSqlBuilder().WithImage("postgres:17-alpine").Build();

    [OneTimeSetUp]
    public async Task GetDbAsync()
    {
        await _pg.StartAsync();
    }

    [OneTimeTearDown]
    public async Task TearDownAsync()
    {
        await _pg.DisposeAsync();
    }

    [Test]
    public async Task MyTest1()
    {
        await using var db = new MyDbContext(_pg.GetConnectionString());
        db.RootEntities.Add(new()
        {
            Owned = new()
            {
                Untyped = JsonSerializer.SerializeToElement(new { x = 5, y = 7 })
            }
        });
        await db.SaveChangesAsync();
    }
}

class RootEntity
{
    public long Id { get; set; }
    public OwnedEntity? Owned { get; set; }
}

class OwnedEntity
{
    public JsonElement? Untyped { get; set; }
}

class MyDbContext(string connectionString) : DbContext
{
    public DbSet<RootEntity> RootEntities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<RootEntity>().OwnsOne<OwnedEntity>(r => r.Owned,
            o => o.ToJson());
    }
}