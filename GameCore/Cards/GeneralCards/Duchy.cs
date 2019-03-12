namespace GameCore.Cards.GeneralCards
{
    public class Duchy : Card
    {
        static Duchy duchy = null;
        private Duchy() : base
        (
            id: 5,
            name: "Duchy",
            price: 5,
            addBuys: 0,
            victoryPoints: 3,
            coins: 0,
            isVictory: true,
            isTreasure: false
        )
        { }

        public static Duchy Get() => duchy ?? new Duchy();
    }
}
