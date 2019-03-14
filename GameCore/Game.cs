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
        public List<Cards.Pile> Kingdom;
        public List<Card> Trash;
        public Logger logger;
        bool endOfTheGame = false;

        public Game (User[] users, List<Cards.Pile> kingdom, Logger logger)
        {
            this.logger = logger;
            Kingdom = kingdom;
            Players = users.Select(u => new Player(this, u)).ToList();
            Trash = new List<Card>();
        }

        public void Run()
        {
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

                    // next player
                    i = (i + 1) % Players.Count;
                }
            });   
        }

        // todo vyresit konec taky
        public Card Gain(CardType type)
        {
            var pile = Kingdom.SingleOrDefault(p => p.Type == type);
            if (pile == null)
                return null;
            var card = pile.GainCard();

            //if (pile.Count == 0)  TODO
            //{
            //    if (card.Type == CardType.Province)
            //        endOfTheGame = true;
            //    if (card.IsAction)
            //        emptyPiles++;
            //    if (emptyPiles >= 3)
            //        endOfTheGame = true;
            //}
            return card;
        }
    }
}
