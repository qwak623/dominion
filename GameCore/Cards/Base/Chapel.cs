namespace GameCore.Cards.Base
{
    public class Chapel : Card
    {
        static Chapel chapel = null;
        private Chapel() : base
        (
            name: "Chapel",
            type: CardType.Chapel,
            price: 2,
            addActions: 0,
            addBuys: 0,
            addCoins: 0,
            drawCards: 0,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: false
        ) { }

        public static Chapel Get() => chapel ?? new Chapel();

        protected override void ActionEffect(Player player)
        {
            string desc = null; // TODO
            var selectedCards = player.user.Choose(player.ps.Hand, player.ps, 0, 4, Phase.Action, desc);
            foreach (var card in selectedCards)
                player.Trash(card);
        }
    }
}
