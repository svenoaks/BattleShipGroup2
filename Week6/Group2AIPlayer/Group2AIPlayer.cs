using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week6;

namespace Gsd311.Week6.Group2
{
    class Group2AIPlayer : IPlayer
    {
        public int Index { get; private set; }

        /// <summary>
        /// Information regarding each opponent.
        /// </summary>
        List<OpponentData> opponentData;

        /// <summary>
        /// All the positions already attacked, we don't want to attack those again ever.
        /// </summary>
        Position[,] positionsAttacked;

        //Our ships.
        Ships myShips;

        public string Name
        {
            get
            {
                return "Group 2 AI Player";
            }
        }

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
            throw new NotImplementedException();
        }

        /// <summary>
        /// General algorithm:
        /// Add the new positions to attack to our opponentData if there was a hit or sunk, and the positions to our
        /// positions attacked.
        /// </summary>
        /// <param name="results"></param>
        public void SetAttackResults(List<AttackResult> results)
        {
            throw new NotImplementedException();
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

            opponentData = new List<OpponentData>();
            positionsAttacked = new Position[gridSize, gridSize];

            //TODO
            //  Place the ships within myShips randomly.
        }
    }
}
