using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;

namespace Services.Storage.SqlServer;

[Table("experiments")]
class ExperimentDataRecord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id
    {
        get; set;
    }

    [Column("cards")]
    public string CardsJson
    {
        get; set;
    }
}

internal class SqlServerExperiment : DbContext
{
    public SqlServerExperiment()
    {
        SqlConnectionStringBuilder builder = new()
        {
            DataSource = "127.0.0.1",
            IntegratedSecurity = true,
        };
        Database.SetConnectionString(builder.ConnectionString);
        Experiments = Set<ExperimentDataRecord>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ExperimentDataRecord>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder contextOptionsBuilder)
    {
        contextOptionsBuilder.UseSqlServer();
    }

    public DbSet<ExperimentDataRecord> Experiments
    {
        get; private set;
    }
}
