using BTAdventure.Interfaces;
using BTAdventure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Services
{
    public class GameService
    {
        private IPlayerCharacterRepository characterRepo;
        private IGameRepository gamerepo;
        private IEventChoiceRepository choiceRepo;
        private IOutcomeRepository outcomeRepo;
        private IPlayerRepository playerRepo;
        private ISceneRepository sceneRepo;
        public GameService(IPlayerCharacterRepository characterRepo, IGameRepository gamerepo, IEventChoiceRepository choiceRepo,
            IOutcomeRepository outcomeRepo, IPlayerRepository playerRepo, ISceneRepository sceneRepo)
        {
            this.characterRepo = characterRepo;
            this.gamerepo = gamerepo;
            this.choiceRepo = choiceRepo;
            this.outcomeRepo = outcomeRepo;
            this.playerRepo = playerRepo;
            this.sceneRepo = sceneRepo;
        }

        public Scene FindSceneById(int id)
        {
            return sceneRepo.FindById(id);
        }

        public Outcome FindOutcomeById(int id)
        {
            return outcomeRepo.FindById(id);
        }

        public EventChoice FindEventChoiceById(int id)
        {
            return choiceRepo.FindById(id);
        }
        public PlayerCharacter FindPlayerCharacterById(int id)
        {
            return characterRepo.FindById(id);
        }

        public Player FindPlayerById(int id)
        {
            return playerRepo.FindById(id);
        }

        public Game FindGameById(int id)
        {
            return gamerepo.FindById(id);
        }

        public Player SavePlayer(Player player)
        {
            var allPlayers = playerRepo.All();
            player.PlayerId = allPlayers.Any() ? allPlayers.Count() + 1 : 1;
            return playerRepo.Save(player);
        }

        public PlayerCharacter SaveCurrentPlayerCharacterGame(int playerId, int characterId, int sceneId, int eventChoiceId)
        {
            PlayerCharacter currentCharacter;
            var allPlayerCharacter = characterRepo.All();
            currentCharacter = allPlayerCharacter.First(p => p.CharacterId == characterId && p.PlayerId == playerId);
            currentCharacter.SceneId = sceneId;
            currentCharacter.EventChoiceId = eventChoiceId;
            //need to figure out how to handle the gold and health
            return characterRepo.Save(currentCharacter);
        }


        public Game NewGame(int playerId, int playerCharacterId, int sceneId)
        {
            var game = gamerepo.FindById(sceneId);

            return game;
        }

        public List<PlayerCharacter> FindListOfPlayerCharactersByPlayerId(int playerId)
        {
            var characters = new List<PlayerCharacter>();

            //sceneId and characterId is stored in player character table which determines all the game the player has
            characters = characterRepo.All().Where(c => c.PlayerId == playerId).ToList();

            return characters;
        }
    }
}
