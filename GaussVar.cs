using ProductionModel.Interfaces;

namespace ProductionModel
{
    public class GaussVar : IFuzzyVar
    {
        public GaussVar(string name, double c, double sigma)
        {
            Name = name;
            C = c;
            Sigma = sigma;
        }

        public GaussVar(string name, double[] points):this(name, points[0], points[1])
        {
        }

        public double C, Sigma;
        public string Name { get; set; }

        public double DefusicatedValue => C;

        public double CalcDegreeOfTruth(double x)
        {
            return Math.Exp(-(x - C) * (x - C) / (2 * Sigma * Sigma));
        }
    }
}