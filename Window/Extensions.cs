using GameCore.Cards;
using System.Drawing;

namespace Window
{
    static class Extensions
    {
        // todo bude potreba predelat na list<Color>
        // todo aby by bylo heske kdyby to bylo ve window projektu
        public static Color ToBackColor(this Card card)
        {
            if (card == null)
                return Color.DarkGray;
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
