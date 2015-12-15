using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        /// All the positions already attacked, we don't want to attack those again ever (maybe in a more advanced AI we would).
        /// </summary>
        Position[,] positionsAttacked;

        //Our ships.
        Ships myShips;

        public Group2AIPlayer() { Name = "Group 2"; }
        public Group2AIPlayer(string name) { Name = name; }
       

        /// <summary>
        /// General algorithm:
        /// 1. Try to find a Position which reprents a likely continuation of enemy ships, from our OpponentData.
        /// 2. If no such Position exists, fire at a random Position.
        /// 3. Check if the position would hit our own ships, we will try another Position unless there is no other Position that has yet been attacked.
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
                valid = ValidatePosition(result);               
            }

            return result;
            
        }

        /// <summary>
        /// Check if there is any of our Ships at the passed Position.
        /// Will return true if there is nothing left to attack but our where our own Ships lie.
        /// </summary>
        /// <param name="potential">The Position to check.</param>
        /// <returns>Whether we should attack it.</returns>
        private bool ValidatePosition(Position potential)
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Whether there are any Positions that have not been attacked where our own Ships don't lie.</returns>
        private bool NothingLeftToAttackButOwnShips()
        {
            var shipPositions = myShips._ships.SelectMany(ship => ship.Positions);

            var allPositions = positionsAttacked.Cast<Position>();

            var unattackedOpenPositions = allPositions
                .Where(position => shipPositions
                .All(pos => !pos.EqualCoordinates(position)) && !position.Hit);

            return unattackedOpenPositions.Count() == 0;

          }

        /// <summary>
        /// Hunt mode.
        /// </summary>
        /// <returns>A random Position not yet attacked.</returns>
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
        /// This method should query the OpponentData's and determine a suitable Position.
        /// It should attack only Oponent's who have not been eliminated, and will favor opponent's with a higher AgressionLevel.
        /// </summary>
        /// <param name="toAttack">Set this Position reference to the position to attack.</param>
        /// <returns>Whether there was a position to attack or not.</returns>
        private bool NextTargetPosition(ref Position toAttack)
        {
            return false;
        }


        

        /// <summary>
        /// Sends an AttackResult to be processed by the corresponding OpponentData.
        /// Records all Positions that have already been attacked.
        /// </summary>
        /// <param name="results">The passed in results.</param>
        public void SetAttackResults(List<AttackResult> results)
        {
            foreach (var result in results)
            {
                int index = result.PlayerIndex;
                if (!opponentData.ContainsKey(index))
                {
                    opponentData.Add(index, new OpponentData(index, positionsAttacked.GetLength(0)));
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
            //This is so multiple instances will get different random numbers.
            Thread.Sleep(20);

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
