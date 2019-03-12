using System.Linq;

namespace GameCore.Cards.Base
{
    public class Library : Card
    {
        static Library library = null;
        private Library() : base
        (
            id: 27,
            name: "Library",
            price: 5,
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

        public static Library Get() => library ?? new Library();

        protected override void SpecialPlayEffect(Player player)
        {
            // todo potencionalne nekonecna smycka
            while (player.ps.Hand.Count < 7)
            {
                var card = player.Show(1).SingleOrDefault();
                if (card == null)
                    break;
                player.Draw(1);
                // if user wishes to discard action card
                if (card.IsAction && player.user.Choose()) 
                    player.Discard(card);
            }
        }
    }
}
