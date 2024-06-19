namespace CSP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1, 2, 3, 4, 5, 6
            //T, W, O, F, U, R

            List<IVariable<int>> vars = new List<IVariable<int>>();
            List<int> Colors = [0, 1, 2];
            for (int i = 0; i < 27; i++)
            {
                vars.Add(new Variable<int>(i % Colors.Count, i, Colors));
            }

            Dictionary<int, List<(Func<int, int, bool>, int)>> parameters = new Dictionary<int, List<(Func<int, int, bool>, int)>>();
            parameters.Add(0, [(NotEquals, 1), (NotEquals, 20), (NotEquals, 22), (NotEquals, 23)]);
            parameters.Add(1, [(NotEquals, 0), (NotEquals, 2), (NotEquals, 6)]);
            parameters.Add(2, [(NotEquals, 1), (NotEquals, 3), (NotEquals, 6), (NotEquals, 7)]);
            parameters.Add(3, [(NotEquals, 2), (NotEquals, 4), (NotEquals, 7)]);
            parameters.Add(4, [(NotEquals, 3), (NotEquals, 5), (NotEquals, 7), (NotEquals, 8)]);
            parameters.Add(5, [(NotEquals, 4), (NotEquals, 8), (NotEquals, 9)]);
            parameters.Add(6, [(NotEquals, 1), (NotEquals, 2), (NotEquals, 7)]);
            parameters.Add(7, [(NotEquals, 2), (NotEquals, 3), (NotEquals, 4), (NotEquals, 6), (NotEquals, 12)]);
            parameters.Add(8, [(NotEquals, 4), (NotEquals, 5), (NotEquals, 9), (NotEquals, 12)]);
            parameters.Add(9, [(NotEquals, 5), (NotEquals, 8), (NotEquals, 12)]);
            parameters.Add(10, [(NotEquals, 14)]);
            parameters.Add(11, [(NotEquals, 15)]);
            parameters.Add(12, [(NotEquals, 7), (NotEquals, 8), (NotEquals, 9), (NotEquals, 16)]);
            parameters.Add(13, [(NotEquals, 16), (NotEquals, 17)]);
            parameters.Add(14, [(NotEquals, 10), (NotEquals, 15)]);
            parameters.Add(15, [(NotEquals, 11), (NotEquals, 14), (NotEquals, 18), (NotEquals, 19)]);
            parameters.Add(16, [(NotEquals, 12), (NotEquals, 13)]);
            parameters.Add(17, [(NotEquals, 13), (NotEquals, 20), (NotEquals, 21)]);
            parameters.Add(18, [(NotEquals, 15), (NotEquals, 24)]);
            parameters.Add(19, [(NotEquals, 15), (NotEquals, 25)]);
            parameters.Add(20, [(NotEquals, 0), (NotEquals, 17), (NotEquals, 24), (NotEquals, 25)]);
            parameters.Add(21, [(NotEquals, 17), (NotEquals, 26)]);
            parameters.Add(22, [(NotEquals, 0), (NotEquals, 23)]);
            parameters.Add(23, [(NotEquals, 0), (NotEquals, 22)]);
            parameters.Add(24, [(NotEquals, 18), (NotEquals, 20), (NotEquals, 25)]);
            parameters.Add(25, [(NotEquals, 19), (NotEquals, 20), (NotEquals, 24)]);
            parameters.Add(26, [(NotEquals, 21)]);

            var result = Solve<int>(vars, parameters);
        }

        /// <summary>
        /// Solves problem
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="variables"></param>
        /// <param name="parameters">Map of id of parameter 1 to list of tuple of constraint and id of parameter 2</param>
        /// <returns></returns>
        public static List<T> Solve<T>(List<IVariable<T>> variables,
                                Dictionary<int, List<(Func<T, T, bool>, int)>> parameters,
                                int current = 0) where T : IComparable<T>
        {
            Random randy = new Random();
            for (int i = 0; i < parameters[current].Count; i++)
            {
                while (!parameters[current][i].Item1(variables[current].value, variables[parameters[current][i].Item2].value))
                {
                    variables[parameters[current][i].Item2].value = variables[parameters[current][i].Item2].Domain[randy.Next(variables[parameters[current][i].Item2].Domain.Count)];
                }
            }

            bool done = true;
            foreach (var variable in variables)
            {
                foreach (var constraint in parameters[variable.ID])
                {
                    if (constraint.Item1(variable.value, variables.First(x => x.ID == constraint.Item2).value))
                    { 
                        done = false;
                        break;
                    }
                }
            }
            if (done)
            {
                return variables.Select(x => x.value).ToList();
            }
            return Solve<T>(variables, parameters, current + 1);
        }

        static bool NotEquals(int x, int y) => x != y;
        static bool Sum(int x, int y) => x == (y * 2) % 10;
    }
}