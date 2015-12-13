using System;
using System.Collections.Generic;
using Week6;

namespace Gsd311.Week6.Group2
{
    internal class OpponentData
    {
        /// <summary>
        /// The index of the player from AttackResult.
        /// </summary>
        internal int Index { get; }

        Stack<Position> positionsToAttack;
        List<Ship> shipsSunk;


        /// <summary>
        /// The number of times our ships have been hit by this opponent, lower values indicate player
        /// has been 'nice' to us.
        /// </summary>
        int niceLevel;

        internal OpponentData(int index)
        {
            Index = index;
            positionsToAttack = new Stack<Position>();
            List<Ship> shipsSunk = new List<Ship>();
        }

        /// <summary>
        /// Processes a result pertaining to  this opponent.
        /// </summary>
        /// <param name="result">A result pertaining to this opponent</param>
        internal void ProcessResult(AttackResult result)
        {
            if (result.PlayerIndex != Index)
                throw new ArgumentException("Result does not apply to this opponent");

            
        }
        /// <summary>
        /// Adds positions to attack to the stack, based on the position passed.
        /// </summary>
        /// <param name="position">A position which was a hit.</param>
        private void ProcessHitPosition(Position position)
        {
            if (!position.Hit)
                throw new ArgumentException("This position was not hit.");
            throw new NotImplementedException();
        }

        internal void AddShipSunk(Ship ship)
        {

        }

       
        /// <summary>
        /// Checks to see if there are positions to attack and sets its parameter if so.
        /// </summary>
        /// <param name="position">The posiion to set</param>
        /// <returns>Whether the position was set.</returns>
        internal bool NextAttackPosition(ref Position position)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        ///  Returns whether the player has been eliminated (battleship has been sunk)
        /// </summary>
        /// <returns></returns>
        internal bool IsEliminated()
        {
            throw new NotImplementedException();
        }
    }
}