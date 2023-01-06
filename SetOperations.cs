using System.ComponentModel.DataAnnotations;

namespace ProductionModel
{
    public static class SetOperations
    {
        public static double Min(IEnumerable<double> inputs) => inputs.Min(x => x);
        public static double Max(IEnumerable<double> inputs) => inputs.Max(x => x);
    }
}
