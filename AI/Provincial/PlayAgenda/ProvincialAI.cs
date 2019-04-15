using AI.Provincial.Evolution;
using GameCore;
using GameCore.Cards;
using GameCore.Cards.GeneralCards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.Provincial.PlayAgenda
{
    public class ProvincialAI : User
    {
        Random rnd = new Random();
        PlayerInfo playerInfo = new PlayerInfo();
        BuyAgenda buyAgenda;
        int[] priorityList = Data.GetPriorityList();
        Stack<Decision> decisions = new Stack<Decision>();

        // debug
        Card lastPlayedCard;

        public override string GetName() => nameof(ProvincialAI);

        public ProvincialAI(BuyAgenda buyAgenda)
        {
            this.buyAgenda = buyAgenda;
        }

        public override IEnumerable<Card> Choose(IEnumerable<Card> cards, PlayerState ps, Kingdom k, int min, int max, Phase phase, Card card = null)
        {
            if (phase == Phase.Attack)
                return Decisions.Decide(cards, ps, min, max, card);
            var decision = decisions.Pop();
            return decision(cards, ps, min, max, phase);
        }

        public override bool Choose()
        {
            // todo better discrete choosing
            return true;
        }

        public override Card PlayCard(IEnumerable<Card> cards, PlayerState ps, Kingdom k, Phase phase, Card card = null)
        {
            if (phase == Phase.Treasure)
                return cards.FirstOrDefault(c => c.IsTreasure);

            int maxScore = 0;
            Card bestCard = null;
            foreach (var c in cards)
            {
                int score = c.Score(cards, priorityList, phase);
                if (score > maxScore)
                {
                    maxScore = score;
                    bestCard = c;
                }
            }

            // push decisions
            decisions.SetComplexDecision(card, playerInfo);

            lastPlayedCard = card;
            return card;
        }

        public override Card SelectCardToGain(IEnumerable<Card> cards, PlayerState ps, Kingdom k)
        {
            // todo colonies
            // todo bylo by hezke mit primo reference na piles ale to se mi ted nechce delat
            // todo asi stejne jako nad timto, bylo by hezke mit kingdom jako dictionary

            // balicek nesmi byt prazdny
            if (!k.Single(p => p.Type == CardType.Province).Empty && 
                buyAgenda.Provinces > k.Where(p => p.Type == CardType.Province).Single().Count)
                return Province.Get();
            if (!k.Single(p => p.Type == CardType.Duchy).Empty && 
                buyAgenda.Duchies > k.Where(p => p.Type == CardType.Duchy).Single().Count)
                return Duchy.Get();
            if (!k.Single(p => p.Type == CardType.Estate).Empty && 
                buyAgenda.Estates > k.Where(p => p.Type == CardType.Estate).Single().Count)
                return Estate.Get();

            for (int i = 0; i < buyAgenda.BuyMenu.Count; i++)
            {
                var tuple = buyAgenda.BuyMenu[i];
                if (tuple.Number > 0)
                {
                    var card = cards.FirstOrDefault(c => c.Type == tuple.Card);
                    if (card != null)
                    {
                        tuple.Number--;
                        buyAgenda.BuyMenu[i] = tuple; // this is a value type, i have to return the value back
                        return card;
                    }
                }
            }
            return null;
        }
    }
}
