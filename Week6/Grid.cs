using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Week6
{
    public class Grid
    {
        private readonly GridEntry[,] _grid;
        private readonly int _gridSize;

        public Grid(int gridSize)
        {
            _gridSize = gridSize;
            _grid = new GridEntry[gridSize,gridSize];
            //Fill the grid with empty entries marked as not hit
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    _grid[x,y] = new GridEntry();
                }
            }
        }

        public void Add(Ships ships)
        {
            foreach (var ship in ships._ships)
            {
                if (ship.Positions == null)
                {
                    throw new ArgumentException("A player has not set the ships positions");
                }

                foreach (var pos in ship.Positions)
                {
                    if (pos.X< 0 || pos.X >_gridSize || pos.Y <0 || pos.Y >= _gridSize)
                    {
                        throw new ArgumentException("One of the ships is outside the grid");
                    }

                    if (pos.Hit)
                    {
                        throw new ArgumentException("One of the players is adding a hit ship to the game");
                    }

                    if (_grid[pos.X, pos.Y].Ship != null)
                    {
                        throw new ArgumentException("One of the players has an overlapping ship");
                    }

                    _grid[pos.X, pos.Y].Ship = ship;
                }
            }
        }

        public void Draw(int drawX, int drawY)
        {
            for (int x = 0; x < _gridSize; x++)
            {
                for (int y = 0; y < _gridSize; y++)
                {
                    Console.SetCursorPosition(drawX + x, drawY + y);
                    Console.ForegroundColor = (_grid[x, y].Ship == null) ? ConsoleColor.Gray : _grid[x, y].Ship.Color;
                    //Find if this segment of the ship is hit
                    Console.BackgroundColor = (_grid[x,y].Hit)? ConsoleColor.Red : ConsoleColor.Black;
                    if (_grid[x, y].Ship == null)
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        Console.Write(_grid[x, y].Ship.Character);
                    }
                }
            }

            //Reset colors
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Attack(Position pos)
        {
            _grid[pos.X, pos.Y].Hit = true;
        }
    }
}
