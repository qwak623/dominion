using System.Linq;

namespace GameCore.Cards.Base
{
    public class Witch : Card
    {
        static Witch witch = null;
        private Witch() : base
        (
            name: "Witch",
            type: CardType.Witch,
            price: 5,
            addActions: 0,
            addBuys: 0,
            addCoins: 0,
            drawCards: 2,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: true
        )
        { }

        public static new Witch Get() => witch ?? new Witch();

        public override void Attack(Player defender, Player attacker)
        {
            // todo problem kdyz dojdou kletby, bylo by hezke udelat z kingdomu dictionary...
            // todo u vybirani kralovstvi by bylo dobre automaticky vybrat kletby kdyz vybiram carodejnici, zalarnika a tak
            defender.Gain(defender.Game.Kingdom.Where(p => p.Type == CardType.Curse).Select(p => p.Type).SingleOrDefault());
        }
    }
}
