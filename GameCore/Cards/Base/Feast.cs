using System.Linq;

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
        )
        { }

        public static Feast Get() => feast ?? new Feast();

        protected override void ActionEffect(Player player)
        {
            player.Discard(this);
            var card = player.user.Choose(player.game.Kingdom.Where(p => p.Price <= 5).Select(p => p.Card), player.ps, 1, Phase.Action, null).SingleOrDefault();
            player.Gain(card.Type);
        }
    }
}
