using BTAdventure.Models;
using BTAdventure.Services;
using BTAdventure.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BTAdventure.UI.Controllers
{
    public class GameWebApiController : ApiController
    {
        private GameService gameService;
        public GameWebApiController(GameService gameService)
        {
            this.gameService = gameService;
        }
        [Route("api/Game")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetGameText()
        {

            var text = new TextAndChocie()
            {
                Text = "The is the story",
                BtnChoice1 = "Choice 1",
                BtnChoice2 = "Choice 2"
            };
            return Ok(text);
        }

        [Route("api/choice/{eventChoiceId}/{route}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult UpdateGameScene(int eventChoiceId, int route)
        {
            //will need to think about where to save player character, possibly pass in player id
            var currentEventChoice = gameService.FindEventChoiceById(eventChoiceId);
            EventChoice NextEventChoice = null;
            if (currentEventChoice.PositiveRoute > 0 && currentEventChoice.NegativeRoute > 0)
            {
                if (currentEventChoice.PositiveRoute > 0)
                {
                    NextEventChoice = gameService.FindEventChoiceById(route);
                }
                if (currentEventChoice.NegativeRoute > 0)
                {
                    NextEventChoice = gameService.FindEventChoiceById(route);
                }
            }

            if (currentEventChoice.PositiveSceneRoute == route || currentEventChoice.NegativeSceneRoute == route)
            {
                var newScene = new NewScene();
                newScene.Scene = gameService.FindSceneById(currentEventChoice.SceneId + 1);
                //newScene.EventChoice = gameService.FindEventChoiceById(newScene.Scene.);
            }
            return Ok(NextEventChoice);
        }
    }


    public class TextAndChocie
    {
        public string Text { get; set; }
        public string BtnChoice1 { get; set; }
        public string BtnChoice2 { get; set; }
    }
}