﻿using System.Linq;

namespace GameCore.Cards.Base
{
    public class Cellar : Card
    {
        static Cellar cellar = null;
        private Cellar() : base
        (
            name: "Cellar",
            type: CardType.Cellar,
            price: 2,
            addActions: 1,
            addBuys: 0,
            addCoins: 0,
            drawCards: 0,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: false,
            message: "Discard any number of cards, then draw that many."
        ) => cellar = this;

        public static Cellar Get() => cellar ?? new Cellar();

        protected override void ActionEffect(Player player)
        {
            var selectedCards = player.User.CellarDiscard(player.ps, player.Game.Kingdom);

            selectedCards.ForEach(card => player.Discard(card));
            player.Draw(selectedCards.Count());
        }
    }
}
