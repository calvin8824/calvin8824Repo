using BTAdventure.Models;
using BTAdventure.Services;
using BTAdventure.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTAdventure.UI.Controllers
{
    [AllowAnonymous]

    public class HomeController : Controller
    {
        private GameService gameSerivce;

        public HomeController(GameService gameService)
        {
            this.gameSerivce = gameService;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Start()
        {
            Player player = new Player();
            return View(player);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignInNew()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignInNew(Player player)
        {
            //gameSerivce.AddPlayer(player);
            return View("MainMenu"); //pass in whatever the VM is
        }

        [HttpGet]
        public ActionResult SignInExisting()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignInExisting(Player player)
        {
            PlayerGame vm = new PlayerGame();
            vm.Player = player;
            return View("MainMenu", vm);
        }

        [HttpGet]
        public ActionResult MainMenu(PlayerGame vm)
        {
            //get all games by player, add to vm
            return View(vm);
        }

        [HttpGet]
        public ActionResult LoadGame(int id)
        {
            PlayerGame vm = new PlayerGame();
            vm.Characters = gameSerivce.FindListOfPlayerCharactersByPlayerId(id);
            vm.Player = gameSerivce.FindPlayerById(id);

            return View(vm);
        }

        [HttpPost]
        public ActionResult LoadGame(PlayerGame vm, int id) //gameId or characterId
        {
            return View("Game", vm);
        }

        [HttpGet]
        public ActionResult Game(int id)//sceneId
        {
            //we'll have something like if not new game...

            GameSceneVM vm = new GameSceneVM();
            vm.Scene = gameSerivce.FindSceneById(id);
            vm.EventChoice = gameSerivce.FindEventChoiceById(id);


            //vm.Character = gameSerivce.FindListOfPlayerCharactersByPlayerId();
            return View(vm);
        }
    }
}