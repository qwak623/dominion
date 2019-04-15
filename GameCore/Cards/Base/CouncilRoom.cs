using System.Linq;

namespace GameCore.Cards.Base
{
    public class CouncilRoom : Card
    {
        static CouncilRoom councilRoom = null;
        private CouncilRoom() : base
        (
            name: "Council Room",
            type: CardType.CouncilRoom,
            price: 5,
            addActions: 0,
            addBuys: 1,
            addCoins: 0,
            drawCards: 4,
            isVictory: false,
            isTreasure: false,
            isAction: true,
            isReaction: false,
            isAttack: false
        )
        { }

        public static new CouncilRoom Get() => councilRoom ?? new CouncilRoom();

        protected override void ActionEffect(Player player)
        {
            foreach (var plr in player.Game.Players.Where(p => p != player))
                plr.Draw(1);
        }
    }
}
