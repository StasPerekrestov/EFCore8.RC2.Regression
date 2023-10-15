using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Npgsql;

await using var db = new DemoDbContext();
var compiled =
    EF.CompileAsyncQuery(
        (DemoDbContext ctx, int id) => from t in ctx.Users
            where t.Id == id
            select new LookupModel(t.Id, t.FirstName, null));

await FetchAndDump(db);
await using var db2 = new DemoDbContext();
await FetchAndDump(db2);

async Task FetchAndDump(DemoDbContext d)
{
    await foreach (var a in compiled(d, 1))
    {
        
    }
}

public class DemoDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var npgsqlDataSourceBuilder = new NpgsqlDataSourceBuilder
        {
            ConnectionStringBuilder =
            {
                ConnectionString =
                    "Server=localhost;Port=5432;Database=postgres;User Id=postgres; Password=secret;Include Error Detail=true"
            }
        };
        /*
            NpgsqlDataSourceBuilder is used to be able to call EnableDynamicJsonMappings to support POCO/jsonb mapping
            npgsqlDataSourceBuilder.EnableDynamicJsonMappings();
        */
        options.UseNpgsql(npgsqlDataSourceBuilder.Build())
            .LogTo(Console.WriteLine);
    }
}

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [Column(TypeName = "jsonb")] public SomePoco? SomePoco { get; set; }
}

public class SomePoco
{
    public string[] SomeArray { get; set; }
}

public record LookupModel(int Id, string Code, string Description);