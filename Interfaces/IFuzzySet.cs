namespace ProductionModel.Interfaces
{
    public interface IFuzzySet
    {
        string Name { get; set; }
        List<IFuzzyVar> FuzzyVars { get; }
    }
}