using System;
using System.Collections.Generic;


namespace Week6
{ 
    public class Ships
    {
        public readonly List<Ship> _ships = new List<Ship>();

        public void Clear()
        {
            _ships.Clear();
        }

        public bool SunkMyBattleShip
        {
            get
            {
                //Find the battleship and see if its sunk
                foreach (var ship in _ships)
                {
                    var battleShip = ship as Battleship;
                    if (battleShip != null)
                    {
                        return battleShip.Sunk;
                    }

                }

                throw new Exception("Cannot find a battleship");

            }
        }

        public void Add(Ship ship)
        {
            _ships.Add(ship);
        }

        public AttackResult Attack(Position pos)
        {
            //Search the positions for a hit 
            foreach (var ship in _ships)
            {
                AttackResult attackResult = ship.Attack(pos);
                if (attackResult.ResultType != AttackResultType.Miss)
                {
                    return attackResult; //Once we hit then no point looking any more
                }

            }

            //No hits means a miss!
            return new AttackResult(0, pos); 

        }


}
}
