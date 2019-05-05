namespace GameCore.Cards.Base
{
    public class Laboratory : Card
    {
        static Laboratory laboratory = null;
        private Laboratory() : base
        (
            name: "Laboratory",
            type: CardType.Laboratory,
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
        ) => laboratory = this;

        public static new Laboratory Get() => laboratory ?? new Laboratory();
    }
}
