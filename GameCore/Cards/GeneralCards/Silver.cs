namespace GameCore.Cards.GeneralCards
{
    public class Silver : Card
    {
        static Silver silver = null;
        private Silver() : base
        (
            name: "Silver",
            type: CardType.Silver,
            price: 3,
            addBuys: 0,
            victoryPoints: 0,
            coins: 2,
            isVictory: false,
            isTreasure: true
        )
        { }

        public static new Silver Get() => silver ?? new Silver();
    }
}
