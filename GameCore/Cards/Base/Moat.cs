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

        public static new Moat Get() => moat ?? new Moat();

        // todo nějakym zpusobem ukazat ze uz byl zahrany (a taky kvuli inteligenci... aby se necyklina navždy)
        public override bool Reaction(Player player) => true;
    }
}
