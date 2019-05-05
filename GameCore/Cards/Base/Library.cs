using System.Linq;

namespace GameCore.Cards.Base
{
    public class Library : Card
    {
        static Library library = null;
        private Library() : base
        (
            name: "Library",
            type: CardType.Library,
            price: 5,
            addActions: 0,
            addBuys: 0,
            addCoins: 0,
            drawCards: 0,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: false,
            message: "You may skip any action card you choose to."
        ) => library = this;

        public static new Library Get() => library ?? new Library();

        protected override void ActionEffect(Player player)
        {
            // todo potencionalne nekonecna smycka
            while (player.ps.Hand.Count < 7)
            {
                try
                {
                    var card = player.Show(1).SingleOrDefault();
                    if (card == null)
                        break;
                    
                    //player.Draw(1);
                    
                    // if user wishes to discard action card
                    string yup = $"Skip {card.Name}";
                    string nay = $"Keep {card.Name}";

                    if (card.IsAction && player.User.Choose(player.ps, player.Game.Kingdom, Phase.Action, yup, nay, this))
                        player.ps.DiscardPile.Add(card);
                    else
                        player.ps.Hand.Add(card);
                }
                catch (System.Exception e)
                { // TODO smazat throw
                    throw e;
                }
            }
        }
    }
}
