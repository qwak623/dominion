using GameCore.Cards.GeneralCards;

namespace GameCore.Cards.Base
{
    public class Moneylender : Card
    {
        static Moneylender moneylender = null;
        private Moneylender() : base
        (
            name: "Moneylender",
            type: CardType.Moneylender,
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

        public static new Moneylender Get() => moneylender ?? new Moneylender();

        protected override void ActionEffect(Player player)
        {
            // todo choice u 
            if (player.ps.Hand.Remove(Copper.Get()))
            {
                player.Game.Logger.Log($"{player.Name} trashes Copper and gains +3$");
                player.ps.Coins += 3;
            }
        }
    }
}
