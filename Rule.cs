using ProductionModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProductionModel
{
    public class Rule : IRule
    {
        private Func<IEnumerable<double>, double> _concatinateOperation;

        public List<IFuzzyVar> Outputs { get; private set; }
        public List<IFuzzyVar> Inputs { get; private set; }


        public Func<IEnumerable<double>, double> ConcatinateOperation
        {
            get => _concatinateOperation;
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(ConcatinateOperation));
                }
                _concatinateOperation = value;
            }
        }

        public Rule( IEnumerable<IFuzzyVar> inputs, IEnumerable<IFuzzyVar> outputs, Func<IEnumerable<double>, double> concatinateOperation)
        {
            Outputs = outputs.ToList();
            Inputs = inputs.ToList();
            ConcatinateOperation = concatinateOperation ?? throw new ArgumentNullException(nameof(concatinateOperation)); ;
        }

        public double CalcDegreeOfTruth(double[] inputPoints)
        {
            if (inputPoints.Length != Inputs.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(inputPoints));
            }

            IEnumerable<double> truthDegrees = Inputs.Select((inp, i) => inputPoints[i]);

            return _concatinateOperation(truthDegrees);
        }

       
    }
}
