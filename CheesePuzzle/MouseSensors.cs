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

        public override bool Equals(object? obj)
        {
            if (obj.GetType() != typeof(MouseSensors)) return false;

            MouseSensors other = (MouseSensors)obj;

            if (other.values.Length != values.Length) return false;

            for (int i = 0; i < values.Length; i++)
            {
                if (other.values[i] != values[i]) return false;
            }

            return other.IsTerminal == IsTerminal && other.Score == Score;
        }

        public override int GetHashCode()
        {
            var result = Score.GetHashCode() + IsTerminal.GetHashCode();
            foreach (var item in values)
            {
                result += item.GetHashCode();
            }
            return result;
        }
    }
}
