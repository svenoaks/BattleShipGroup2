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
        internal int AgressionLevel { get; private set; }

        Stack<Position> positionsToAttack;
        List<ShipTypes> shipsSunk;
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
            shipsSunk = new List<ShipTypes>();
        }

        /// <summary>
        /// Processes a result pertaining to  this opponent.
        /// </summary>
        /// <param name="result">A result pertaining to this opponent</param>
        internal void ProcessResult(AttackResult result)
        {
            if (result.PlayerIndex != Index)
                throw new ArgumentException("Result does not apply to this opponent");

            if (result.ResultType == AttackResultType.Hit)
            {
                ProcessHitPosition(result.Position);
            }

            if (result.SunkShip != ShipTypes.None)
            {
                AddShipSunk(result.SunkShip);
            }



        }
        /// <summary>
        /// Adds positions to attack to the stack, based on the position passed.
        /// This will add one Position on each horizontal and vertical side of the Position at most (it needs to check gridSize, don't go out of bounds).
        /// </summary>
        /// <param name="position">A position which was a hit on a ship.</param>
        private void ProcessHitPosition(Position position)
        {
            Position temp = new Position(0, 0);

            if (((position.X + 1) >= 0) && ((position.X + 1) < gridSize) && ((position.Y) >= 0) && ((position.Y) < gridSize))
            {
                temp.X = position.X + 1;
                temp.Y = position.Y;
                positionsToAttack.Push(temp);

            }

            if (((position.X - 1) >= 0) && ((position.X - 1) < gridSize) && ((position.Y) >= 0) && ((position.Y) < gridSize))
            {
                temp.X = position.X - 1;
                temp.Y = position.Y;
                positionsToAttack.Push(temp);

            }

            if (((position.X) >= 0) && ((position.X) < gridSize) && ((position.Y + 1) >= 0) && ((position.Y + 1) < gridSize))
            {
                temp.X = position.X;
                temp.Y = position.Y + 1;
                positionsToAttack.Push(temp);

            }

            if (((position.X) >= 0) && ((position.X) < gridSize) && ((position.Y - 1) >= 0) && ((position.Y - 1) < gridSize))
            {
                temp.X = position.X;
                temp.Y = position.Y - 1;
                positionsToAttack.Push(temp);

            }

        }

        /// <summary>
        /// Adds a sunken ship to the List of sunken ships.
        /// </summary>
        /// <param name="ship"></param>
        private void AddShipSunk(ShipTypes ship)
        {
            shipsSunk.Add(ship);
        }


        /// <summary>
        /// Checks to see if there are Positions on the stack to attack and sets the parameter if so.
        /// </summary>
        /// <param name="position">The posiion to set</param>
        /// <returns>Whether the position was set.</returns>
        internal bool NextAttackPosition(ref Position position)
        {
            if (positionsToAttack.Count == 0)
            {
                return false;
            }
            else
            {
                position = positionsToAttack.Pop();
                return true;
            }
        }


        /// <summary>
        ///  Returns whether the player has been eliminated (battleship has been sunk)
        /// </summary>
        /// <returns>Whether the BattleShip was sunk.</returns>
        internal bool IsEliminated()
        {
            //throw new NotImplementedException();
            bool temp = false;

            foreach (ShipTypes shipType in shipsSunk)
            {
                if (shipType == ShipTypes.Battleship)
                {
                    temp = true;
                    break;
                }
            }

            return temp;

        }
    }
}
