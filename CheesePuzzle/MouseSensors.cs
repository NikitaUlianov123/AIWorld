using AIWorld;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheesePuzzle
{
    public class MouseSensors : ISensorReading
    {
        public byte[] values { get; set; }

        public bool IsTerminal { get; private set; }

        public float Score { get; private set; }

        public MouseSensors(CheeseState state)
        {
            IsTerminal = state.IsTerminal;
            Score = state.Score;
            values = [(byte)state.Mouse.X, (byte)state.Mouse.Y];
        }
    }
}
