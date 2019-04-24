using System.Linq;

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
        )
        { }

        public static new Remodel Get() => remodel ?? new Remodel();

        protected override void ActionEffect(Player player)
        {
            // if user didnt select card he wont gain any.
            var oldCard = player.User.Choose(player.ps.Hand, player.ps, player.Game.Kingdom, 1, Phase.Action, null).SingleOrDefault();
            if (oldCard == null)
                return;
            player.Trash(oldCard);

            var newCard = player.User.SelectCardToGain(player.Game.Kingdom
                .Where(p => p.Price <= oldCard.Price + 2 && p.Count != 0)
                .Select(p => p.Card), player.ps, player.Game.Kingdom);
            player.Gain(newCard.Type);
        }
    }
}
