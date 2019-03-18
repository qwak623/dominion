namespace GameCore.Cards.Base
{
    public class Moat : Card
    {
        static Moat moat = null;
        private Moat() : base
        (
            name: "Moat",
            type: CardType.Moat,
            price: 2,
            addActions: 0,
            addBuys: 0,
            addCoins: 0,
            drawCards: 2,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: true,
            isAttack: false
        )
        { }

        public static Moat Get() => moat ?? new Moat();

        public override bool Reaction(Player player) => true;
    }
}
