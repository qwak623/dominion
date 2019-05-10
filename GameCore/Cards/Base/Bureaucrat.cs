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
        ) => bureaucrat = this;

        public static new Bureaucrat Get() => bureaucrat ?? new Bureaucrat();

        protected override void ActionEffect(Player player) => player.GainToDrawPile(CardType.Silver);

        public override void Attack(Player def, Player att)
        {
            if (!def.ps.Hand.Any(c => c.IsVictory))
                return;
            var card = def.User.Choose(def.ps.Hand.Where(c => c.IsVictory), def.ps, att.Game.Kingdom, 1, Phase.Attack, this).Single();
            def.ReturnToDrawPile(card);
        }
    }
}
