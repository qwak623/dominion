namespace GameCore.Cards.Base
{
    public class Woodcutter : Card
    {
        static Woodcutter woodcutter = null;
        private Woodcutter() : base
        (
            name: "Woodcutter",
            type: CardType.Woodcutter,
            price: 3,
            addActions: 0,
            addBuys: 1,
            addCoins: 2,
            drawCards: 0,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: false
        ) => woodcutter = this;

        public static new Woodcutter Get() => woodcutter ?? new Woodcutter();
    }
}
