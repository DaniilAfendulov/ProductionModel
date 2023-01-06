namespace ProductionModel.Interfaces
{
    public interface IRule
    {
        double CalcDegreeOfTruth(double[] inputPoints);
        List<IFuzzyVar> Outputs { get; }
        List<IFuzzyVar> Inputs { get; }
    }
}