using ProductionModel.Interfaces;
using System.Collections.Generic;
using System.Drawing;

namespace ProductionModel
{
    public class ProductionModel
    {
        private readonly List<IRule> _rules = new();
        private readonly List<IFuzzySet> _inputs = new();
        private readonly List<IFuzzySet> _outputs = new();

        public ProductionModel(List<IRule> rules, List<IFuzzySet> inputs, List<IFuzzySet> outputs)
        {
            CheckInputs(rules, inputs, outputs);
            _rules = rules;
            _inputs = inputs;
            _outputs = outputs;
        }
        public ProductionModel(List<IFuzzySet> inputs, List<IFuzzySet> outputs):
            this(new(), inputs, outputs)
        {
        }
        public ProductionModel():this(new(), new())
        {
        }

        private bool CheckInputs(List<IRule> rules, List<IFuzzySet> inputs, List<IFuzzySet> outputs)
        {
            if (rules is null) throw new ArgumentNullException(nameof(rules));
            if (inputs is null) throw new ArgumentNullException(nameof(inputs));
            if (outputs is null) throw new ArgumentNullException(nameof(outputs));

            return true;
        }



        public IFuzzyVar[] Execute(double[] inputPoints)
        {
            if (inputPoints.Length != _inputs.Count)
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
            return _rules[mostTrustedRuleIndex].GetOutputs();
        }
    }
}