namespace GameCore.Cards.Base
{
    public class Laboratory : Card
    {
        static Laboratory laboratory = null;
        private Laboratory() : base
        (
            id: 26,
            name: "Laboratory",
            price: 5,
            addActions: 1,
            addBuys: 0,
            addCoins: 0,
            drawCards: 2,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: false
        )
        { }

        public static Laboratory Get() => laboratory ?? new Laboratory();
    }
}
