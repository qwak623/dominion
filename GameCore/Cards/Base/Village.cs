namespace GameCore.Cards.Base
{
    public class Village : Card
    {
        static Village village = null;
        private Village() : base
        (
            name: "Village",
            type: CardType.Village,
            price: 3,
            addActions: 2,
            addBuys: 0,
            addCoins: 0,
            drawCards: 1,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: false
        )
        { }

        public static Village Get() => village ?? new Village();
    }
}
