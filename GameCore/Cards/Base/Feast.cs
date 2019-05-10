using System.Linq;

namespace GameCore.Cards.Base
{
    public class Feast : Card
    {
        static Feast feast = null;
        private Feast() : base
        (
            name: "Feast",
            type: CardType.Feast,
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
        ) => feast = this;

        public static new Feast Get() => feast ?? new Feast();

        protected override void ActionEffect(Player player)
        {
            // bylo by hezke zjistit jestli se mi vůbec vyplati hrat, kdyz neni karta co bych chtel
            try
            {
                player.ps.PlayedCards.Remove(this);
                player.Game.Trash.Add(this);
                player.Game.Logger?.Log($"{player.Name} trashes {Name}");
                var card = player.User.SelectCardToGain(player.Game.Kingdom.GetWrapper(5), player.ps, player.Game.Kingdom, Phase.Gain);
                if (card != null)
                    player.Gain(card.Type);
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}
