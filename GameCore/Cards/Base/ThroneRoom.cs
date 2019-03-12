using System.Linq;

namespace GameCore.Cards.Base
{
    public class ThroneRoom : Card
    {
        static ThroneRoom throneRoom = null;
        private ThroneRoom() : base
        (
            id: 20,
            name: "Throne Room",
            price: 4,
            addActions: 0,
            addBuys: 0,
            addCoins: 0,
            drawCards: 0,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: false
        )
        { }

        public static ThroneRoom Get() => throneRoom ?? new ThroneRoom();

        protected override void SpecialPlayEffect(Player player)
        {
            var card = player.user.Choose(player.ps.Hand.Where(c => c.IsAction), player.ps, 1, 1).SingleOrDefault();
            if (card == null)
                return;
            player.ps.Hand.Remove(card);
            player.ps.PlayedCards.Add(card);
            for (int i = 0; i < 2; i++)
            {
                card.PlayEffect(player);
                if (card.IsAttack)
                    foreach (var defender in player.game.Players.Where(p => p != player))
                        defender.DealAttack(card.Attack, player);
            }
        }
    }
}
