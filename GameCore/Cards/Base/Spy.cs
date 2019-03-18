using System.Linq;

namespace GameCore.Cards.Base
{
    public class Spy : Card
    {
        static Spy spy = null;
        private Spy() : base
        (
            name: "Spy",
            type: CardType.Spy,
            price: 4,
            addActions: 1,
            addBuys: 0,
            addCoins: 0,
            drawCards: 1,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: true
        )
        { }

        public static Spy Get() => spy ?? new Spy();

        protected override void ActionEffect(Player player)
        {
            var card = player.Show(1).SingleOrDefault();
            if (card == null)
                return;
            if (player.user.Choose())
            {
                player.Draw(1);
                player.Discard(card);
            }
        }

        public override void Attack(Player defender, Player attacker)
        {
            var card = defender.Show(1).SingleOrDefault();
            if (attacker.user.Choose())
            {
                defender.Draw(1);
                defender.Discard(card);
            }
        }
    }
}
