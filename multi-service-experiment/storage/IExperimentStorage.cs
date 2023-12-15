namespace Services.Storage;

internal interface IExperimentStorage
{
    public abstract void StoreExperiment(in ExperimentData experimentData);
    public abstract ExperimentData? GetExperimentDataByNumber(int number);
}
