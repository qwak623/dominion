using System.Linq;

namespace GameCore.Cards.Base
{
    public class Spy : Card
    {
        // todo tady chybi vsechna rozhodnuti
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
            isAttack: true // TODO message
        ) => spy = this;

        public static new Spy Get() => spy ?? new Spy();

        protected override void ActionEffect(Player player)
        {
            var card = player.Show(1).SingleOrDefault();
            if (card == null)
                return;
            string discard = $"Discard {card.Name}";
            string back = $"Put it back";
            if (player.User.Choose(player.ps, player.Game.Kingdom, Phase.Action, discard, back, this))
                player.ps.DiscardPile.Add(card);
            else
                player.ps.DrawPile.Add(card);
        }

        public override void Attack(Player defender, Player attacker)
        {
            var card = defender.Show(1).SingleOrDefault();
            if (card == null)
                return;
            string discard = $"Discard {card.Name}";
            string back = $"Put it back";
            if (attacker.User.Choose(attacker.ps, attacker.Game.Kingdom, Phase.Action, discard, back, this))
                defender.ps.DiscardPile.Add(card);
            else
                defender.ps.DrawPile.Add(card);
        }
    }
}
