using GameCore;
using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Trivial
{
    public class MilitialAI : User
    {
        Random rnd = new Random(23);
        public override string GetName() => nameof(MilitialAI);

        public override IEnumerable<Card> Choose(IEnumerable<Card> cards, PlayerState gs, Kingdom k, int min, int max, Phase phase, Card card = null)
        {
            return cards.OrderBy(x => rnd.Next()).Take(max);
        }

        public override bool Choose(PlayerState ps, Kingdom k, Phase p, string yup, string nay, Card c) => rnd.Next() > 0;

        public override Card PlayCard(IEnumerable<Card> cards, PlayerState gs, Kingdom k, Phase phase, Card card = null)
        {
            if (phase == Phase.Buy)
                return cards.FirstOrDefault(x => x.Type == CardType.Militia);
            //return cards.OrderByDescending(x => x.Price).FirstOrDefault(x => x.Type != CardType.Estate && x.Type != CardType.Copper);

            return cards.OrderBy(x => rnd.Next()).FirstOrDefault();
        }

        public override Card SelectCardToGain(KingdomWrapper wrapper, PlayerState ps, Kingdom k, Phase phase)
        {
            var militia = wrapper.GetCard(CardType.Militia);
            return militia ?? wrapper.AvailableCards.OrderByDescending(c => c.Price).FirstOrDefault();
        }
    }
}
