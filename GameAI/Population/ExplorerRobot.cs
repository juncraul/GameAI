using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;
using System.Drawing;

namespace Population
{
    public class ExplorerRobot : BaseRobot
    {
        double explorationRange;

        public ExplorerRobot(Vector2 position, Color color) : base(position, color)
        {
            explorationRange = 50;
        }

        public void DoLogic(bool[,] map, SizeF cellSize, SizeF worldSize)
        {
            for(int i = 0; i < map.GetLength(0); i ++)
            {
                for(int j = 0; j < map.GetLength(1); j ++)
                {
                    if(Functions.DistanceBetweenTwoPoints(Position, new Vector2(j * cellSize.Width, worldSize.Height - i * cellSize.Height)) < explorationRange)
                    {
                        map[i, j] = true;
                    }
                }
            }
        }
    }
}
