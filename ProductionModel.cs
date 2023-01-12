using ProductionModel.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace ProductionModel
{
    public class ProductionModel
    {
        private readonly List<IRule> _rules = new();
        public readonly List<IFuzzySet> Inputs = new();
        public readonly List<IFuzzySet> Outputs = new();
        private readonly Func<IEnumerable<double>, double> _concatinateOperation = SetOperations.Min;

        public ProductionModel(List<IRule> rules, List<IFuzzySet> inputs, List<IFuzzySet> outputs)
        {
            CheckInputs(rules, inputs, outputs);
            _rules = rules;
            Inputs = inputs;
            Outputs = outputs;
        }
        public ProductionModel(List<IFuzzySet> inputs, List<IFuzzySet> outputs):
            this(new(), inputs, outputs)
        {
        }
        public ProductionModel():this(new(), new())
        {
        }

        public string Name { get; set; }

        private bool CheckInputs(List<IRule> rules, List<IFuzzySet> inputs, List<IFuzzySet> outputs)
        {
            if (rules is null) throw new ArgumentNullException(nameof(rules));
            if (inputs is null) throw new ArgumentNullException(nameof(inputs));
            if (outputs is null) throw new ArgumentNullException(nameof(outputs));

            return true;
        }

        public void AddRule(int[] inputsIndexes, int[] outputsIndexes)
        {
            if (inputsIndexes.Length != Inputs.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(inputsIndexes));
            }
            if (outputsIndexes.Length != Outputs.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(outputsIndexes));
            }

            IEnumerable<IFuzzyVar> inputs = Inputs.Select((inp, i) => inp.FuzzyVars[i]);
            IEnumerable<IFuzzyVar> outputs = Outputs.Select((outp, i) => outp.FuzzyVars[i]);
            _rules.Add(new Rule(inputs, outputs, _concatinateOperation));
        }

        public void RemoveRule(int index) => _rules.RemoveAt(index);

        public IFuzzyVar[] Execute(double[] inputPoints)
        {
            if (inputPoints.Length != Inputs.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(inputPoints));
            }

            double maxDegreeOfTruth = _rules[0].CalcDegreeOfTruth(inputPoints);
            int mostTrustedRuleIndex = 0;
            for (int i = 1; i < _rules.Count; i++)
            {
                if (_rules[i].CalcDegreeOfTruth(inputPoints) > maxDegreeOfTruth)
                {
                    mostTrustedRuleIndex = i;
                }
            }
            return _rules[mostTrustedRuleIndex].Outputs.ToArray();
        }

        public double[] Defuzzy(IFuzzyVar[] fuzzyVars) => fuzzyVars.Select(v => v.DefusicatedValue).ToArray();

        public double[] ExecuteWithDefuzzy(double[] inputPoints)
        {
            return Defuzzy(Execute(inputPoints));
        }
    }
}