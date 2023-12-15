namespace Services.Storage.Sqlite;

internal class SqliteExperimentStorage : IExperimentStorage
{
    public ExperimentData? GetExperimentDataByNumber(int number)
    {
        var data = experimentContext.Experiments.Find(new object?[] { number });
        if (data == null)
        {
            return null;
        }
        return ExperimentData.FromJson(data.CardsJson);
    }

    public void StoreExperiment(in ExperimentData experimentData)
    {
        experimentContext.Add(new ExperimentDataRecord()
        {
            CardsJson = experimentData.ToJson(),
        });
        experimentContext.SaveChanges();
    }

    private readonly Sqlite.ExperimentSqlite experimentContext = new ExperimentSqlite();
}
