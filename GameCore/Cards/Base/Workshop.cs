using System.Linq;

namespace GameCore.Cards.Base
{
    public class Workshop : Card
    {
        static Workshop workshop = null;
        private Workshop() : base
        (
            name: "Workshop",
            type: CardType.Workshop,
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

        public static new Workshop Get() => workshop ?? new Workshop();

        protected override void ActionEffect(Player player)
        {
            var card = player.user.SelectCardToGain(player.Game.Kingdom.Where(p => !p.Empty && p.Price <= 4)
                .Select(p => p.Card), player.ps, player.Game.Kingdom);
            player.Gain(card.Type);
        }
    }
}
