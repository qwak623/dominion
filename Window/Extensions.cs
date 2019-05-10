using GameCore.Cards;
using System.Drawing;

namespace Window
{
    static class Extensions
    {
        public static Color ToBackColor(this Card card)
        {
            if (card == null)
                return Color.DarkGray;
            if (card.Type == CardType.Curse)
                return Color.Violet;
            if (card.IsTreasure)
                return Color.Yellow;
            if (card.IsVictory)
                return Color.LightGreen;
            if (card.IsReaction)
                return Color.LightBlue;
            else return Color.White;
        }
    }
}
