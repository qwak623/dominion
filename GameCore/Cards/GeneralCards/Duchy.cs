namespace GameCore.Cards.GeneralCards
{
    public class Duchy : Card
    {
        static Duchy duchy = null;
        private Duchy() : base
        (
            name: "Duchy",
            type: CardType.Duchy,
            price: 5,
            addBuys: 0,
            victoryPoints: 3,
            coins: 0,
            isVictory: true,
            isTreasure: false
        ) => duchy = this;

        public static new Duchy Get() => duchy ?? new Duchy();
    }
}
