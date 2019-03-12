using System.Linq;

namespace GameCore.Cards.Base
{
    public class Witch : Card
    {
        static Witch witch = null;
        private Witch() : base
        (
            id: 30,
            name: "Witch",
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
        )
        { }

        public static Witch Get() => witch ?? new Witch();

        public override void Attack(Player defender, Player attacker)
        {
            defender.Gain(defender.game.Kingdom.Where(p => p.CardId == 31).Select(p => p.Card).SingleOrDefault());
        }
    }
}
