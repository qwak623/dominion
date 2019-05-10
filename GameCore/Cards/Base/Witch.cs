namespace GameCore.Cards.Base
{
    public class Witch : Card
    {
        static Witch witch = null;
        private Witch() : base
        (
            name: "Witch",
            type: CardType.Witch,
            price: 5,
            addActions: 0,
            addBuys: 0,
            addCoins: 0,
            drawCards: 2,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: true
        ) => witch = this;

        public static new Witch Get() => witch ?? new Witch();

        public override Card RequiredCards => GeneralCards.Curse.Get();

        public override void Attack(Player defender, Player attacker) => defender.Gain(CardType.Curse);
    }
}
