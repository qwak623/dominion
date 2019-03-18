namespace GameCore.Cards.Base
{
    public class Chancellor : Card
    {
        static Chancellor chancellor = null;
        private Chancellor() : base
        (
            name: "Chancellor",
            type: CardType.Chancelor,
            price: 3,
            addActions: 0,
            addBuys: 0,
            addCoins: 2,
            drawCards: 0,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: false
        )
        { }

        public static Chancellor Get() => chancellor ?? new Chancellor();

        protected override void ActionEffect(Player player)
        {  // todo ta choose by mela byt s nejakym textem asi... obecne vsechny asi...
            if (player.user.Choose())
                player.DiscardDrawPile();
        }
    }
}
