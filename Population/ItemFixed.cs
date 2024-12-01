using Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Population
{
    public class ItemFixed : BaseItem
    {
        double _outputRange;

        public ItemFixed(Vector2 position, int value) : base(position)
        {
            Value = value;
            _outputRange = 10;
        }

        public ItemMovable Mine(int value)
        {
            if (!IsAlive) return null;
            Random random = new Random();

            int CollectedAmount = Value - value < 0 ? Value : value;
            Vector2 itemOffset = new Vector2(1, 0).Rotate(random.NextDouble() * Math.PI * 2) * (_outputRange + random.NextDouble() * _outputRange);
            ItemMovable itemMovable = new ItemMovable(Position + itemOffset, Value)
            {
                Value = CollectedAmount
            };
            Value -= CollectedAmount;
            if(Value <= 0)
            {
                IsAlive = false;
            }


            return itemMovable;
        }
    }
}
