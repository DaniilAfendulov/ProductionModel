using ProductionModel.Interfaces;
using System.Data;

namespace ProductionModel
{
    /// <summary>
    /// Class for parsing matlab fis model
    /// </summary>
    public class FisModelParser
    {
        public ProductionModel Parse(string filepath)
        {
            ProductionModel productionModel;
            using (StreamReader sr = new(filepath))
            {
                Skip(sr, 4);
                // [System]
                // Name
                // Type
                // Version

                int numInputs = ReadInt(sr);
                int numOutputs = ReadInt(sr);
                int numRules = ReadInt(sr);

                Skip(sr, 5);
                // AndMethod
                // OrMethod
                // ImpMethod
                // AggMethod
                // DefuzzMethod

                IFuzzySet[] Inputs = new IFuzzySet[numInputs];
                for (int i = 0; i < numInputs; i++)
                {
                    Inputs[i] = ReadFuzzyInpOut(sr);
                }
                

                IFuzzySet[] Outputs = new IFuzzySet[numOutputs];
                for (int i = 0; i < numOutputs; i++)
                {
                    Outputs[i] = ReadFuzzyInpOut(sr);
                }
 

                productionModel = new ProductionModel(Inputs.ToList(), Outputs.ToList());

                string line;
                do
                {
                    line = sr.ReadLine();
                } while (string.IsNullOrWhiteSpace(line));
                if (!line.Contains("Rules")) throw new InvalidCastException(line); // [Rules]

                for (int i = 0; i < numRules; i++)
                {
                    ReadRule(sr, productionModel);
                }

            }

            return productionModel;
        }

        private void ReadRule(StreamReader sr, ProductionModel productionModel)
        {
            string[] parts = sr.ReadLine().Split(',');
            int[] inputs = parts[0].Split(' ',StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)-1).ToArray();
            int[] outputs = parts[1].Split('(')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)-1).ToArray();
            productionModel.AddRule(inputs, outputs);
        }

        private void Skip(StreamReader sr, int linesAmount = 1)
        {
            for (int i = 0; i < linesAmount; i++)
            {
                sr.ReadLine();
            }
        }

        private int ReadInt(StreamReader sr)
        {
            return int.Parse(ReadString(sr));
        }

        /// <summary>
        /// Read value after '='
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        private string ReadString(StreamReader sr)
        {
            string line = sr.ReadLine();
            return line.Split('=')[1];
        }

        private string ReadName(StreamReader sr)
        {
            return ReadWithoutBrackets(sr);
        }

        private string ReadWithoutBrackets(StreamReader sr)
        {
            return ReadWithoutBrackets(ReadString(sr));
        }

        private string ReadWithoutBrackets(string line)
        {
            line = line.Substring(1, line.Length - 2);
            return line;
        }

        private IEnumerable<double> ReadNumbers(StreamReader sr)
        {
            return ReadNumbers(sr.ReadLine());
        }

        private IEnumerable<double> ReadNumbers(string line)
        {
            return ReadWithoutBrackets(line)
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => double.Parse(x));
        }

        private IFuzzySet ReadFuzzyInpOut(StreamReader sr)
        {
            string line;
            do
            {
                line = sr.ReadLine();
            } while (string.IsNullOrWhiteSpace(line));
            if (!(line.Contains("Input") || line.Contains("Output"))) throw new InvalidCastException(line); // [Input1]


            var set = new FuzzySet(ReadName(sr)); // Name='Teta'
            var range = ReadNumbers(ReadString(sr)).ToArray(); // Range=[-4 4]
            set.RangeStart = range[0];
            set.RangeEnd = range[1];

            int numMFs = ReadInt(sr); // NumMFs=5
            for (int i = 0; i < numMFs; i++)
            {
                set.FuzzyVars.Add(ReadFuzzyVar(sr)); // MF1='NM':'trimf',[-6 -4 -2]
            }
            return set;
        }

        private IFuzzyVar ReadFuzzyVar(StreamReader sr)
        {
            string[] lines = ReadString(sr).Split(new char[] { ':', ',' });
            string name = ReadWithoutBrackets(lines[0]);
            string type = ReadWithoutBrackets(lines[1]);
            double[] points = ReadNumbers(lines[2]).ToArray();
            switch (type)
            {
                case "trimf":
                    return new TriangleVar(name, points);
                case "gaussmf":
                    return new GaussVar(name, points);
            }
            throw new InvalidCastException(nameof(ReadFuzzyVar));
        }
    }
}
