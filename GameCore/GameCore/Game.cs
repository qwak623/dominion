using GameCore.Cards;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCore
{
    // todo udelat nejake optional kontroly tahu 
    public class Game
    {
        public const int maxRounds = 50;

        User[] users;
        public List<Player> Players;
        public Kingdom Kingdom;
        public List<Card> Trash;
        public ILogger Logger;

        public bool GameEnd { get; private set; }

        const int drawCount = 5;

        public Game (User[] users, Kingdom kingdom, ILogger logger = null)
        {
            this.Logger = logger;
            Kingdom = kingdom;
            this.users = users;Trash = new List<Card>();
        }

        public Task<GameResults> Play()
        {
            return Task.Run(() =>
            {
                try
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
                        Logger?.Log("Hand: " + Players[i].ps.Hand.Select(c => c.Name).Aggregate((a, b) => a + ", " + b));

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

                        Logger?.Log("Hand: " + Players[i].ps.Hand.Select(c => c.Name).Aggregate((a, b) => a + ", " + b));
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
                            Logger?.Log("\r\n\tResults:");
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
                            Logger?.Log("\r\nGame was terminated, number of rounds exceeded 50.");
                            return new GameResults
                            {
                                Players = Players,
                                Score = new List<int> { 0, 0 }
                            };
                        }
                    }

                }
                catch (System.Exception e) // TODO smazat ten try
                {
                    throw e;
                }
            });
        }

        // todo pridat kolonie
        private bool isGameEnd() => Kingdom.GetPile(CardType.Province).Empty || Kingdom.EmptyPiles >= 3;
    }
}
