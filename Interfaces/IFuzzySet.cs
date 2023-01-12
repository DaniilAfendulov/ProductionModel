namespace ProductionModel.Interfaces
{
    public interface IFuzzySet
    {
        string Name { get; set; }
        List<IFuzzyVar> FuzzyVars { get; }
        public double RangeStart { get; set; }
        public double RangeEnd { get; set; }
    }
}