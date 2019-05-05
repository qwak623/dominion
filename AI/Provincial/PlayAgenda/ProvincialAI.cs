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
        float[] priorityList = Data.GetPriorityList();
        Stack<Decision> decisions = new Stack<Decision>();
        Stack<Binary> binary = new Stack<Binary>();
        string name;

        public override string GetName() => name;

        public ProvincialAI(BuyAgenda buyAgenda, string name = nameof(ProvincialAI))
        {
            this.buyAgenda = buyAgenda.Clone();
            this.name = name;
        }

        public override IEnumerable<Card> Choose(IEnumerable<Card> cards, PlayerState ps, Kingdom k, int min, int max, Phase phase, Card attackCard)
        {
            if (phase == Phase.Attack)
                return Decisions.Decide(cards, ps, min, max, attackCard);
            var decision = decisions.Pop();
            return decision(cards, ps, min, max, phase);
        }

        public override bool Choose(PlayerState ps, Kingdom k, Phase phase, string yup, string nay, Card attackCard)
        {
            var decision = binary.Pop();
            return decision(ps, phase);
        }

        public override Card PlayCard(IEnumerable<Card> cards, PlayerState ps, Kingdom k, Phase phase, Card attackCard)
        {
            if (phase == Phase.Treasure)
                return cards.FirstOrDefault(c => c.IsTreasure);

            float maxScore = 0;
            Card bestCard = null;
            foreach (var c in cards)
            {
                // todo vypsat score a ruku pro testovani
                float score = c.Score(cards, ps, priorityList, phase);
                if (score > maxScore)
                {
                    maxScore = score;
                    bestCard = c;
                }
            }

            // push decisions
            decisions.SetComplexDecision(bestCard, playerInfo);

            return bestCard;
        }

        public override Card SelectCardToGain(KingdomWrapper wrapper, PlayerState ps, Kingdom k, Phase phase)
        {
            // todo colonies
            // todo bylo by hezke mit primo reference na piles ale to se mi ted nechce delat

            var provinces = k.GetPile(CardType.Province);
            if (buyAgenda.Provinces > provinces.Count && wrapper.GetCard(CardType.Province) != null)
                return Province.Get();

            var duchies = k.GetPile(CardType.Duchy);
            if (buyAgenda.Duchies > provinces.Count && wrapper.GetCard(CardType.Duchy) != null)
                return Duchy.Get();

            var estates = k.GetPile(CardType.Estate);
            if (buyAgenda.Estates > provinces.Count && wrapper.GetCard(CardType.Estate) != null)
                return Estate.Get();

            for (int i = 0; i < buyAgenda.BuyMenu.Count; i++)
            {
                var tuple = buyAgenda.BuyMenu[i];
                if (tuple.Number > 0)
                {
                    var card = wrapper.GetCard(tuple.Card);
                    if (card != null)
                    {
                        tuple.Number--;
                        // todo, asi muze byt neefektivni, ale pri u dlouhych jedincu s nizkymy cisly asi pomuze
                        if (tuple.Number == 0)
                            buyAgenda.BuyMenu.RemoveAt(i);
                        else
                            buyAgenda.BuyMenu[i] = tuple; // this is a value type, i have to return the value back
                        return card;
                    }
                }
            }
            return null;
        }
    }
}
