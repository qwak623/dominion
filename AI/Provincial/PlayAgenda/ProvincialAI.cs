using AI.Provincial.Evolution;
using GameCore;
using GameCore.Cards;
using GameCore.Cards.GeneralCards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.Provincial.PlayAgenda
{
    // todo 
    // pet leading strategies ukladat
    // varinance v leadrech
    public class ProvincialAI : User
    {
        PlayerInfo playerInfo = new PlayerInfo();
        BuyAgenda buyAgenda;
        string name;

        // todo smazat pak
        Card lastCard;

        public override string GetName() => name;

        public ProvincialAI(BuyAgenda buyAgenda, string name = nameof(ProvincialAI))
        {
            this.buyAgenda = buyAgenda.Clone();
            this.name = name;
        }

        public override IEnumerable<Card> Choose(IEnumerable<Card> cards, PlayerState ps, Kingdom k, int min, int max, Phase phase, Card card)
        {
            return card.Decide(cards, ps, playerInfo, phase, min, max).ToList();
        }

        public override bool Choose(PlayerState ps, Kingdom k, Phase phase, string yup, string nay, Card card, Card decision)
        {
            return card.Decide(ps, playerInfo, phase, decision);
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
                var neco = Data.GetPriorityList()[(int)c.Type];

                float score = c.Score(cards, ps, phase);
                if (score >= maxScore)
                {
                    maxScore = score;
                    bestCard = c;
                }
            }

            lastCard = bestCard;
            return bestCard;
        }

        public override Card SelectCardToGain(KingdomWrapper wrapper, PlayerState ps, Kingdom k, Phase phase)
        {
            // todo colonies

            var provinces = k.GetPile(CardType.Province);
            playerInfo.TrashEstate = buyAgenda.Estates <= provinces.Count; // modifies if estate will be trashed
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
                if (tuple.Number <= 0)
                    continue;

                var card = wrapper.GetCard(tuple.Card);
                if (card == null)
                    continue;

                tuple.Number--;
                // todo, asi muze byt neefektivni, ale pri u dlouhych jedincu s nizkymy cisly asi pomuze
                if (tuple.Number == 0)
                    buyAgenda.BuyMenu.RemoveAt(i);
                else
                    buyAgenda.BuyMenu[i] = tuple; // this is a value type, i have to return the value back
                if (card.IsTreasure)
                    playerInfo.TreasureTotal += card.Coins; // todo u moneylendera se nesnizi

                // todo vyresit na pricitani a odcitani pri trash / gain
                if (card.Type == CardType.Gold)
                    playerInfo.TreasureTotal += 3;
                else if (card.Type == CardType.Silver)
                    playerInfo.TreasureTotal += 2;
                else if (card.Type == CardType.Copper)
                    playerInfo.TreasureTotal += 1;
                else if (card.Type == CardType.Moneylender)
                    playerInfo.TreasureTotal -= 1;
                else if (card.Type == CardType.Bureaucrat)
                    playerInfo.TreasureTotal += 2;
                else if (card.Type == CardType.Mine)
                    playerInfo.TreasureTotal += 1;

                return card;
            }
            return null;
        }

        public override IEnumerable<Card> ChapelTrash(PlayerState ps, Kingdom k)
        {
            var cards = ps.Hand;

            // allways trash curse
            var trash = cards.Where(c => c.Type == CardType.Curse);

            var neco = trash.ToString();

            // in the beginnig trash estate as well
            if (playerInfo.TrashEstate)
                trash = trash.Concat(cards.Where(c => c.Type == CardType.Estate));

            if (trash.Count() >= 4)
                return trash.Take(4).ToList();

            // trash only unnecesary coppers
            int coins = cards.Select(c => c.Coins).Sum() + ps.Coins;
            var card = SelectCardToGain(k.GetWrapper(coins), ps, k, Phase.Buy);
            int price = card == null ? 0 : card.Price;

            if (playerInfo.TreasureTotal > 3)
            {
                var coppers = cards.Where(c => c.Type == CardType.Copper).Take(coins - price);
                trash = trash.Concat(coppers);
                // player info update
                playerInfo.TreasureTotal -= coppers.Count();
            }

            return trash.Take(4).ToList();
        }
    }
}
