namespace ProductionModel
{
    public class LineFunction
    {
        public readonly double k, b;
        public LineFunction(double k, double b)
        {
            this.k = k;
            this.b = b;
        }
        public LineFunction(Tuple<double, double> point1, Tuple<double, double> point2)
        {
            k = (point2.Item2 - point1.Item2) / (point2.Item1 - point1.Item1);
            b = point1.Item2 - k * point1.Item1;
        }

        public double CalcY(double x) => k * x + b;
        public double CalcX(double y) => (y - b) / k;
    }
}
