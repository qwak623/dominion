using System.Linq;

namespace GameCore.Cards.Base
{
    public class Bureaucrat : Card
    {
        static Bureaucrat bureaucrat = null;

        private Bureaucrat() : base
        (
            name: "Bureaucrat",
            type: CardType.Bureaucrat,
            price: 4,
            addActions: 0,
            addBuys: 0,
            addCoins: 0,
            drawCards: 0,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: true
        )
        { }

        public static new Bureaucrat Get() => bureaucrat ?? new Bureaucrat();

        protected override void ActionEffect(Player player)
        {
            player.GainToDrawPile(player.Game.Kingdom.SingleOrDefault(p => p.Type == CardType.Silver).Type);
        }

        public override void Attack(Player defender, Player attacker)
        {
            if (!defender.ps.Hand.Any(c => c.IsVictory))
                return;
            var card = defender.user.Choose(defender.ps.Hand.Where(c => c.IsVictory), defender.ps, attacker.Game.Kingdom, 1, Phase.Attack, null).Single();
            defender.ReturnToDrawPile(card);
        }
    }
}
