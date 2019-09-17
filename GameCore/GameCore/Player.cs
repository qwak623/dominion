﻿using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace GameCore
{
    public class Player
    {
        public readonly string Name;
        public User User;
        public Game Game;
        ThreadSafeRandom rnd;
        
        public PlayerState ps;

        public int CardCount => ps.DrawPile.Count + ps.DiscardPile.Count + ps.Hand.Count + ps.PlayedCards.Count;

        private int? victoryPoints;

        /// <summary>
        /// Returns earned victory points. Working properly only at the end of the game.
        /// </summary>
        public int VictoryPoints
        {
            get
            {   // points can be counted only at the end of the game
                if (Game.GameEnd && victoryPoints == null)
                {
                    // it will be better to have all cards in discard pile before counting
                    Cleanup();
                    DiscardDrawPile();
                    victoryPoints = ps.DiscardPile.Select(c => c.CountPoints(this)).Sum();
                }
                return victoryPoints.GetValueOrDefault();
            }
        }

        public Player(Game game, User user, ThreadSafeRandom rnd)
        {
            this.Name = user.GetName();
            this.Game = game;
            this.User = user;
            this.rnd = rnd;

            ps = new PlayerState()
            {
                Actions = 1,
                Buys = 1,
                Coins = 0,
                Name = user.GetName(),
            };


            // gain estate
            for (int i = 0; i < 2; i++)
                ps.DrawPile.Add(Cards.GeneralCards.Estate.Get());

            // gain copper
            for (int i = 0; i < 7; i++)
                ps.DrawPile.Add(Cards.GeneralCards.Copper.Get());

            // gain estate
            for (int i = 0; i < 1; i++)
                ps.DrawPile.Add(Cards.GeneralCards.Estate.Get());
        }

        /// <summary>
        ///     Null pointer indicates that player cand or doesnt want to play any action card.
        ///     Player asks user on every decision (card selection, card related decision etc.)
        ///     when play actions and attack acitons are executed here.
        /// </summary>
        /// <returns> Played card </returns>
        public Card PlayCard()
        {
            // if player has no actions left or he doesnt have any action cards, he cant select an action card
            if (ps.Actions == 0 || ps.Hand.All(c => !c.IsAction))
                return null;

            // user selects card to play, card is removed from hand and added to played cards
            var card = User.PlayCard(ps.Hand.Where(c => c.IsAction), ps, Game.Kingdom, Phase.Action);
            if (card == null)
                return null;

            Game.Logger?.Log($"{Name} plays {card.Name}.");

            ps.Hand.Remove(card);
            ps.PlayedCards.Add(card);
            ps.Actions--;

            card.WhenPlayAction(this);

            if (card.IsAttack)
                foreach (var player in Game.Players.Where(p => p != this))
                    player.DealAttack(this, card);

            return card;
        }

        /// <summary>
        /// All treasure cards are automaticly played. 
        /// </summary>
        public void PlayTreasure()
        {
            foreach (var card in ps.Hand.Where(c => c.IsTreasure))
                card.WhenPlayTreasure(this);
        }

        /// <summary>
        ///     Null means end of buy phase.
        ///     Allowed buys player counts by himself.
        /// </summary>
        /// <returns> Purchased card </returns>
        public Card Buy()
        {
            if (ps.Buys == 0)
                return null;

            // buy
            var card = User.SelectCardToGain(Game.Kingdom.GetWrapper(ps.Coins), ps, Game.Kingdom, Phase.Buy);
            if (card == null)
                return null;

            Game.Logger?.Log($"{Name} pays ${card.Price}.");

            Gain(card.Type);
            ps.Buys--;
            ps.Coins -= card.Price;

            return card;
        }

        /// <summary>
        ///     Hand and all played and purchased cards are placed to discard pile.
        /// </summary>
        public void Cleanup()
        {
            ps.Hand.ForEach(card => ps.DiscardPile.Add(card));
            ps.Hand.Clear();
            ps.PlayedCards.ForEach(card => ps.DiscardPile.Add(card));
            ps.PlayedCards.Clear();
        }

        /// <summary>
        ///     Draws cards from draw pile to hand. If there are no cards 
        ///     shuffles the discard pile and places it onto draw pile place.
        ///     Then draws cards.
        /// </summary>
        /// <param name="count"></param>
        public void Draw(int count)
        {
            for (; count > 0; count--)
            {
                // if drawPile is empty, we need to shuffle discard pile and place it instead of drawPile
                if (ps.DrawPile.Count == 0)
                {
                    // there are no cards to draw
                    if (ps.DiscardPile.Count == 0)
                    {
                        Game.Logger?.Log($"{Name} doesn't have any cards left.");
                        break;
                    }

                    Game.Logger?.Log($"{Name} shuffles the pile.");

                    // swap
                    var pile = ps.DrawPile;
                    ps.DrawPile = ps.DiscardPile;
                    ps.DiscardPile = pile;

                    // shuffle
                    ps.DrawPile.Shuffle(rnd);
                }

                // draw one card
                var card = ps.DrawPile[ps.DrawPile.Count - 1];
                Game.Logger?.Log($"{Name} draws {card.Name}");
                ps.Hand.Add(card);
                ps.DrawPile.RemoveAt(ps.DrawPile.Count - 1);
            }
        }

        /// <summary>
        /// Takes specified card from hand and adds it to common trash list.
        /// </summary>
        /// <param name="card"></param>
        public void Trash(Card card)
        {
            Game.Logger?.Log($"{Name} trashes {card.Name}.");
            ps.Hand.Remove(card);
            Game.Trash.Add(card);
        }

        /// <summary>
        /// Takes specified card from hand and adds it to discard pile.
        /// </summary>
        /// <param name="card"></param>
        public void Discard(Card card)
        {
            Game.Logger?.Log($"{Name} discards {card.Name}.");
            ps.Hand.Remove(card);
            ps.DiscardPile.Add(card);
        }

        /// <summary>
        /// Gains card to discard pile, if it is possible.
        /// </summary>
        /// <param name="type"></param>
        public void Gain(CardType type)
        {
            var card = gainCard(type);
            if (card == null)
                return;
            Game.Logger?.Log($"{Name} gains {card.Name}.");
            ps.PlayedCards.Add(card);
        }

        /// <summary>
        /// Gains card to hand, if it is possible.
        /// </summary>
        /// <param name="type"></param>
        public void GainToHand(CardType type)
        {
            var card = gainCard(type);
            if (card == null)
                return;
            Game.Logger?.Log($"{Name} gains {card.Name} to hand.");
            ps.Hand.Add(card);
        }

        /// <summary>
        /// Gains card up to draw pile, if it is possible.
        /// </summary>
        /// <param name="type"></param>
        public void GainToDrawPile(CardType type)
        {
            var card = gainCard(type);
            if (card == null)
                return;
            Game.Logger?.Log($"{Name} gains {card.Name} up to draw pile.");
            ps.DrawPile.Add(card);
        }

        private Card gainCard(CardType type)
        {
            var pile = Game.Kingdom.GetPile(type);

            // counts empty piles without enumerating 
            if (pile.Count == 1)
                Game.Kingdom.EmptyPiles++;

            return pile.GainCard();
        }

        /// <summary>
        /// Returns card from hand up to draw pile.
        /// </summary>
        /// <param name="card"></param>
        public void ReturnToDrawPile(Card card)
        {
            Game.Logger?.Log($"{Name} returns {card.Name} up to draw pile.");
            ps.Hand.Remove(card);
            ps.DrawPile.Add(card);
        }

        /// <summary>
        /// Discards all cards in draw pile.
        /// </summary>
        public void DiscardDrawPile()
        {
            Game.Logger?.Log($"{Name} discards draw pile.");
            ps.DiscardPile.AddRange(ps.DrawPile);
            ps.DrawPile.Clear();
        }
        
        /// <summary>
        /// Shows cards on top of the draw pile. Then returns them back.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Card> Show(int count)
        {
            var list = new List<Card>(count);

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
                    ps.DrawPile.Shuffle(rnd);
                }

                // draw one card
                var card = ps.DrawPile[ps.DrawPile.Count - 1];
                ps.DrawPile.RemoveAt(ps.DrawPile.Count - 1);
                Game.Logger?.Log($"{Name} shows {card.Name}");
                list.Add(card);
            }
            return list;
        }

        /// <summary>
        /// If player deals attack, defended can reveal reaction cards.
        /// If any of revealed cards is Moad attack effect wont be executed.
        /// </summary>
        /// <param name="attack"></param>
        /// <param name="attacker"></param>
        /// <param name="attackCard"></param>
        public void DealAttack(Player attacker, Card attackCard)
        {
            Game.Logger?.Log($"{Name} deals attack.");

            // before attack is executed defender can select some reaction cards.
            Card card = null;
            bool defended = false;
            var reactions = new LinkedList<Card>(ps.Hand.Where(c => c.IsReaction));
            for (int i = 0; i < reactions.Count; i++)
            {
                if (reactions.Count == 0)
                    break;
                card = User.PlayCard(reactions, ps, Game.Kingdom, Phase.Reaction, attackCard);
                reactions.Remove(card);
                if (card == null)
                    break;
                defended |= card.Reaction(this);
            }

            if (!defended)
                attackCard.Attack(this, attacker);
        }

        public override string ToString() => User.GetName();
    }
}
