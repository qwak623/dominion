namespace GameCore.Cards.GeneralCards
{
    public class Gold : Card
    {
        static Gold gold = null;
        private Gold() : base
        (
            id: 3,
            name: "Gold",
            price: 6,
            addBuys: 0,
            victoryPoints: 0,
            coins: 3,
            isVictory: false,
            isTreasure: true
        )
        { }

        public static Gold Get() => gold ?? new Gold();
    }
}
