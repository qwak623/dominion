using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GameCore
{
    public static class Extensions
    {
        public static void Shuffle<T>(this List<T> list, ThreadSafeRandom rnd)
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

        public static Kingdom GetKingdom(this List<Card> cards, int players)
        {
            // this should be correct, since list of cards wont change at this point
            return new Kingdom(cards.Select(c => new Pile(c)).ToList(), players);
        }

        public static List<Card> AddRequiredCards(this List<Card> cards)
        {
            return cards.Concat(PresetGames.VictoryAndTreasures())
                .Concat(cards.Select(c => c.RequiredCards).Where(c => c != null).Distinct()).ToList();
        }

        public static bool Contains(this IEnumerable<Card> cards, CardType type)
        {
            foreach (var card in cards)
                if (card.Type == type)
                    return true;
            return false;
        }
    }
}
