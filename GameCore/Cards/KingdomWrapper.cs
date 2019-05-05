using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Cards
{
    public class KingdomWrapper
    {
        public Kingdom kingdom;
        public int price;
        public bool onlyTreasures;

        public Card GetCard(CardType type)
        {
            var pile = kingdom.GetPile(type);
            if (pile != null && isAvailable(pile))
                return kingdom.GetPile(type).Card;
            return null;
        }

        public IEnumerable<Card> AvailableCards => 
            kingdom.Where(p => isAvailable(p))
            .Select(p => p.Card);

        bool isAvailable(Pile pile) => pile.Count > 0 && pile.Price <= price && (onlyTreasures && pile.Card.IsTreasure || !onlyTreasures);
    }
}
