using GameCore.Cards;
using System.Collections.Generic;

namespace GameCore
{
    public abstract class User
    {
        public abstract Card PlayCard(IEnumerable<Card> cards, PlayerState gs, Phase phase);

        public abstract string GetName();

        // todo hodil by se tady string s popisem volby...
        public abstract IEnumerable<Card> Choose(IEnumerable<Card> cards, PlayerState gs, int min, int max);

        public IEnumerable<Card> Choose(IEnumerable<Card> cards, PlayerState gs, int count) => Choose(cards, gs, count, count);

        public abstract bool Choose();
    }

    public enum Phase { Action, Treasure, Buy}
}
