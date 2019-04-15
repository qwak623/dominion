namespace GameCore.Cards.Base
{
    public class Festival : Card
    {
        static Festival festival = null;
        private Festival() : base
        (
            name: "Festival",
            type: CardType.Festival,
            price: 5,
            addActions: 2,
            addBuys: 1,
            addCoins: 2,
            drawCards: 0,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: false
        )
        { }

        public static new Festival Get() => festival ?? new Festival();
    }
}
