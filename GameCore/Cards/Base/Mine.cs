using System.Linq;

namespace GameCore.Cards.Base
{
    public class Mine : Card
    {
        static Mine mine = null;
        private Mine() : base
        (
            id: 29,
            name: "Mine",
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

        protected override void SpecialPlayEffect(Player player)
        {
            var oldCard = player.user.Choose(player.ps.Hand.Where(c => c.IsTreasure), player.ps, 1).SingleOrDefault();
            if (oldCard == null)
                return;
            player.Trash(oldCard);
            var newCard = player.user.Choose(player.game.Kingdom.Where(p => p.Card.IsTreasure && p.CardPrice <= oldCard.Price + 3).Select(p => p.Card), player.ps, 1).SingleOrDefault();
            player.GainToHand(newCard);
        }
    }
}
