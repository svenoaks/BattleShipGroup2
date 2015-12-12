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

        internal OpponentData(int index)
        {
            Index = index;
            positionsToAttack = new Stack<Position>();
            List<Ship> shipsSunk = new List<Ship>();
        }

        /// <summary>
        /// Adds a position to attack to the stack.
        /// </summary>
        internal void AddPosition()
        {
            throw new NotImplementedException();
        }

        internal void AddShipSunk(Ship ship)
        {

        }

        /// <summary>
        /// Returns whether there are any positions left in stack to attack.
        /// </summary>
        /// <returns></returns>
        internal bool HasNextAttackPosition()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This will return the next position to attack by popping the stack. It will need to be checked for validity against other game
        /// conditions from the outside (whether the AI will be attacking it's own ship, whether the Position has already been
        /// attacked, etc.
        /// </summary>
        /// <returns></returns>
        internal Position NextAttackPosition()
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