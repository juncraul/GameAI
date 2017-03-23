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
        private enum RobotMinerState{
            Idle,
            GoToMiningSite,
            Mine
        }

        private RobotMinerState _state;
        private ItemFixed _itemToMine;
        private int _amountToMinePerCycle;
        private int _cycleTickIteration;
        private int _cycleMaxTick;

        public RobotMiner(Vector2 position, Random random) : base(position, random)
        {
            Color = Color.Orchid;
            Size = new Size(20, 20);
            _state = RobotMinerState.GoToMiningSite;
            _amountToMinePerCycle = 10;
            _cycleMaxTick = 20;
        }

        public ItemMovable DoLogic(List<ItemFixed> fixedItems)
        {
            ItemMovable item = null;

            switch(_state)
            {
                case RobotMinerState.Idle:
                    break;
                case RobotMinerState.GoToMiningSite:
                    if (WhereToGo == null)
                    {
                        if (fixedItems.Count > 0)
                        {
                            double minDistance = 100000;
                            double distance;
                            ItemFixed goForThisItem = fixedItems[0];
                            foreach (ItemFixed i in fixedItems)
                            {
                                distance = Functions.DistanceBetweenTwoPoints(Position, i.Position);
                                if (distance < minDistance)
                                {
                                    goForThisItem = i;
                                    minDistance = distance;
                                }
                            }
                            WhereToGo = goForThisItem.Position;
                        }
                    }
                    else
                    {
                        if(Functions.DistanceBetweenTwoPoints(Position, WhereToGo) < destinationRange)
                        {
                            WhereToGo = null;
                            _state = RobotMinerState.Mine;
                        }
                    }
                    break;
                case RobotMinerState.Mine:
                    if(_itemToMine == null)
                    {
                        foreach (ItemFixed i in fixedItems)
                        {
                            if (Functions.DistanceBetweenTwoPoints(Position, i.Position) < destinationRange)
                            {
                                _itemToMine = i;
                                break;
                            }
                        }
                        if(_itemToMine == null)
                        {
                            _state = RobotMinerState.GoToMiningSite;
                        }
                    }
                    else
                    {
                        _cycleTickIteration++;
                        if(_cycleTickIteration == _cycleMaxTick)
                        {
                            _cycleTickIteration = 0;
                            item = _itemToMine.Mine(_amountToMinePerCycle);
                            if (!_itemToMine.IsAlive)
                                _itemToMine = null;
                        }
                    }
                    break;
            }

            return item;
        }
    }
}
