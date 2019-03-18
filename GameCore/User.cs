using GameCore.Cards;
using System.Collections.Generic;

namespace GameCore
{
    public abstract class User
    {
        public abstract Card PlayCard(IEnumerable<Card> cards, PlayerState ps, Phase phase, string cardName = null);

        public abstract string GetName();

        // todo hodil by se tady string s popisem volby...
        public abstract IEnumerable<Card> Choose(IEnumerable<Card> cards, PlayerState ps, int min, int max, Phase phase, string desc);

        public IEnumerable<Card> Choose(IEnumerable<Card> cards, PlayerState ps, int count, Phase phase, string desc) => Choose(cards, ps, count, count, phase, desc);

        public abstract bool Choose();
    }

    public enum Phase { Action, Treasure, Buy, Reaction, Attack}
}
