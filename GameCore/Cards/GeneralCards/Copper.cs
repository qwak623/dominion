namespace GameCore.Cards.GeneralCards
{
    public class Copper : Card
    {
        static Copper copper = null;
        private Copper() : base
        (
            id: 1,
            name: "Copper",
            price: 0,
            addBuys: 0,
            victoryPoints: 0,
            coins: 1,
            isVictory: false,
            isTreasure: true
        )
        { }

        public static Copper Get() => copper ?? new Copper();
    }
}
