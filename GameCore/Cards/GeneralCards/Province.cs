namespace GameCore.Cards.GeneralCards
{
    public class Province : Card
    {
        static Province province = null;
        private Province() : base
        (
            name: "Province",
            type: CardType.Province,
            price: 8,
            addBuys: 0,
            victoryPoints: 6,
            coins: 0,
            isVictory: true,
            isTreasure: false
        )
        { }

        public static new Province Get() => province ?? new Province();
    }
}
