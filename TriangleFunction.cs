using ProductionModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionModel
{
    public class TriangleFunction : IFuzzyVar
    {
        public TriangleFunction(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));

        }

        public string Name { get; set; }

        public double DefusicatedValue => throw new NotImplementedException();

        public double CalcDegreeOfTruth(double inputPoint)
        {
            throw new NotImplementedException();
        }
    }
}
