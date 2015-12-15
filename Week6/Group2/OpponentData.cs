﻿using System;
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
        internal int AgressionLevel { get; private set; }

        Stack<Position> positionsToAttack;
        List<Ship> shipsSunk;
        int gridSize;


        /// <summary>
        /// The number of times our ships have been hit by this opponent, lower values indicate player
        /// has been 'nice' to us.
        /// </summary>

        internal OpponentData(int index, int gridSize)
        {
            Index = index;
            this.gridSize = gridSize;
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
        /// This will add one Position on each horizontal and vertical side of the Position at most (it needs to check gridSize, don't go out of bounds).
        /// </summary>
        /// <param name="position">A position which was a hit on a ship.</param>
        private void ProcessHitPosition(Position position)
        {
            if (!position.Hit)
                throw new ArgumentException("This position was not hit.");

            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a sunken ship to the List of sunken ships.
        /// </summary>
        /// <param name="ship"></param>
        internal void AddShipSunk(Ship ship)
        {

        }

       
        /// <summary>
        /// Checks to see if there are Positions on the stack to attack and sets the parameter if so.
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
        /// <returns>Whether the BattleShip was sunk.</returns>
        internal bool IsEliminated()
        {
            throw new NotImplementedException();
        }
    }
}