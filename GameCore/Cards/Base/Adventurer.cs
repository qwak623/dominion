using System.Linq;

namespace GameCore.Cards.Base
{
    public class Adventurer : Card
    {
        static Adventurer adventurer = null;

        private Adventurer() : base
        (
            name: "Adventurer",
            type: CardType.Adventurer,
            price: 6,
            addActions: 0,
            addBuys: 0,
            addCoins: 0,
            drawCards: 0,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: false
        ) => adventurer = this;

        public static new Adventurer Get() => adventurer ?? new Adventurer();

        protected override void ActionEffect(Player player) => player.GainToDrawPile(CardType.Silver);

        public override void Attack(Player defender, Player attacker)
        {
            throw new System.Exception();
        }
    }
}
