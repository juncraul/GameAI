using Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Population
{
    public abstract class BaseRobot : BaseEntity
    {
        protected Vector2 WhereToGo;
        protected Random Random;
        protected double destinationRange;
        protected double DirectionRadian;

        private double _moveSpeed;
        List<Point> path;

        public bool ShowPath;

        public BaseRobot(Vector2 position, Random rand) : base(position)
        {
            _moveSpeed = 4.5f;
            destinationRange = 10;
            Random = rand;
        }

        public override void Draw(Graphics graphics)
        {

            Brush brush = new SolidBrush(Color);
            graphics.FillEllipse(brush, (float)Position.X - Size.Width / 2, (float)Position.Y - Size.Height / 2, Size.Width, Size.Height);
            

            Pen pen = new Pen(Color);
            if (path != null && path.Count > 1 && ShowPath)
            {
                pen.Color = Color;
                graphics.DrawLines(pen, path.ToArray());
            }
            pen.Color = Color.Black;
            Vector2 direction = (new Vector2(15, 0).Rotate(DirectionRadian));
            graphics.DrawLine(pen, (float)Position.X, (float)(Position.Y), (float)(direction + Position).X, (float)(direction + Position).Y);
        }

        public virtual void Move()
        {
            if (WhereToGo == null) return;
            DirectionRadian = Functions.AngleBetweenTwoPoints(Position, WhereToGo);
            Position += new Vector2(1, 0).Rotate(DirectionRadian).Normaize() * _moveSpeed;
        }

        public virtual void Move(SizeF cellSize, bool[,] mapVisibility)
        {
            if (WhereToGo == null) return;
            Vector2 nextCell = FindTheNextPointToTheShortesPath(Position, WhereToGo, cellSize, mapVisibility);
            DirectionRadian = Functions.AngleBetweenTwoPoints(Position, nextCell);
            Position += new Vector2(1, 0).Rotate(DirectionRadian).Normaize() * _moveSpeed;
        }

        private Vector2 FindTheNextPointToTheShortesPath(Vector2 startPosition, Vector2 endPosition, SizeF cellSize, bool[,] mapVisibility)
        {
            (int startIndexI, int startIndexJ) = GetPositionInGrid(startPosition, cellSize);
            (int endIndexI, int endIndexJ) = GetPositionInGrid(endPosition, cellSize);

            //Dijkstra algorithm
            List<(int, int, double)> vertexes = new List<(int, int, double)>();
            double[,] distances = new double[mapVisibility.GetLength(0), mapVisibility.GetLength(1)];
            Point[,] previous = new Point[mapVisibility.GetLength(0), mapVisibility.GetLength(1)];
            bool[,] visited = new bool[mapVisibility.GetLength(0), mapVisibility.GetLength(1)];

            for (int i = 0; i < mapVisibility.GetLength(0); i ++)
            {
                for(int j = 0; j < mapVisibility.GetLength(1); j ++)
                {
                    distances[i, j] = 10000;
                    previous[i, j] = new Point(-1, -1);
                }
            }

            vertexes.Add((startIndexI, startIndexJ, 0));
            distances[startIndexI, startIndexJ] = 0;

            bool shortestPathWasFound = false;
            while(vertexes.Count > 0 && !shortestPathWasFound)
            {
                double minimumDistance = vertexes.Min(a => a.Item3);
                (int indexI, int indexJ, double distance) vertex = vertexes.Find(a=>a.Item3 == minimumDistance);
                vertexes.Remove(vertex);
                visited[vertex.indexI, vertex.indexJ] = true;

                for (int i = -1; i <= 1; i ++)
                {
                    for(int j = -1; j <= 1; j ++)
                    {
                        int nextPointI = i + vertex.indexI;
                        int nextPointJ = j + vertex.indexJ;
                        //Check if is not going in plus directions
                        //either i or j must be 0, but not both
                        if ((i != 0 && j != 0) || i == j)
                            continue;
                        //Check if is outside the map
                        if (nextPointI < 0 || nextPointI >= mapVisibility.GetLength(0) ||
                            nextPointJ < 0 || nextPointJ >= mapVisibility.GetLength(1))
                            continue;
                        //Check if the cell is visible
                        if (!mapVisibility[nextPointI, nextPointJ])
                            continue;
                        //Check if node was already visited
                        if(visited[nextPointI, nextPointJ])
                            continue;
                        double alternativeRoute = vertex.distance + 1;
                        if(alternativeRoute < distances[nextPointI, nextPointJ])
                        {
                            distances[nextPointI, nextPointJ] = alternativeRoute;
                            previous[nextPointI, nextPointJ] = new Point(vertex.indexI, vertex.indexJ);
                            vertexes.Add((nextPointI, nextPointJ, alternativeRoute));
                        }
                        if (nextPointI == endIndexI && nextPointJ == endIndexJ)
                            shortestPathWasFound = true;
                    }
                }
            }

            path = new List<Point>();
            Point nextPoint = new Point(endIndexI, endIndexJ);
            Point currentPoint = previous[endIndexI, endIndexJ];
            //Check if the algorithm found a viable path to the destination
            if (currentPoint.X == -1 && currentPoint.Y == -1)
                return startPosition;
            //Backtrack to the beginning of the path
            while(currentPoint.X != startIndexI || currentPoint.Y != startIndexJ)
            {
                path.Add(new Point((int)(currentPoint.Y * cellSize.Width + cellSize.Width / 2), (int)(currentPoint.X * cellSize.Height + cellSize.Height / 2)));
                nextPoint = currentPoint;
                currentPoint = previous[currentPoint.X, currentPoint.Y];
            }
            //the next destination of the path was found and is stored in nextPoint
            //Get the position as a vector
            Vector2 retVec = GetPositionInMap(nextPoint, cellSize);
            return retVec;
        }

        private (int indexI, int indexJ) GetPositionInGrid(Vector2 position, SizeF cellSize)
        {
            return ((int)(position.Y / cellSize.Height), (int)(position.X / cellSize.Width));
        }

        private Vector2 GetPositionInMap(Point position, SizeF cellSize)
        {
            return new Vector2(position.Y * cellSize.Width + cellSize.Width / 2,
                               position.X * cellSize.Height + cellSize.Height / 2);
        }
    }
}
