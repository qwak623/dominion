namespace GameCore.Cards.Base
{
    public class Chancellor : Card
    {
        static Chancellor chancellor = null;
        private Chancellor() : base
        (
            id: 12,
            name: "Chancellor",
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

        protected override void SpecialPlayEffect(Player player)
        {  // todo ta choose by mela byt s nejakym textem asi... obecne vsechny asi...
            if (player.user.Choose())
                player.DiscardDrawPile();
        }
    }
}
