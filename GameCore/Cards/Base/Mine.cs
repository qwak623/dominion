﻿using System.Linq;

namespace GameCore.Cards.Base
{
    public class Mine : Card
    {
        static Mine mine = null;
        static int c;
        private Mine() : base
        (
            name: "Mine",
            type: CardType.Mine,
            price: 5,
            addActions: 0,
            addBuys: 0,
            addCoins: 0,
            drawCards: 0,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: false,
            message: "Trash a treasure, gain a treasure to your hand costing up to $3 more."
        ) => mine = this;

        public static new Mine Get() => mine ?? new Mine();

        protected override void ActionEffect(Player p)
        {
            c++;
            var oldCard = p.User.MineTrash(p.ps, p.Game.Kingdom);
            if (oldCard == null)
                return;
            p.Trash(oldCard);
            var newCard = p.User.SelectCardToGain(p.Game.Kingdom.GetWrapper(oldCard.Price + 3, true), p.ps, p.Game.Kingdom, Phase.Gain);
            if (newCard != null)
                p.GainToHand(newCard.Type);
        }
    }
}
