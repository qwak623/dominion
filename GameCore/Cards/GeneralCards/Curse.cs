namespace GameCore.Cards.GeneralCards
{
    // TODO v nekterych rozsirenich je rozdil protoze curse neni victorycard a nevztahuji se na ni nejake veci... ale ted to pro zjednoduseni nejakych veci nebudu resit
    public class Curse : Card
    {
        static Curse curse = null;
        private Curse() : base
        (
            name: "Curse",
            type: CardType.Curse,
            price: 0,
            addBuys: 0,
            victoryPoints: -1,
            coins: 0,
            isVictory: true,
            isTreasure: false
        ) => curse = this;

        public static new Curse Get() => curse ?? new Curse();
    }
}
