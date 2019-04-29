using System.Linq;

namespace GameCore.Cards.Base
{
    public class Mine : Card
    {
        static Mine mine = null;
        private Mine() : base
        (
            name: "Mine",
            type: CardType.Mine,
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

        public static new Mine Get() => mine ?? new Mine();

        protected override void ActionEffect(Player p)
        {
            // todo tady by melo mozna byt neco jako trash choice
            var oldCard = p.User.Choose(p.ps.Hand.Where(c => c.IsTreasure), p.ps, p.Game.Kingdom, 0, 1, Phase.Action).SingleOrDefault();
            if (oldCard == null)
                return;
            p.Trash(oldCard);
            var newCard = p.User.SelectCardToGain(p.Game.Kingdom.GetWrapper(oldCard.Price + 3, true), p.ps, p.Game.Kingdom, Phase.Gain);
            if (newCard != null)
                p.GainToHand(newCard.Type);
        }
    }
}
