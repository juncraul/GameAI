using Mathematics;
using Population;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAI
{
    public class ApplicationEngine
    {
        private static ApplicationEngine _instance;

        private Graphics _graphics;
        private Bitmap _bitmap;

        List<BaseRobot> robots;
        bool[,] map;
        SizeF cellSize;

        private ApplicationEngine(Size worldCanvasSize)
        {
            _bitmap = new Bitmap(worldCanvasSize.Width, worldCanvasSize.Height);
            _graphics = Graphics.FromImage(_bitmap);
            map = new bool[ApplicationSettings.mapCells.Height, ApplicationSettings.mapCells.Width];
            cellSize = new SizeF((float)worldCanvasSize.Width / ApplicationSettings.mapCells.Width, (float)worldCanvasSize.Height / ApplicationSettings.mapCells.Height);
            robots = new List<BaseRobot>
            {
                new ExplorerRobot(new Vector2(20, 20), Color.Red)
            };
        }

        public static ApplicationEngine GetInstance(Size worldCanvasSize)
        {
            return _instance = _instance ?? new ApplicationEngine(worldCanvasSize);
        }

        public void DoLogic()
        {
            for (int i = 0; i < ApplicationSettings.mapCells.Height; i++)
            {
                for (int j = 0; j < ApplicationSettings.mapCells.Width; j++)
                {
                    map[i, j] = false;
                }
            }

            foreach (BaseRobot b in robots)
            {
                switch(b)
                {
                    case ExplorerRobot explorer:
                        explorer.DoLogic(map, cellSize, _bitmap.Size);
                        break;
                }
                b.Move();
            }
        }

        public Bitmap Draw()
        {
            SolidBrush brush = new SolidBrush(Color.White);
            _graphics.FillRectangle(brush, new Rectangle(0, 0, _bitmap.Width, _bitmap.Height));

            for(int i = 0; i < ApplicationSettings.mapCells.Height; i ++)
            {
                for(int j = 0; j < ApplicationSettings.mapCells.Width; j ++)
                {
                    brush.Color = map[i, j] ? Color.White : Color.Black;
                    _graphics.FillRectangle(brush, new RectangleF(j * cellSize.Width, i * cellSize.Height, cellSize.Width, cellSize.Height));
                }
            }

            foreach (BaseRobot b in robots)
            {
                b.Draw(_graphics, _bitmap);
            }
            return _bitmap;
        }
    }
}
