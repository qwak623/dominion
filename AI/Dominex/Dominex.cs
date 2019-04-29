using GameCore;
using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Dominex
{
    public class Dominex : User
    {
        public override IEnumerable<Card> Choose(IEnumerable<Card> cards, PlayerState ps, Kingdom k, int min, int max, Phase phase, Card card = null)
        {
            throw new NotImplementedException();
        }

        public override bool Choose()
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            throw new NotImplementedException();
        }

        public override Card PlayCard(IEnumerable<Card> cards, PlayerState ps, Kingdom k, Phase phase, Card card = null)
        {
            throw new NotImplementedException();
        }

        public override Card SelectCardToGain(KingdomWrapper wrapper, PlayerState ps, Kingdom k, Phase phase)
        {
            throw new NotImplementedException();
        }
    }
}
