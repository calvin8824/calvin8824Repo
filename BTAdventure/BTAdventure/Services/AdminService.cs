using BTAdventure.Interfaces;
using BTAdventure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Services
{
    public class AdminService
    {
        private IPlayerCharacterRepository characterRepo;
        private IGameRepository gamerepo;
        private IEventChoiceRepository choiceRepo;
        private IOutcomeRepository outcomeRepo;
        private IPlayerRepository playerRepo;
        private ISceneRepository sceneRepo;
        public AdminService(IPlayerCharacterRepository characterRepo, IGameRepository gamerepo, IEventChoiceRepository choiceRepo,
            IOutcomeRepository outcomeRepo, IPlayerRepository playerRepo, ISceneRepository sceneRepo)
        {
            this.characterRepo = characterRepo;
            this.gamerepo = gamerepo;
            this.choiceRepo = choiceRepo;
            this.outcomeRepo = outcomeRepo;
            this.playerRepo = playerRepo;
            this.sceneRepo = sceneRepo;
        }

        public Scene CreateScene(Scene scene)
        {
            return sceneRepo.Save(scene);

        }

        public EventChoice CreateEventChoice(EventChoice eventChoice)
        {
            return choiceRepo.Save(eventChoice);
        }

        public Game CreateGame(Game game)
        {
            return gamerepo.Save(game);
        }

        public List<Game> GetAllGames()
        {
            return gamerepo.All().ToList();
        }

        public List<Scene> GetAllScenes()
        {
            return sceneRepo.All().ToList();
        }

        public List<EventChoice> GetAllEventChoice()
        {
            return choiceRepo.All().ToList();
        }
    }
}
