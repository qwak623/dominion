using System.Linq;

namespace GameCore.Cards.Base
{
    public class Remodel : Card
    {
        static Remodel remodel = null;
        private Remodel() : base
        (
            name: "Remodel",
            type: CardType.Remodel,
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

        public static new Remodel Get() => remodel ?? new Remodel();

        protected override void ActionEffect(Player p)
        {
            // if user didnt select card he wont gain any.
            var oldCard = p.User.Choose(p.ps.Hand, p.ps, p.Game.Kingdom, 1, Phase.Action, null).SingleOrDefault();
            if (oldCard == null)
                return;
            p.Trash(oldCard);

            var newCard = p.User.SelectCardToGain(p.Game.Kingdom.GetWrapper(oldCard.Price + 2), p.ps, p.Game.Kingdom, Phase.Gain);
            p.Gain(newCard.Type);
        }
    }
}
