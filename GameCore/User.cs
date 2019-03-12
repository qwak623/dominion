using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public abstract class User
    {
        protected Action<IEnumerable<Card>, PlayerState, Phase> playCard;
        // set, min, max
        protected Action<IEnumerable<Card>, PlayerState, int, int> choice;
        protected Action alternativeChoice;
        protected Job job;

        public abstract Card PlayCard(IEnumerable<Card> cards, PlayerState gs, Phase phase);

        public abstract string GetName();

        // todo hodil by se tady string s popisem volby...
        public abstract IEnumerable<Card> Choose(IEnumerable<Card> cards, PlayerState gs, int min, int max);
        public IEnumerable<Card> Choose(IEnumerable<Card> cards, PlayerState gs, int count) => Choose(cards, gs, count, count);
        public abstract bool Choose();
    }

    public enum Phase { Action, Treasure, Buy}
}
