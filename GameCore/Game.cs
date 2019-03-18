using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public class Game
    {
        public List<Player> Players;
        public Kingdom Kingdom;
        public List<Card> Trash;
        public Logger logger;

        public event EventHandler OnGameStart;

        public bool GameEnd { get; private set; }

        public Game (User[] users, Kingdom kingdom, Logger logger)
        {
            this.logger = logger;
            Kingdom = kingdom;
            Kingdom.Reset(users.Length == 2);
            Players = users.Select(u => new Player(this, u)).ToList();
            Trash = new List<Card>();
        }

        public void Run()
        {
            OnGameStart?.Invoke(this, null);
            Task.Run(() =>
            {
                logger.Log("New game has started.");

                // intitial drawing
                foreach (var player in Players)
                    player.Draw(5);

                // player index
                int i = 0;

                // one turn of one player
                while (true)
                {
                    logger.Log("\n");

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

                    // buy phase
                    do
                    {
                        card = Players[i].Buy();
                    } while (card != null);

                    // cleanup phase
                    Players[i].Cleanup();

                    // draw phase
                    Players[i].Draw(5);

                    GameEnd = isGameEnd();

                    if (GameEnd)
                    {
                        logger.Log("/n__results__");
                        // todo informace o endgame pro ai
                        foreach (Player player in Players.OrderBy(p => p.VictoryPoints))
                            logger.Log($"{player.Name} has {player.VictoryPoints}.");
                        return;
                    }

                    // next player
                    i = (i + 1) % Players.Count;
                }
            });   
        }

        // todo tohle asi vrarim zpet do playera...
        public Card Gain(CardType type)
        {
            return Kingdom.SingleOrDefault(p => p.Type == type)?.GainCard();
        }

        private bool isGameEnd()
        {  // todo pridat kolonie
            if (Kingdom.Single(k => k.Type == CardType.Province).Count == 0)
                return true;
            return Kingdom.Where(k => k.Count == 0).Count() >= 3;
        }
    }
}
