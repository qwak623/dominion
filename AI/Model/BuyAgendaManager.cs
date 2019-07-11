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
        public abstract BuyAgenda Load(IEnumerable<int> cards);
        public BuyAgenda Load(List<Card> cards) => Load(cards.Select(c => (int)c.Type));

        public abstract BuyAgenda LoadBest(List<Card> cards, ILogger logger = null);

        public abstract void Save(IEnumerable<int> cards, BuyAgenda agenda);
        public void Save(List<Card> cards, BuyAgenda agenda) => Save(cards.Select(c => (int)c.Type), agenda);

        public abstract IEnumerator<BuyAgenda> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
