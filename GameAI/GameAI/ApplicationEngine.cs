﻿using Mathematics;
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
        List<BaseItem> items;
        List<BaseBuilding> buildings;
        bool[,] mapVisibility;
        SizeF cellSize;
        
        private ApplicationEngine(Size worldCanvasSize)
        {
            _bitmap = new Bitmap(worldCanvasSize.Width, worldCanvasSize.Height);
            _graphics = Graphics.FromImage(_bitmap);
            mapVisibility = new bool[ApplicationSettings.MapVisibilityCells.Height, ApplicationSettings.MapVisibilityCells.Width];
            cellSize = new SizeF((float)worldCanvasSize.Width / ApplicationSettings.MapVisibilityCells.Width, (float)worldCanvasSize.Height / ApplicationSettings.MapVisibilityCells.Height);
            robots = new List<BaseRobot>
            {
                new RobotExplorer(new Vector2(20, 20), ApplicationSettings.Random),
                new RobotMiner(new Vector2(20, 20), ApplicationSettings.Random),
                new RobotTransporter(new Vector2(20, 20), ApplicationSettings.Random),
            };
            items = new List<BaseItem>();

            buildings = new List<BaseBuilding>
            {
                new BuildingHQ(new Vector2(20, 20))
            };

            for(int i = 0; i < 10; i ++)
            {
                items.Add(new ItemFixed(new Vector2(ApplicationSettings.Random.NextDouble() * (worldCanvasSize.Width - 100) + 50, ApplicationSettings.Random.NextDouble() * (worldCanvasSize.Height - 100) + 50), 100));
            }
        }

        public static ApplicationEngine GetInstance(Size worldCanvasSize)
        {
            return _instance = _instance ?? new ApplicationEngine(worldCanvasSize);
        }

        public void DoLogic()
        {
            foreach (BaseRobot b in robots)
            {
                switch(b)
                {
                    case RobotExplorer explorer:
                        //Do explorer logic, while passing the map to it
                        explorer.DoLogic(cellSize, mapVisibility, _bitmap.Size);
                        //Move the explorer in straight line
                        explorer.Move();
                        break;
                    case RobotMiner miner:
                        //Do miner logic, while passing the list of fixed items which are visible
                        ItemMovable item = miner.DoLogic(items.Where(a => (a as ItemFixed) != null && a.IsVisible).Select(a=> a as ItemFixed).ToList());
                        if (item != null)
                            items.Add(item);
                        //Move the miner on a grid
                        miner.Move(cellSize, mapVisibility);
                        break;
                    case RobotTransporter transporter:
                        //Do transporter logic, while passing the list of fixed items which are visible
                        transporter.DoLogic(items.Where(a => (a as ItemMovable) != null).Select(a => a as ItemMovable).ToList(), buildings[0] as BuildingHQ);
                        //Move the transporter on a grid
                        transporter.Move(cellSize, mapVisibility);
                        break;
                }
            }

            //Update the visibility property
            foreach(BaseItem b in items.Where(i => !i.IsVisible && (i as ItemFixed) != null))
            {
                b.UpdateVisibility(mapVisibility, cellSize);
            }

            items.RemoveAll(a => !a.IsAlive);
        }

        public Bitmap Draw()
        {
            SolidBrush brush = new SolidBrush(Color.White);
            _graphics.FillRectangle(brush, new Rectangle(0, 0, _bitmap.Width, _bitmap.Height));

            for (int i = 0; i < ApplicationSettings.MapVisibilityCells.Height; i++)
            {
                for (int j = 0; j < ApplicationSettings.MapVisibilityCells.Width; j++)
                {
                    if (!mapVisibility[i, j]) continue;
                    brush.Color = Color.White;
                    _graphics.FillRectangle(brush, new RectangleF(j * cellSize.Width, i * cellSize.Height, cellSize.Width, cellSize.Height));
                }
            }
            
            foreach (BaseItem b in items)
            {
                b.Draw(_graphics);
            }

            for (int i = 0; i < ApplicationSettings.MapVisibilityCells.Height; i ++)
            {
                for(int j = 0; j < ApplicationSettings.MapVisibilityCells.Width; j ++)
                {
                    if (mapVisibility[i, j]) continue;
                    brush.Color = Color.Black;
                    _graphics.FillRectangle(brush, new RectangleF(j * cellSize.Width, i * cellSize.Height, cellSize.Width, cellSize.Height));
                }
            }

            foreach (BaseRobot b in robots)
            {
                b.Draw(_graphics);
            }

            foreach (BaseBuilding b in buildings)
            {
                b.Draw(_graphics);
            }
            return _bitmap;
        }

        public void AddExplorer()
        {
            robots.Add(new RobotExplorer(new Vector2(20, 20), ApplicationSettings.Random));
            robots.Last().ShowPath = robots.First().ShowPath;
        }

        public void AddMiner()
        {
            robots.Add(new RobotMiner(new Vector2(20, 20), ApplicationSettings.Random));
            robots.Last().ShowPath = robots.First().ShowPath;
        }

        public void AddTransporter()
        {
            robots.Add(new RobotTransporter(new Vector2(20, 20), ApplicationSettings.Random));
            robots.Last().ShowPath = robots.First().ShowPath;
        }

        public void ShowHideRobotPath(bool show)
        {
            foreach(BaseRobot r in robots)
            {
                r.ShowPath = show;
            }
        }
    }
}
