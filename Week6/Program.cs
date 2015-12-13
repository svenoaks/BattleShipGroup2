using Gsd311.Week6.Group2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week6
{
    class Program
    {
        static void Main(string[] args)
        {


            List<IPlayer> players = new List<IPlayer>();

            for (int i = 1; i <= 8; ++i)
                players.Add(new Group2AIPlayer(i.ToString()));
            
            MultiPlayerBattleShip game = new MultiPlayerBattleShip(players);
            game.Play(PlayMode.Pause);
        }
    }
}
