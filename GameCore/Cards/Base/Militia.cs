﻿namespace GameCore.Cards.Base
{
    public class Militia : Card
    {
        static Militia militia = null;
        private Militia() : base
        (
            name: "Militia",
            type: CardType.Militia,
            price: 4,
            addActions: 0,
            addBuys: 0,
            addCoins: 2,
            drawCards: 0,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: true
        )
        { }

        public static new Militia Get() => militia ?? new Militia();

        public override void Attack(Player defender, Player attacker)
        {
            if (defender.ps.Hand.Count <= 3)
                return;
            var cards = defender.User.Choose(defender.ps.Hand, defender.ps, defender.Game.Kingdom, defender.ps.Hand.Count - 3, Phase.Attack, this);
            foreach (var card in cards)
                defender.Discard(card);
        }

    }
}
