using Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Population
{
    public class RobotMiner : BaseRobot
    {
        public RobotMiner(Vector2 position, Random random) : base(position, random)
        {
            Color = Color.Orchid;
            Size = new Size(20, 20);
        }
    }
}
