using ProductionModel.Interfaces;

namespace ProductionModel
{
    public class FuzzySet : IFuzzySet
    {
        public string Name { get; set; }

        public List<IFuzzyVar> FuzzyVars { get; private set; }
        public double RangeStart { get; set; }
        public double RangeEnd { get; set; }

        public FuzzySet(string name)
        {
            FuzzyVars = new List<IFuzzyVar>();
            Name = name;
        }
    }
}
