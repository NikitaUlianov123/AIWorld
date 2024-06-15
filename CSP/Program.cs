namespace CSP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1, 2, 3, 4, 5, 6
            //T, W, O, F, U, R

            List<IVariable<int>> vars = new List<IVariable<int>>();
            List<int> nums = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
            List<int> positive = [1, 2, 3, 4, 5, 6, 7, 8, 9];
            vars.Add(new Variable<int>(1, 1, positive));
            vars.Add(new Variable<int>(2, 2, nums));
            vars.Add(new Variable<int>(3, 3, nums));
            vars.Add(new Variable<int>(4, 4, positive));
            vars.Add(new Variable<int>(5, 5, nums));
            vars.Add(new Variable<int>(6, 6, nums));
            Dictionary<int, List<(Func<int, int, bool>, int)>> parameters = new Dictionary<int, List<(Func<int, int, bool>, int)>>();
            parameters.Add(1, [(NotEquals, 2), (NotEquals, 3), (NotEquals, 4), (NotEquals, 5), (NotEquals, 6)]);
            parameters.Add(2, [(NotEquals, 1), (NotEquals, 3), (NotEquals, 4), (NotEquals, 5), (NotEquals, 6)]);
            parameters.Add(3, [(NotEquals, 1), (NotEquals, 2), (NotEquals, 4), (NotEquals, 5), (NotEquals, 6), (Sum, 1)]);
            parameters.Add(4, [(NotEquals, 1), (NotEquals, 2), (NotEquals, 3), (NotEquals, 5), (NotEquals, 6), (Sum, )]);
            parameters.Add(5, [(NotEquals, 1), (NotEquals, 2), (NotEquals, 3), (NotEquals, 4), (NotEquals, 6), (Sum, 2)]);
            parameters.Add(6, [(NotEquals, 1), (NotEquals, 2), (NotEquals, 3), (NotEquals, 4), (NotEquals, 5), (Sum, 3)]);

        }

        /// <summary>
        /// Solves problem
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="variables"></param>
        /// <param name="parameters">Map of id of parameter 1 to list of tuple of constraint and id of parameter 2</param>
        /// <returns></returns>
        public List<T> Solve<T>(List<IVariable<T>> variables,
                                Dictionary<int, List<(Func<T, T, bool>, int)>> parameters,
                                List<T> solutions = null) where T : IComparable<T>
        {
            if (solutions == null)
            { 
                solutions = new List<T>();
            }
            Random randy = new Random();
            bool done = false;
            while (!done)
            { 
                for (int i = 0; i < variables.Count; i++)//randomize all variables
                {
                    variables[i].value = variables[i].Domain[randy.Next(variables[i].Domain.Count)];
                }

                done = true;
                for (int i = 0; i < variables.Count * parameters.Count; i++)
                {
                    foreach (var variable in variables)//check constraints
                    {
                        foreach (var constraint in parameters[variable.ID])
                        {
                            if (!done && !constraint.Item1(variable.value, variables.First(x => x.ID == constraint.Item2).value))
                            {
                                done = false;
                                variable.value = variable.Domain[randy.Next(variable.Domain.Count)];
                            }
                        }
                    }
                    if (done) break;
                }
            }

            return solutions;
        }

        static bool NotEquals(int x, int y) => x != y;
        static bool Sum(int x, int y) => x == (y * 2) % 10;
    }
}