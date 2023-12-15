using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Storage.Sqlite;

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

internal class ExperimentSqlite : DbContext
{
    public ExperimentSqlite()
    {
        SqliteConnectionStringBuilder stringBuilder = new()
        {
            DataSource = "experiments.sqlite",
            Mode = SqliteOpenMode.Memory,
        };

        Database.SetDbConnection(new SqliteConnection(stringBuilder.ConnectionString));
        Experiments = Set<ExperimentDataRecord>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ExperimentDataRecord>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder contextOptionsBuilder)
    {
        contextOptionsBuilder.UseSqlite();
    }

    public DbSet<ExperimentDataRecord> Experiments
    {
        get; private set;
    }
}
