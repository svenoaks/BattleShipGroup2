using System;
using System.Collections.Generic;

namespace Week6
{
    internal class RandomPlayer : IPlayer
    {
        private static readonly List<Position> Guesses = new List<Position>();
        private int _index;
        private static readonly Random Random = new Random();
        private int _gridSize;

        public RandomPlayer(string name)
        {
            Name = name;
        }

        public void StartNewGame(int playerIndex, int gridSize, Ships ships)
        {
            _gridSize = gridSize;
            _index = playerIndex;

            GenerateGuesses();

            //Random player just puts the ships in the grid in Random columns
            //Note it cannot deal with the case where there's not enough columns
            //for 1 per ship
            var availableColumns = new List<int>();
            for (int i = 0; i < gridSize; i++)
            {
                availableColumns.Add(i);
            }

            foreach (var ship in ships._ships)
            {
                //Choose an X from the set of remaining columns
                var x = availableColumns[Random.Next(availableColumns.Count)];
                availableColumns.Remove(x); //Make sure we can't pick it again

                //Choose a Y based o nthe ship length and grid size so it always fits
                var y = Random.Next(gridSize - ship.Length);
                ship.Place(new Position(x, y), Direction.Vertical);
            }
        }

        private void GenerateGuesses()
        {
            //We want all instances of RandomPlayer to share the same pool of guesses
            //So they don't repeat each other.

            //We need to populate the guesses list, but not for every instance - so we only do it if the set is missing some guesses
            if (Guesses.Count < _gridSize*_gridSize)
            {
                Guesses.Clear();
                for (int x = 0; x < _gridSize; x++)
                {
                    for (int y = 0; y < _gridSize; y++)
                    {
                        Guesses.Add(new Position(x,y));
                    }
                }
            }
        }

        public string Name { get; }
        public int Index => _index;

        public Position GetAttackPosition()
        {
            //RandomPlayer just guesses random squares. Its smart in that it never repeats a move from any other random 
            //player since they share the same set of guesses
            //But it doesn't take into account any other players guesses
            var guess = Guesses[Random.Next(Guesses.Count)];
            Guesses.Remove(guess); //Don't use this one again
            return guess;
        }

        public void SetAttackResults(List<AttackResult> results)
        {
            //Random player does nothing useful with these results, just keeps on making random guesses
        }
    }
}