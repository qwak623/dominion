using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public abstract class Card
    {
        public readonly int Id;    // id of card type
        public readonly string Name;
        public readonly int Price;
        public readonly int AddActions;
        public readonly int AddBuys;
        public readonly int AddCoins;
        public readonly int DrawCards;
        public readonly int Coins;

        public int VictoryPoints { get; protected set; }

        public readonly bool IsVictory;
        public readonly bool IsTreasure;
        public readonly bool IsAction;
        public readonly bool IsReaction;
        public readonly bool IsAttack;

        public Card(int id, string name, int price, int addActions, int addBuys, int addCoins, int drawCards, bool isVictory, bool isTreasure, bool isAction, bool isReaction, bool isAttack)
        {
            Id = id;
            Name = name;
            Price = price;
            AddActions = addActions;
            AddBuys = addBuys;
            AddCoins = addCoins;
            DrawCards = drawCards;
            IsVictory = isVictory;
            IsTreasure = isTreasure;
            IsAction = isAction;
            IsReaction = isReaction;
            IsAttack = isAttack;
        }

        public Card(int id, string name, int price, int addBuys, int victoryPoints, int coins, bool isVictory, bool isTreasure)
        {
            Id = id;
            Name = name;
            Price = price;
            AddBuys = addBuys;
            VictoryPoints = victoryPoints;
            Coins = coins;
            IsVictory = isVictory;
            IsTreasure = isTreasure;
        }

        public Card Get() => this;

        public void PlayEffect(Player player)
        {
            player.ps.Actions += AddActions;
            player.ps.Coins += AddCoins;
            player.ps.Buys += AddBuys;
            if (DrawCards != 0)
                player.Draw(DrawCards);

            SpecialPlayEffect(player);
        }

        protected virtual void SpecialPlayEffect(Player player) { }

        public void BuyEffect(Player player)
        {
            player.ps.Buys += AddBuys;
            player.ps.Coins += AddCoins;

            SpecialBuyEffect(player);
        }

        protected virtual void SpecialBuyEffect(Player player) { }

        /// <summary>
        /// Returns true if attack was defended.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="player"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual bool ReactionEffect(Player player) => false;

        public virtual void EndOfGameEffect(Player player) { }

        public virtual void Attack(Player defender, Player attacker) { }

        public override string ToString() => Name;
    }
}
