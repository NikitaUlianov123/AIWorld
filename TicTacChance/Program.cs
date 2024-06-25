using AIWorld;

namespace TicTacChance
{
    public class Program
    {
        static void Main(string[] args)
        {
            var env = new TicTacEnvironment();
            var runner = new AgentRunner<TicTacState>(env, new ExpectiMax<TicTacState>(env.GetSuccessors, new TicTacState()));
            Random randy = new Random();
            while (!runner.agents[0].CurrentGameState.IsTerminal)
            {
                runner.DoTurn();
                Console.WriteLine($"{runner.agents[0].CurrentGameState.Grid[0, 0]}|{runner.agents[0].CurrentGameState.Grid[0, 1]}|{runner.agents[0].CurrentGameState.Grid[0, 2]}");
                Console.WriteLine($"{runner.agents[0].CurrentGameState.Grid[1, 0]}|{runner.agents[0].CurrentGameState.Grid[1, 1]}|{runner.agents[0].CurrentGameState.Grid[1, 2]}");
                Console.WriteLine($"{runner.agents[0].CurrentGameState.Grid[2, 0]}|{runner.agents[0].CurrentGameState.Grid[2, 1]}|{runner.agents[0].CurrentGameState.Grid[2, 2]}");
                int x = int.Parse(Console.ReadLine());
                int y = int.Parse(Console.ReadLine());
                env.MakeMove(new TicTacState(runner.agents[0].CurrentGameState.Grid, false, randy.NextDouble() < TicTacEnvironment.chances[x, y], (x, y)), runner.agents[0].CurrentGameState);
            }
        }
    }
}