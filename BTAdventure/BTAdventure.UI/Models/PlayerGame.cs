using BTAdventure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTAdventure.UI.Models
{
    public class PlayerGame
    {
        public IEnumerable<Game> Games { get; set; }
        public Player Player { get; set; }
        public IEnumerable<PlayerCharacter> Characters { get; set; }
        public Game Game { get; set; }
        public Scene Scene { get; set; }
    }
}