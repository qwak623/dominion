using System.Linq;

namespace GameCore.Cards.Base
{
    public class Mine : Card
    {
        static Mine mine = null;
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
            isAttack: false
        )
        { }

        public static Mine Get() => mine ?? new Mine();

        protected override void ActionEffect(Player player)
        {
            var oldCard = player.user.Choose(player.ps.Hand.Where(c => c.IsTreasure), player.ps, 1, Phase.Action, null).SingleOrDefault();
            if (oldCard == null)
                return;
            player.Trash(oldCard);
            var newCard = player.user.Choose(player.game.Kingdom.Where(p => p.Card.IsTreasure && p.Price <= oldCard.Price + 3)
                                                                .Select(p => p.Card), player.ps, 1, Phase.Action, null).SingleOrDefault();
            player.GainToHand(newCard.Type);
        }
    }
}
