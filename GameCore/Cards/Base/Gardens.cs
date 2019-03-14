namespace GameCore.Cards.Base
{
    public class Gardens : Card
    {
        static Gardens gardens = null;
        private Gardens() : base
        (
            name: "Gardens",
            type: CardType.Gardens,
            price: 4,
            addActions: 0,
            addBuys: 0,
            addCoins: 0,
            drawCards: 0,
            isVictory: true,
            isTreasure: false,
            isAction: false,
            isReaction: false,
            isAttack: false
        )
        { }

        public static Gardens Get() => gardens ?? new Gardens();

        public override void EndOfGameEffect(Player player) => player.VictoryPoints += player.CardCount / 10;
    }
}
