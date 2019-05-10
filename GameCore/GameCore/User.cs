using GameCore.Cards;
using System.Collections.Generic;

namespace GameCore
{
    public abstract class User
    {
        public abstract string GetName();

        public abstract Card PlayCard(IEnumerable<Card> cards, PlayerState ps, Kingdom k, Phase phase, Card card = null);

        public abstract Card SelectCardToGain(KingdomWrapper wrapper, PlayerState ps, Kingdom k, Phase phase);

        // todo pak nekdy udelat test na tuto metodu...
        public abstract IEnumerable<Card> Choose(IEnumerable<Card> cards, PlayerState ps, Kingdom k, int min, int max, Phase phase, Card card);

        public IEnumerable<Card> Choose(IEnumerable<Card> cards, PlayerState ps, Kingdom k, int count, Phase phase, Card card) => Choose(cards, ps, k, count, count, phase, card);

        public abstract bool Choose(PlayerState ps, Kingdom k, Phase phase, string yup, string nay, Card card, Card decisionCard = null);

        public abstract IEnumerable<Card> ChapelTrash(PlayerState ps, Kingdom k);

        public override string ToString() => GetName();
    }

    public enum Phase { Action, Treasure, Buy, Gain, Reaction, Attack}
}
