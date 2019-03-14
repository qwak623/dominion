using GameCore;
using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    public class Provincial : User
    {
        Random rnd = new Random(23);
        public override string GetName() => "Provincial";

        public override IEnumerable<Card> Choose(IEnumerable<Card> cards, PlayerState gs, int min, int max)
        {
            return cards.OrderBy(x => rnd.Next()).Take(max);
        }

        public override bool Choose()
        {
            return true;
        }

        public override Card PlayCard(IEnumerable<Card> cards, PlayerState gs, Phase phase)
        {
            if (phase == Phase.Buy)
                return cards.OrderByDescending(x => x.Price).FirstOrDefault(x => x.Type != CardType.Estate && x.Type != CardType.Copper && (x.IsTreasure || x.IsVictory));

            return cards.OrderBy(x => rnd.Next()).FirstOrDefault();
        }
    }
}
