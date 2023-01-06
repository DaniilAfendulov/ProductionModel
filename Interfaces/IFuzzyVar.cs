namespace ProductionModel.Interfaces
{
    public interface IFuzzyVar
    {
        string Name { get; set; }
        double CalcDegreeOfTruth(double inputPoint);
        double DefusicatedValue { get; }
    }
}