using System;

namespace GameCore.Cards
{
    public abstract class Card
    {
        public string Name { get; protected set; }
        public CardType Type;
        public int Price;
        public int AddActions;
        public int AddBuys;
        public int AddCoins;
        public int DrawCards;
        public int Coins;

        public  bool IsVictory;
        public  bool IsTreasure;
        public  bool IsAction;
        public  bool IsReaction;
        public  bool IsAttack;
        public int VictoryPoints { get; protected set; }

        protected Card(string name, CardType type, int price, int addActions, int addBuys, int addCoins, int drawCards, bool isVictory, bool isTreasure, bool isAction, bool isReaction, bool isAttack)
        {
            Name = name;
            Type = type;
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

        protected Card(string name, CardType type, int price, int addBuys, int victoryPoints, int coins, bool isVictory, bool isTreasure)
        {
            Name = name;
            Type = type;
            Price = price;
            AddBuys = addBuys;
            VictoryPoints = victoryPoints;
            Coins = coins;
            IsVictory = isVictory;
            IsTreasure = isTreasure;
        }

        public Card Get() => this;

        public void WhenPlayAction(Player player)
        {
            player.ps.Actions += AddActions;
            player.ps.Coins += AddCoins;
            player.ps.Buys += AddBuys;
            if (DrawCards != 0)
                player.Draw(DrawCards);

            ActionEffect(player);
        }

        protected virtual void ActionEffect(Player player) { }

        public void WhenPlayTreasure(Player player)
        {
            player.ps.Buys += AddBuys;
            player.ps.Coins += Coins;

            TreasureEffect(player);
        }

        protected virtual void TreasureEffect(Player player) { }

        /// <summary>
        /// Returns true if attack was defended.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="player"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual bool Reaction(Player player) => false;

        public virtual int CountPoints(Player player) => VictoryPoints;

        public virtual void Attack(Player defender, Player attacker) { }

        public override string ToString() => Name;
    }
}
