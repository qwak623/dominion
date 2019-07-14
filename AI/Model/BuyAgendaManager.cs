using GameCore;
using GameCore.Cards;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// todo hodit model asi do ai, novou slozku
namespace AI.Model
{
    public abstract class BuyAgendaManager : IEnumerable<BuyAgenda>
    {
        public abstract BuyAgenda Load(IEnumerable<Card> cards);
        public BuyAgenda Load(IEnumerable<int> cards) => Load(cards.Select(c => Card.Get((CardType)c)));

        public abstract BuyAgenda LoadBest(List<Card> cards, ILogger logger = null);

        public abstract void Save(IEnumerable<Card> cards, BuyAgenda agenda);
        public void Save(IEnumerable<int> cards, BuyAgenda agenda) => Save(cards.Select(c => Card.Get((CardType)c)), agenda);

        public abstract IEnumerator<BuyAgenda> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
