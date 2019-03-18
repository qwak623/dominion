using System.Linq;

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
            isAttack: false
        )
        { }

        public static Cellar Get() => cellar ?? new Cellar();

        protected override void ActionEffect(Player player)
        {
            // TODO napis tu spravny desc pro cellar
            var selectedCards = player.user.Choose(player.ps.Hand, player.ps, 0, player.ps.Hand.Count, Phase.Action, null);
            foreach (var card in selectedCards)
                player.Discard(card);
            player.Draw(selectedCards.Count());
        }
    }
}
