using System;

namespace Week6
{
    class Battleship : Ship
    {
        public Battleship() : base(4, ConsoleColor.DarkGreen, ShipTypes.Battleship)
        {

        }

        public override bool IsBattleShip => true;
    }
}
