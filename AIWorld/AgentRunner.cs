using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIWorld
{
    public class AgentRunner<T> where T : IGameState
    {
        public IEnvironment<T> environment;
        
        public List<IAgent<T>> agents;

        public AgentRunner(IEnvironment<T> env, IAgent<T> agent)
        {
            environment = env;
            agents = new List<IAgent<T>>();
            agents.Add(agent);
        }

        public void DoTurn()
        { 
            for (int i = 0;i < agents.Count;i++)
            {
                agents[i].CurrentGameState = environment.MakeMove(agents[i].Move(environment.GetSuccessors(agents[i].CurrentGameState)), agents[i].CurrentGameState);
            }
        }
    }
}