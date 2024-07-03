using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIWorld
{
    public interface IEnvironment<TState, TSensors> where TState : IAgentState
                                                    where TSensors : ISensorReading
    {
        Dictionary<int, TState> AgentInfo { get; }

        List<Akshun<TSensors>> GetActions(int agentID);

        TSensors MakeMove(Akshun<TSensors> move, int agentID);
    }
}