using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIWorld
{
    public class AgentRunner<TState, TSensors> where TState : IAgentState, new()
                                               where TSensors : ISensorReading
    {
        public IEnvironment<TState, TSensors> environment;
        
        public List<IAgent<TSensors>> agents;

        public AgentRunner(IEnvironment<TState, TSensors> env, IAgent<TSensors> agent)
        {
            environment = env;
            agents = new List<IAgent<TSensors>>();
            agents.Add(agent);
            env.AddAgent(0, new TState());
        }

        public void DoTurn()
        { 
            for (int i = 0;i < agents.Count;i++)
            {
                var successors = environment.GetActions(i);
                var move = agents[i].Move(successors);
                agents[i].CurrentGameState = environment.MakeMove(move, i);
            }
        }

        public void PlayerTurn(Akshun<TSensors> move)
        { 
            var newState = environment.MakeMove(move, 0);
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].CurrentGameState = newState;
            }
        }
    }
}