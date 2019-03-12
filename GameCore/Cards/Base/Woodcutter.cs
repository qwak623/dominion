namespace GameCore.Cards.Base
{
    public class Woodcutter : Card
    {
        static Woodcutter woodcutter = null;
        private Woodcutter() : base
        (
            id: 13,
            name: "Woodcutter",
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
        )
        { }

        public static Woodcutter Get() => woodcutter ?? new Woodcutter();
    }
}
