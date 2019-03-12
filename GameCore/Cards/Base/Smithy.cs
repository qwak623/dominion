namespace GameCore.Cards.Base
{
    public class Smithy : Card
    {
        static Smithy smithy = null;
        private Smithy() : base
        (
            id: 19,
            name: "Smithy",
            price: 4,
            addActions: 0,
            addBuys: 0,
            addCoins: 0,
            drawCards: 3,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: false
        )
        { }

        public static Smithy Get() => smithy ?? new Smithy();
    }
}
