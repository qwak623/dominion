using GameCore;
using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.Provincial.PlayAgenda
{
    delegate IEnumerable<Card> Decision(IEnumerable<Card> cards, PlayerState ps, int min, int max, Phase phase);
    delegate bool Binary(PlayerState ps, Card card, Phase phase);

    static class Decisions
    {
        public static IEnumerable<Card> Decide(this Card card, IEnumerable<Card> cards, PlayerState ps, PlayerInfo pi, Phase phase, int min, int max)
        {
            if (card == null)
                return null;

            switch (card.Type)
            {
                case CardType.Bureaucrat:
                    return cards.Where(c => c.IsVictory).Take(1);
                case CardType.Cellar:
                    return cards.Where(c => c.IsVictory && !c.IsTreasure && !c.IsAction);
                case CardType.Chapel:
                    // todo smazat pak
                    throw new NotSupportedException();
                case CardType.Militia:
                    {
                        var hand = cards.ToList();
                        var discards = new LinkedList<Card>();
                        while (discards.Count < min)
                        {
                            // first choice is random victory card
                            card = cards.FirstOrDefault(c => c.IsVictory);

                            // kdyz nemam victory tak vyberu nejzbytecnejsi kartu
                            if (card == null)
                            {
                                card = (from c in cards
                                        let m = cards.Min(a => Score(a, hand, ps, Phase.Attack))
                                        where m == Score(c, hand, ps, Phase.Attack)
                                        select c).FirstOrDefault();
                            }

                            discards.AddLast(card);
                            hand.Remove(card);
                        }
                        return discards;
                    }
                case CardType.Mine:
                    { // todo neefektivni a nepromyslene
                        var c = cards.Where(a => a.Type == CardType.Silver).Take(1);
                        if (c == null)
                            return cards.OrderBy(a => a.Price).Take(1);
                        return c;
                    };
                case CardType.Remodel:
                    {
                        var trash = cards.Where(c => c.Type == CardType.Curse);
                        if (pi.TreasureTotal >= 7)
                            trash.Concat(cards.Where(c => c.Type == CardType.Copper));
                        if (true) // todo tady by to chtělo nejakou parametrickou podminku jestli se chceme zbavovat statku nebo ne
                            trash.Concat(cards.Where(c => c.Type == CardType.Estate));
                        // todo neco s priority at muzu predelavat i jine veci
                        // treba milice ke konci hry uz vůbec neskodi => predelat na zlatak nebo vevodstvi
                        return trash.Take(1);
                    }
                case CardType.Thief:
                    return cards.OrderByDescending(c => c.Price).Take(1);
                case CardType.ThroneRoom:
                    {
                        var selected = (from c in cards
                                        let m = cards.Min(a => Score(a, cards, ps, Phase.Attack))
                                        where m == Score(c, cards, ps, Phase.Attack)
                                        select c).Take(1);
                        return selected;
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        public static bool Decide(this Card card, PlayerState ps, PlayerInfo pi, Phase phase, Card decision)
        {
            switch (card.Type)
            {
                case CardType.Chancellor:
                    return false;
                case CardType.Library:
                    return ps.Actions == 0;
                case CardType.Spy:
                    // todo is victory nebude fungovat az tu budou body s akcemi a tak
                    if (phase == Phase.Attack)
                        return !(card.IsVictory || card.Type == CardType.Copper);
                    else if (phase == Phase.Action)
                        return (card.IsVictory || card.Type == CardType.Copper);
                    else
                        throw new NotSupportedException();
                case CardType.Thief:
                    return card.Price >= 3;
                default:
                    throw new NotImplementedException();
            }
        }

        public static float Score(this Card card, IEnumerable<Card> cards, PlayerState ps, Phase phase)
        {
            float result = 0;
            if (card.AddActions >= 1 && ps.Actions == 1)
                result += 100;
            switch (phase)
            {
                case Phase.Attack:
                case Phase.Action:
                    if (card.Type == CardType.Chapel)
                        return result + cards.Where(c => c.Type == CardType.Curse).Count() * 3 + Data.GetPriorityList()[(int)card.Type];
                    if (card.Type == CardType.Library)
                        return result + -1.5f + 3 * (7 - ps.Hand.Count);
                    if (card.Type == CardType.Moneylender && ps.Hand.Contains(CardType.Copper))
                        return -1;
           
                    // todo cardtype.mine

                    // todo ostatni karty co tam ma (soubor playerHeuristics.cpp)

                    return Data.GetPriorityList()[(int)card.Type];
                case Phase.Treasure:
                    break;
                case Phase.Buy:
                    break;
                case Phase.Reaction:
                    break;
                default:
                    break;
                    
            }

            return -1;
        }
    }
}