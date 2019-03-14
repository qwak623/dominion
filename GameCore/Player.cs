using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCore
{
    // todo asi sjednotit humanplayera a playera
    public class Player
    {
        public readonly string Name;
        public User user;
        public Game game;
        
        public PlayerState ps;

        // statistics
        public int VictoryPoints = 0;
        public int CardCount => ps.DrawPile.Count + ps.DiscardPile.Count + ps.Hand.Count + ps.PlayedCards.Count;

        public Player(Game game, User user)
        {
            this.Name = user.GetName();
            this.game = game;
            this.user = user;

            ps = new PlayerState()
            {
                Actions = 1,
                Buys = 1,
                Coins = 0
            };
           
            // gain copper
            for (int i = 0; i < 7; i++)
                ps.DrawPile.Add(game.Kingdom.Where(p => p.Type == CardType.Copper).Single().GainCard());
            
            // gain estate
            for (int i = 0; i < 3; i++)
                ps.DrawPile.Add(game.Kingdom.Where(p => p.Type == CardType.Estate).Single().GainCard());
        }

        /// <summary>
        ///     Null means end of action phase 
        ///     Allowed actions, draws, discarts etc. player handles by himself
        /// </summary>
        /// <returns> Played card </returns>
        public Card PlayCard()
        {
            // if player has no actions left he cant select an action card
            if (ps.Actions == 0)
                return null;

            // user selects card to play, card is removed from hand and addet to played cards
            var card = user.PlayCard(ps.Hand.Where(c => c.IsAction), ps, Phase.Action);
            if (card == null)
                return null;

            game.logger.Log($"{Name} plays '{card.Name}'.");

            ps.Hand.Remove(card);
            ps.PlayedCards.Add(card);
            ps.Actions--;

            card?.PlayEffect(this);

            if (card.IsAttack)
                foreach (var player in game.Players.Where(p => p != this))
                    player.DealAttack(card.Attack, this);

            return card;
        }

        public Card PlayTreasure()
        {
            var treasure = user.PlayCard(ps.Hand.Where(c => c.IsTreasure), ps, Phase.Treasure);
            if (treasure == null)
                return null;

            game.logger.Log($"{Name} plays '{treasure.Name}'.");

            ps.Hand.Remove(treasure);
            ps.PlayedCards.Add(treasure);
            ps.Coins += treasure.Coins;
            return treasure;
        }

        /// <summary>
        ///     Null means end of buy phase.
        ///     Allowed buys player counts by himself.
        ///     After last buy player does the cleanup.
        /// </summary>
        /// <param name="kingdom">
        ///     Gives possible buys. 
        /// </param> 
        /// <returns> Purchased card </returns>
        public Card Buy()
        {
            if (ps.Buys == 0)
                return null;

            // buy
            var card = user.PlayCard(game.Kingdom.Where(k => !k.Empty && k.Price <= ps.Coins).Select(k => k.Card), ps, Phase.Buy);
            if (card == null)
                return null;

            game.logger.Log($"{Name} pays ${card.Price}.");

            Gain(card.Type);
            ps.Buys--;
            ps.Coins -= card.Price;
            
            return card;
        }

        /// <summary>
        ///     Hand and all played and purchased cards are placed to discart pile
        /// </summary>
        public void Cleanup()
        {
            foreach (var card in ps.Hand)
                ps.DiscardPile.Add(card);
            ps.Hand.Clear();

            foreach (var card in ps.PlayedCards)
                ps.DiscardPile.Add(card);
            ps.PlayedCards.Clear();
        }

        /// <summary>
        ///     Draws cards from draw pile to hand.
        /// </summary>
        /// <param name="count"></param>
        public void Draw(int count)
        {
            game.logger.Log($"{Name} draws {count} cards.");

            for (; count > 0; count--)
            {
                // if drawPile is empty, we need to shuffle discard pile and place it instead of drawPile
                if (ps.DrawPile.Count == 0)
                {
                    // there are no cards to draw
                    if (ps.DiscardPile.Count == 0)
                        break;

                    // swap
                    var pile = ps.DrawPile;
                    ps.DrawPile = ps.DiscardPile;
                    ps.DiscardPile = pile;

                    // shuffle
                    ps.DrawPile.Shuffle();
                }

                // draw one card
                ps.Hand.Add(ps.DrawPile[ps.DrawPile.Count - 1]);
                ps.DrawPile.RemoveAt(ps.DrawPile.Count - 1);
            }
        }

        public void Trash(Card card)
        {
            game.logger.Log($"{Name} trashes '{card.Name}'.");
            ps.Hand.Remove(card);
            game.Trash.Add(card);
        }

        public void Discard(Card card)
        {
            game.logger.Log($"{Name} discards '{card.Name}'.");
            ps.Hand.Remove(card);
            ps.DiscardPile.Add(card);
        }

        public void Gain(CardType type)
        {
            var card = game.Gain(type);
            if (card == null)
                return;
            game.logger.Log($"{Name} gains '{card.Name}'.");
            ps.PlayedCards.Add(card);
        }

        public void GainToHand(CardType type)
        {
            var card = game.Gain(type);
            if (card == null)
                return;
            game.logger.Log($"{Name} gains '{card.Name}' to hand.");
            ps.Hand.Add(card);
        }

        public void GainToDrawPile(CardType type)
        {
            var card = game.Gain(type);
            if (card == null)
                return;
            game.logger.Log($"{Name} gains '{card.Name}' up to draw pile.");
            ps.DrawPile.Add(card);
        }

        public void ReturnToDrawPile(Card card)
        {
            game.logger.Log($"{Name} returns '{card.Name}' up to draw pile.");
            ps.Hand.Remove(card);
            ps.DrawPile.Add(card);
        }

        public void DiscardDrawPile()
        {
            game.logger.Log($"{Name} discards draw pile.");
            ps.DiscardPile.AddRange(ps.DrawPile);
            ps.DrawPile.Clear();
        }

        public List<Card> Show(int count)
        {
            game.logger.Log($"{Name} shows {count} cards.");

            var list = new List<Card>(count);

            for (int i = ps.DrawPile.Count - 1; i >= ps.DrawPile.Count - count || i >= 0; i--)
            {
                // if drawPile is empty, we need to shuffle discard pile and place it instead of drawPile
                if (ps.DrawPile.Count == 0)
                {
                    // there are no cards to draw
                    if (ps.DiscardPile.Count == 0)
                        break;

                    // swap
                    var pile = ps.DrawPile;
                    ps.DrawPile = ps.DiscardPile;
                    ps.DiscardPile = pile;

                    // shuffle
                    ps.DrawPile.Shuffle();
                }

                // showOneCard
                list.Add(ps.DrawPile[ps.DrawPile.Count - 1]);
            }
            return list;
        }

        public void DealAttack(Action<Player, Player> attack, Player attacker)
        {
            game.logger.Log($"{Name} deals attack.");

            var selectedCard = user.Choose(ps.Hand.Where(c => c.IsReaction), ps, 0, ps.Hand.Count); // todo mozna tady dat reaction.count
            // todo vyresit co s namestim, to bude prdel

            // call all reactionEffects and returns true if any of them returns true
            if (!selectedCard.All(c => !c.ReactionEffect(this)))
                attack(this, attacker);
        }
    }
}
