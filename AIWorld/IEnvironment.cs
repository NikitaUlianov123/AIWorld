using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIWorld
{
    public interface IEnvironment<TSensors> where TSensors : ISensorReading
    {
        void AddAgent(int ID, TSensors state);

        List<Akshun<TSensors>> GetActions(int agentID);

        List<Akshun<TSensors>> GetActions(TSensors state);

        TSensors MakeMove(Akshun<TSensors> move, int agentID);
    }
}