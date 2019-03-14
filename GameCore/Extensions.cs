using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameCore
{
    public static class Extensions
    {
        static Random rnd = new Random();
        public static void Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // todo bude potreba predelat na list<Color>
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
