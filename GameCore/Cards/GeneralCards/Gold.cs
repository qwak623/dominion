namespace GameCore.Cards.GeneralCards
{
    public class Gold : Card
    {
        static Gold gold = null;
        private Gold() : base
        (
            name: "Gold",
            type: CardType.Gold,
            price: 6,
            addBuys: 0,
            victoryPoints: 0,
            coins: 3,
            isVictory: false,
            isTreasure: true
        ) => gold = this;

        public static new Gold Get() => gold ?? new Gold();
    }
}
