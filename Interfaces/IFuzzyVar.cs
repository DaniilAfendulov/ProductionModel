namespace ProductionModel.Interfaces
{
    public interface IFuzzyVar
    {
        public string Name { get; set; }
        double CalcDegreeOfTruth(double inputPoint);
    }
}