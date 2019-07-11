﻿using System.Linq;

namespace GameCore.Cards.Base
{
    public class Remodel : Card
    {
        static Remodel remodel = null;
        private Remodel() : base
        (
            name: "Remodel",
            type: CardType.Remodel,
            price: 4,
            addActions: 0,
            addBuys: 0,
            addCoins: 0,
            drawCards: 0,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: false
        ) => remodel = this;

        public static Remodel Get() => remodel ?? new Remodel();

        protected override void ActionEffect(Player p)
        {
            // if user didnt select card he wont gain any.
            var oldCard = p.User.RemodelTrash(p.ps, p.Game.Kingdom);
            if (oldCard == null)
                return;
            p.Trash(oldCard);

            var newCard = p.User.SelectCardToGain(p.Game.Kingdom.GetWrapper(oldCard.Price + 2), p.ps, p.Game.Kingdom, Phase.Gain);
            if (newCard != null)
                p.Gain(newCard.Type);
        }
    }
}
