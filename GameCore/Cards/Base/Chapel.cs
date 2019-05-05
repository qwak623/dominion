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
            isAttack: false,
            message: "Trash up to 4 cards from your hand."
        ) => chapel = this;

        public static new Chapel Get() => chapel ?? new Chapel();

        protected override void ActionEffect(Player player)
        {
            var selectedCards = player.User.Choose(player.ps.Hand, player.ps, player.Game.Kingdom, 0, 4, Phase.Action);
            foreach (var card in selectedCards)
                player.Trash(card);
        }
    }
}
