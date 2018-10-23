using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL;
using BattleShip.BLL.GameLogic;

namespace BattleShip.UI
{
    class Player
    {
        public Board Board { get; set; }
        public String Name { get; set; }
        public String FirstMate { get; set; }

        public Player()
        {
            Board = new Board();
        }
    }
}
