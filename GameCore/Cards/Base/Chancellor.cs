namespace GameCore.Cards.Base
{
    public class Chancellor : Card
    {
        static Chancellor chancellor = null;
        private Chancellor() : base
        (
            name: "Chancellor",
            type: CardType.Chancellor,
            price: 3,
            addActions: 0,
            addBuys: 0,
            addCoins: 2,
            drawCards: 0,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: false,
            message: "You may immediately put your deck into your discard pile."
        ) => chancellor = this;

        public static new Chancellor Get() => chancellor ?? new Chancellor();

        protected override void ActionEffect(Player player)
        { 
            if (player.User.Choose(player.ps, player.Game.Kingdom, Phase.Action, "Do", "Don't", this))
                player.DiscardDrawPile();
        }
    }
}
