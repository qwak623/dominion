using GameCore.Cards;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCore
{
    // todo hra by asi mela sama vice kontrolovat spravnost tahu
    // pravdepodobne to trochu zpomali, ale kdo vi co si pak ty inteligence budou delat jinak
    public class Game
    {
        public const int maxRounds = 50;

        User[] users;
        public List<Player> Players;
        public Kingdom Kingdom;
        public List<Card> Trash;
        public Logger Logger;

        public bool GameEnd { get; private set; }

        const int drawCount = 5;

        public Game (User[] users, Kingdom kingdom, Logger logger = null)
        {
            this.Logger = logger;
            Kingdom = kingdom;
            this.users = users;Trash = new List<Card>();
        }

        public Task<GameResults> Play()
        {
            return Task.Run(() =>
            {
                // random needs to be instantiated and used in the same thread
                var rnd = new ThreadSafeRandom();
                Players = users.Select(u => new Player(this, u, rnd)).ToList();

                Logger?.Log("New game has started.");

                // intitial drawing
                foreach (var player in Players)
                    player.Draw(drawCount);

                // player index
                int i = 0;
                int round = 0;

                // one turn of one player
                while (true)
                {
                    Logger?.Log("\n");

                    Players[i].ps.Buys = 1;
                    Players[i].ps.Actions = 1;
                    Players[i].ps.Coins = 0;

                    // action phase
                    Card card;
                    do
                    {
                        card = Players[i].PlayCard();
                    } while (card != null);

                    // treasure phase
                    do
                    {
                        card = Players[i].PlayTreasure();
                    } while (card != null);

                    Logger?.Log($"{Players[i].Name} has ${Players[i].ps.Coins}.");

                    // buy phase
                    do
                    {
                        card = Players[i].Buy();
                    } while (card != null);

                    // cleanup phase
                    Players[i].Cleanup();

                    // draw phase
                    Players[i].Draw(drawCount);

                    GameEnd = isGameEnd();

                    if (GameEnd)
                    {
                        Logger?.Log("\n\n__Results__");
                        foreach (Player player in Players.OrderBy(p => p.VictoryPoints))
                            Logger?.Log($"{player.Name} has {player.VictoryPoints}.");
                        return new GameResults
                        {
                            Players = Players,
                            Score = Players.Select(p => p.VictoryPoints).ToList()
                        };
                    }
                    // next player
                    i = (i + 1) % Players.Count;

                    // stopping too long games
                    if (i == 0)
                        round++;
                    if (round >= maxRounds)
                    {
                        Logger?.Log("\n\nGame was terminated, number of rounds exceeded 50.");
                        return new GameResults
                        {
                            Players = Players,
                            Score = new List<int> { 0, 0 }
                        };
                    }
                }
            });
        }

        

        private bool isGameEnd()
        {  // todo pridat kolonie
            if (Kingdom.GetPile(CardType.Province).Count == 0)
                return true;
            return Kingdom.Where(k => k.Count == 0).Count() >= 3;
        }
    }
}
