using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week6;

namespace Gsd311.Week6.Group2
{
    static class Extension
    {
        /// <summary>
        /// Use an extension method for this, so we don't edit Position and change the test harness setup.
        /// </summary>
        /// <param name="thisOne"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool EqualCoordinates(this Position thisOne, Position other)
        {
            return thisOne.X == other.X && thisOne.Y == other.Y;
        }

    }
    class Group2AIPlayer : IPlayer
    {
        public int Index { get; private set; }
        public string Name { get; }

        /// <summary>
        /// Information regarding each opponent. This is no guarantee about the order we will receive the opponents or the number
        /// so use a dictionary for direct access to values by index.
        /// </summary>
        Dictionary<int, OpponentData> opponentData;

        /// <summary>
        /// All the positions already attacked, we don't want to attack those again ever.
        /// </summary>
        Position[,] positionsAttacked;

        //Our ships.
        Ships myShips;

        public Group2AIPlayer() { Name = "Group 2"; }
        public Group2AIPlayer(string name) { Name = name; }
       

        /// <summary>
        /// General algorithm:
        /// 1. Iterate through opponent data for ships which have positions to be attacked and retrieve one.
        /// 2. Check if this position has been attacked already, and this opponent is not eliminated and keep going if so.
        /// 3. Check if the position would hit our own ships, we will probably ignore if it so.
        /// 4. If no opponents have positions to attack, generate a random position to attack that hasn't been attacked yet.
        /// 
        /// </summary>
        /// <returns>Our next attack.</returns>
        public Position GetAttackPosition()
        {
            Position result = null;
            bool valid = false;

            while(!valid)
            {
                if (NextTargetPosition(ref result))
                {
                    ;
                }
                else
                {
                    result = NextHuntPosition();
                }
                valid = CheckFriendly(result);               
            }

            return result;
            
        }

        /// <summary>
        /// We need to check if our ships are at the potential position.
        /// If our last potential position is the same as this one, we need to accept it regardless, otherwise
        /// we could be in an infinite loop.
        /// </summary>
        /// <param name="potenial">The Position to check.</param>
        /// <returns>Whether we should attack it.</returns>
        private bool CheckFriendly(Position potential)
        {
            if (NothingLeftToAttackButOwnShips())
            {
                return true;
            }   
            else
            {
                //Avoid our own ships.
                List<Ship> ships = myShips._ships;

                return ships
                    .All(ship => ship.Positions
                    .All(position => !position.EqualCoordinates(potential)));

            }
        }

        private bool NothingLeftToAttackButOwnShips()
        {
            List<Ship> ships = myShips._ships;

            bool nothingLeft = true;
            for (int i = 0; i < positionsAttacked.GetLength(0); ++i)
            {
                for (int j = 0; j < positionsAttacked.GetLength(1); ++j)
                {
                    Position thePosition = positionsAttacked[i, j];
                    if (thePosition.Hit)
                        continue;
                    bool onShip = false;
                    foreach (Ship ship in ships)
                    {
                        foreach(Position pos in ship.Positions)
                        {
                            if (pos.EqualCoordinates(thePosition))
                            {
                                onShip = true;
                                goto EndShip;
                            }

                        }
                    }
                    EndShip:
                    if (!onShip)
                    {
                        nothingLeft = false;
                        goto End;
                    }
                       
                }
            }
            End:
            return nothingLeft;
        }

        /// <summary>
        /// Hunt mode.
        /// </summary>
        /// <returns>The Nex</returns>
        private Position NextHuntPosition()
        {
            Random rnd = new Random();
            Position result = null;
            bool valid = false;
            while (!valid)
            {
                int x = rnd.Next(0, positionsAttacked.GetLength(0));
                int y = rnd.Next(0, positionsAttacked.GetLength(1));

                if (!positionsAttacked[x, y].Hit)
                {
                    valid = true;
                    result = new Position(x, y);
                }
                   
            }
            return result;
        }



        /// <summary>
        /// Target mode.
        /// </summary>
        /// <param name="toAttack">Set this position to the position to attack.</param>
        /// <returns>Whether there was a position to attack or not.</returns>
        private bool NextTargetPosition(ref Position toAttack)
        {
            return false;
        }


        

        /// <summary>
        /// General algorithm:
        /// Add the new positions to attack to our opponentData if there was a hit or sunk, and the positions to our
        /// positions attacked.
        /// </summary>
        /// <param name="results"></param>
        public void SetAttackResults(List<AttackResult> results)
        {
            foreach (var result in results)
            {
                int index = result.PlayerIndex;
                if (!opponentData.ContainsKey(index))
                {
                    opponentData.Add(index, new OpponentData(index));
                }
               
                opponentData[index].ProcessResult(result);
                positionsAttacked[result.Position.X, result.Position.Y].Hit = true;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerIndex"></param>
        /// <param name="gridSize"></param>
        /// <param name="ships"></param>
        public void StartNewGame(int playerIndex, int gridSize, Ships ships)
        {
            Index = playerIndex;
            myShips = ships;

            opponentData = new Dictionary<int, OpponentData>();
            InitPositionsAttacked(gridSize);
            PlaceShipsRandomly(gridSize);
        }

        private void InitPositionsAttacked(int gridSize)
        {
            positionsAttacked = new Position[gridSize, gridSize];
            for (int i = 0; i < gridSize; ++i)
            {
                for (int j = 0; j < gridSize; ++j)
                {
                    positionsAttacked[i, j] = new Position(i, j);
                }
            }
        }

       
        private void PlaceShipsRandomly(int gridSize)
        {
            var availableRows = new List<int>();
            for (int a = 0; a < gridSize; a++)
            {
                availableRows.Add(a);
            }
            var availableColumns = new List<int>();
            for (int b = 0; b < gridSize; b++)
            {
                availableColumns.Add(b);
            }

            Random rand = new Random();

            if (rand.NextDouble() >= 0.5)

                foreach (var ship in myShips._ships)
                {

                    var y = availableRows[rand.Next(availableRows.Count)];
                    availableRows.Remove(y);



                    var x = rand.Next(gridSize - ship.Length);
                    ship.Place(new Position(x, y), Direction.Horizontal);


                }
            else
                foreach (var ship in myShips._ships)
                {

                    var x = availableColumns[rand.Next(availableColumns.Count)];
                    availableColumns.Remove(x);


                    var y = rand.Next(gridSize - ship.Length);
                    ship.Place(new Position(x, y), Direction.Vertical);
                }
        }
        private void PlaceShipsDumbly()
        {
            int y = 0;
            foreach (var ship in myShips._ships)
            {
                ship.Place(new Position(0, y++), Direction.Horizontal);

            }
        }
    }
}
