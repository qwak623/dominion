using System.Linq;

namespace GameCore.Cards.Base
{
    public class Workshop : Card
    {
        static Workshop workshop = null;
        private Workshop() : base
        (
            id: 11,
            name: "Workshop",
            price: 3,
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

        public static Workshop Get() => workshop ?? new Workshop();

        protected override void SpecialPlayEffect(Player player)
        {
            var card = player.user.Choose(player.game.Kingdom.Where(p => p.CardPrice <= 4).Select(p => p.Card), player.ps, 0, 1).SingleOrDefault();
            player.Gain(card);
        }
    }
}
