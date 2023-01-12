using ProductionModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionModel
{
    public class TriangleVar : IFuzzyVar
    {
        private double[] _points;
        private LineFunction[] _lineFunctions;

        public TriangleVar(string name, double[] points)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _lineFunctions = new LineFunction[2];
            Points = points ?? throw new ArgumentNullException(nameof(points));
        }

        public double[] Points 
        { 
            get => _points;
            set
            {
                if (value.Length != 3)
                {
                    throw new ArgumentOutOfRangeException(nameof(Points));
                }
                _points = value;

                _lineFunctions[0] = new LineFunction(new Tuple<double,double>(_points[0], 0), new Tuple<double, double>(_points[1], 1));
                _lineFunctions[1] = new LineFunction(new Tuple<double, double>(_points[1], 1), new Tuple<double, double>(_points[2], 0));
            }
        }

        public string Name { get; set; }

        public double DefusicatedValue => (Points[0] + Points[1] + Points[2]) / 3;

        public double CalcDegreeOfTruth(double inputPoint)
        {
            if (inputPoint < Points[0] || inputPoint > Points[2]) return 0;
            if (inputPoint < Points[1]) return _lineFunctions[0].CalcY(inputPoint);
            return _lineFunctions[1].CalcY(inputPoint);
        }
    }
}
