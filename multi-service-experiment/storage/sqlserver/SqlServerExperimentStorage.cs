namespace Services.Storage.SqlServer;

internal class SqlServerExperimentStorage : IExperimentStorage
{
    public SqlServerExperimentStorage()
    {
    }
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

    SqlServerExperiment experimentContext = new();
}
