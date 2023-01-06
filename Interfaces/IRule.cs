namespace ProductionModel.Interfaces
{
    public interface IRule
    {
        double CalcDegreeOfTruth(double[] inputPoints);
        IFuzzyVar[] GetOutputs();
    }
}