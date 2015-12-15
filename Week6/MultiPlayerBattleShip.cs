using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace Week6
{
    internal class MultiPlayerBattleShip
    {
        const int GridSize = 10; //Your player should work when GridSize >=7

        private static readonly Random Random = new Random();

        private readonly List<IPlayer> _players;

        private List<Grid> _playerGrids;
        private List<Ships> _playerShips;
         
        private List<IPlayer> currentPlayers;


        public MultiPlayerBattleShip(List<IPlayer> players)
        {
            this._players = players;
        }

        internal void Play(PlayMode playMode)
        {
            currentPlayers = new List<IPlayer>();
            var availablePlayers = new List<IPlayer>(_players);
            _playerGrids = new List<Grid>();
            _playerShips = new List<Ships>();

            //Add each player in a random order
            for (int i = 0; i < _players.Count; i++)
            {
                var player = availablePlayers[Random.Next(availablePlayers.Count)];
                availablePlayers.Remove(player);
                currentPlayers.Add(player);
            }

            //Tell each player the game is about to start
            for (int i=0; i<currentPlayers.Count; i++)
            {
                var ships = new Ships();
                ships.Add(new AircraftCarrier());
                ships.Add(new Submarine());
                ships.Add(new Destroyer());
                ships.Add(new Destroyer());
                ships.Add(new PatrolBoat());
                ships.Add(new PatrolBoat());
                ships.Add(new PatrolBoat());
                ships.Add(new Battleship());

                var count = ships._ships.Count();
                int totalLength = ships._ships.Sum(ship => ship.Length);
                
                currentPlayers[i].StartNewGame(i, GridSize, ships);

                //Make sure player didn't change ships
                if (count != ships._ships.Count()
                    || totalLength != ships._ships.Sum(ship => ship.Length))
                {
                    throw new Exception("Ship collection has ships added or removed");
                    
                }

                var grid = new Grid(GridSize);
                grid.Add(ships);
                _playerGrids.Add(grid);
                _playerShips.Add(ships);
            }

            int currentPlayerIndex = 0;
            while (currentPlayers.Count > 1)
            {
                var currentPlayer = currentPlayers[currentPlayerIndex];

                //Ask the current player for their move
                Position pos = currentPlayer.GetAttackPosition();

                //Work out if anything was hit
                var results = CheckAttack(pos);

                //Notify each player of the results
                foreach (var player in currentPlayers)
                {
                    player.SetAttackResults(results);
                }



                DrawGrids();


                Console.WriteLine("\nPlayer " + currentPlayer.Index + "[" + currentPlayer.Name + "]  turn.");
                Console.WriteLine("    Attack: " + pos.X + "," + pos.Y);
                Console.WriteLine("\nResults:");
                foreach (var result in results)
                {
                    Console.Write("    Player " + result.PlayerIndex + " " + result.ResultType);
                    if (result.ResultType == AttackResultType.Sank)
                    {
                        Console.Write(" - " + result.SunkShip);
                    }
                    Console.WriteLine();
                }

                //Remove any ships with sunken battleships
                //Iterate backwards so that we don't mess with the indexes
                for (int i = currentPlayers.Count - 1; i >= 0; --i)
                {
                    var player = currentPlayers[i];
                    if (_playerShips[player.Index].SunkMyBattleShip)
                    {
                        currentPlayers.Remove(player);
                        //We never want to remvoe all the players... 
                        if (currentPlayers.Count == 1)
                        {
                            break;
                        }
                    }
                }

                //Move to next player wrapping around the end of the collection
                currentPlayerIndex = (currentPlayerIndex + 1)%currentPlayers.Count;

                

                if (playMode == PlayMode.Pause)
                {
                    Console.WriteLine("\nPress a key to continue");
                    Console.ReadKey(true);
                }
                else
                {
                    Thread.Sleep(2000);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Winner is '" + currentPlayers[0].Name + "'");
            Console.ReadKey(true);




        }

        private List<AttackResult> CheckAttack(Position pos)
        {
            var results = new List<AttackResult>();

            foreach (var player in currentPlayers)
            {
                var result = _playerShips[player.Index].Attack(pos);

                //Mark attacks on the grid
                foreach (var grid in _playerGrids)
                {
                    grid.Attack(pos);
                }

                result.PlayerIndex = player.Index;
                results.Add(result);
            }
            return results;
        }


        private void DrawGrids()
        {
            Console.Clear();
            int drawX = 0;
            int drawY = 0;

            for (int i=0; i < currentPlayers.Count; i++)
            {
                var player = currentPlayers[i];
                var playerIndex = player.Index;

                var grid = _playerGrids[playerIndex];
                Console.SetCursorPosition(drawX, drawY);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;

                Console.Write(player.Name);
                grid.Draw(drawX, drawY+1);


                drawX += GridSize + 4;
                if (drawX + GridSize > Console.BufferWidth)
                {
                    drawY += GridSize + 5;
                    drawX = 0;
                }
            }
        }

    }
}