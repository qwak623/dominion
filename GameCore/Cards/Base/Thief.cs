using System.Linq;

namespace GameCore.Cards.Base
{
    public class Thief : Card
    {
        static Thief thief = null;
        private Thief() : base
        (
            name: "Thief",
            type: CardType.Thief,
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

        public static Thief Get() => thief ?? new Thief();

        public override void Attack(Player defender, Player attacker)
        {
            // show two cards
            var cards = defender.Show(2);
            // selecting treasures
            var treasures = cards.Where(c => c.IsTreasure);
            defender.Draw(cards.Count);
            // if there are treasure cards
            if (treasures.Count() > 0)
            {
                // attacker have to pick one
                var card = attacker.user.Choose(treasures, defender.ps, 1, treasures.Count(), Phase.Action, null).Single();
                // the other one is discarded (if there is)
                cards.Remove(card);
                foreach (var item in cards)
                    defender.Discard(card);

                // attaker chooses if he will trash or steal
                if (attacker.user.Choose())
                {  // he will steal
                    defender.ps.Hand.Remove(card);
                    attacker.ps.PlayedCards.Add(card);
                }
                else
                {  // he will trash
                    defender.Trash(card);
                }
            }
            else
            {
                foreach (var card in cards)
                    defender.Discard(card);
            }
        }
    }
}
