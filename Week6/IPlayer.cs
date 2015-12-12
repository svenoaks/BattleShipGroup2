using System;
using System.Collections.Generic;

namespace Week6
{
    interface IPlayer
    {
        /// <summary>
        /// Initializes the players are the start of a game and returns the positions of the ships
        /// Note: This method should be used to reset any AI state. It will be called once per game and each session might be multiple games
        /// You may also use this to generate new data structures for this game. The Test harness will handle checking for hits based on your
        /// returned value so it is up to you if and how you want to store the representation of your own grid
        /// </summary>
        /// <param name="playerIndex">What is the index of this player for this game - may change each game</param>
        /// <param name="gridSize">Size of the square grid - may change each game</param>
        /// <param name="ships">A list of Ships to provide positions for - may change each game. You should populate this collection with positions</param>
        void StartNewGame(int playerIndex, int gridSize, Ships ships);

        /// <summary>
        /// The name of this player - displayed in the UI
        /// </summary>
        String Name { get; }

        /// <summary>
        /// The index of this player - it should return the index passed into the StartNewGame
        /// </summary>
        int Index { get; }

        /// <summary>
        /// This is where you put the AI that chooses which square to target
        /// </summary>
        /// <returns>A position with an x, y coordinate</returns>
        Position GetAttackPosition();

        /// <summary>
        /// The game will notify you of the results of each attack. 
        /// </summary>
        /// <param name="results">A collection for each player still in the game
        /// You will get the index, the attack position and the result of the attack</param>
        void SetAttackResults(List<AttackResult> results);
    }
}
