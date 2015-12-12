using System;
using System.Collections.Generic;
using System.Linq;


namespace Week6
{
    public abstract class Ship
    {

        private Position[] _positions;
        public readonly int Length;
        public readonly ConsoleColor Color;
        public readonly ShipTypes ShipType;
        
        private static readonly char[] Characters =  {'?', 'P', 'S', 'D', 'A', 'B'};
        

        public virtual bool IsBattleShip => false;

        protected Ship(int length, ConsoleColor color, ShipTypes shipType)
        {
            Length = length;
            Color = color;
            ShipType = shipType;
        }

        public void Reset()
        {
            _positions = null;
        }

        public char Character => Characters[(int) ShipType];

        public Position[] Positions
        {
            get
            {
                if (_positions == null)
                {
                    return null;
                }
                //Return a copy since this is a readonly field
                var retval = new Position[_positions.Length];
                Array.Copy(_positions, retval, _positions.Length);
                return retval;
            }
        }

        public void Place(Position start, Direction direction)
        {
            _positions = new Position[Length];
            for (int i = 0; i < Length; i++)
            {
                _positions[i] = new Position(start.X, start.Y);
                if (direction == Direction.Horizontal) start.X++;
                if (direction == Direction.Vertical) start.Y++;
            }
        }


        public AttackResult Attack(Position pos)
        {
            foreach (var position in _positions)
            {
                if (position.X == pos.X && position.Y == pos.Y)
                {
                    position.Hit = true;
                    if (Sunk)
                    {
                        return new AttackResult(0, pos, AttackResultType.Sank, ShipType);
                    }
                    return new AttackResult(0, pos, AttackResultType.Hit);
                }
            }

            return new AttackResult(0, pos); //Miss
        }

        public bool Sunk
        {
            get
            {
                return _positions.All(position => position.Hit);
            }
        }
    }
}
