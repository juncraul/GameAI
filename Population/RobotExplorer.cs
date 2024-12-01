using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;
using System.Drawing;

namespace Population
{
    public class RobotExplorer : BaseRobot
    {
        double explorationRange;
        
        public RobotExplorer(Vector2 position, Random rand) : base(position, rand)
        {
            explorationRange = 50;
            Color = Color.Red;
            Size = new Size(20, 20);
        }

        public void DoLogic(SizeF cellSize, bool[,] map, SizeF worldSize)
        {
            if (WhereToGo == null || Functions.DistanceBetweenTwoPoints(Position, WhereToGo) < destinationRange)
            {
                WhereToGo = new Vector2(Random.NextDouble() * worldSize.Width, Random.NextDouble() * worldSize.Height);
            }
            double distance = Functions.DistanceBetweenTwoPoints(Position, WhereToGo);

            for(int i = 0; i < map.GetLength(0); i ++)
            {
                for(int j = 0; j < map.GetLength(1); j ++)
                {
                    if(Functions.DistanceBetweenTwoPoints(Position, new Vector2(j * cellSize.Width + cellSize.Width / 2, i * cellSize.Height + cellSize.Height / 2)) < explorationRange)
                    {
                        map[i, j] = true;
                    }
                }
            }
        }
    }
}
