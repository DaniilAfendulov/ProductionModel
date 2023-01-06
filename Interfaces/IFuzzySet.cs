using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionModel.Interfaces
{
    /// <summary>
    /// Нечеткое множество
    /// </summary>
    public interface IFuzzySet
    {
        public IFuzzyVar[] GetAllVariables();
        public IFuzzyVar[] GetSuitableVariables(double inputPoint);
        public int[] GetSuitableVariablesIndexes(double v);
    }
}
