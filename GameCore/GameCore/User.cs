using GameCore.Cards;
using System.Collections.Generic;

namespace GameCore
{
    public abstract class User
    {
        public abstract string GetName();

        public abstract Card PlayCard(IEnumerable<Card> cards, PlayerState ps, Kingdom k, Phase phase, Card card = null);

        public abstract Card SelectCardToGain(KingdomWrapper wrapper, PlayerState ps, Kingdom k, Phase phase);

        #region cards base
        public abstract IEnumerable<Card> CellarDiscard(PlayerState ps, Kingdom k);

        public abstract Card BureaucratDiscard(PlayerState ps, Kingdom k);

        public abstract bool ChancellorDiscard(PlayerState ps, Kingdom k);

        public abstract IEnumerable<Card> ChapelTrash(PlayerState ps, Kingdom k);

        public abstract bool LibrarySkip(PlayerState ps, Kingdom k, Card c);

        public abstract IEnumerable<Card> MilitiaDiscard(PlayerState ps, Kingdom k, int discardCount);

        public abstract Card MineTrash(PlayerState ps, Kingdom k);

        public abstract Card RemodelTrash(PlayerState ps, Kingdom k);

        public abstract bool SpyDiscard(PlayerState ps, Kingdom k, Card c, Phase p);

        public abstract Card ThiefChoose(PlayerState ps, Kingdom k, IEnumerable<Card> cards);

        public abstract bool ThiefSteal(PlayerState ps, Kingdom k, Card c);

        public abstract Card ThroneRoomPlay(PlayerState ps, Kingdom k, IEnumerable<Card> cards);
        #endregion

        public override string ToString() => GetName();
    }

    public enum Phase { Action, Treasure, Buy, Gain, Reaction, Attack}
}
