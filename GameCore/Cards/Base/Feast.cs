﻿using System.Linq;

namespace GameCore.Cards.Base
{
    public class Feast : Card
    {
        static Feast feast = null;
        private Feast() : base
        (
            name: "Feast",
            type: CardType.Feast,
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
        ) => feast = this;

        public static Feast Get() => feast ?? new Feast();

        protected override void ActionEffect(Player player)
        {
            player.ps.PlayedCards.Remove(this);
            player.Game.Trash.Add(this);
            player.Game.Logger?.Log($"{player.Name} trashes {Name}");
            var card = player.User.SelectCardToGain(player.Game.Kingdom.GetWrapper(5), player.ps, player.Game.Kingdom, Phase.Gain);
            if (card != null)
                player.Gain(card.Type);
        }
    }
}
