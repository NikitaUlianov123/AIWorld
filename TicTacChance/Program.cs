using AIWorld;

namespace TicTacChance
{
    public class Program
    {
        static void Main(string[] args)
        {
            var env = new TicTacEnvironment();
            var runner = new AgentRunner<TicTacState>(env, new ExpectiMax<TicTacState>(env.GetSuccessors, new TicTacState()));
        }
    }
}