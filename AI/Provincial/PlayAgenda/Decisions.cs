﻿using GameCore;
using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.Provincial.PlayAgenda
{
    delegate IEnumerable<Card> Decision(IEnumerable<Card> cards, PlayerState ps, int min, int max, Phase phase);

    static class Decisions
    {
        // todo mozna by bylo hezke vyresit visitorem ale zase se mi nelibi aby karty neco vedely o ui
        public static void SetComplexDecision(this Stack<Decision> decisions, Card card, PlayerInfo pi)
        {
            if (card == null)
                return;

            switch (card.Type)
            {
                case CardType.Cellar:
                    decisions.Push((hand, ps, min, max, phase) =>
                    {
                        // todo obcas se hodi zahodit i jine karty nez jen body
                        return hand.Where(c => c.IsVictory && !c.IsTreasure && !c.IsAction);
                    });
                    break;
                case CardType.Chancellor:
                    break;
                case CardType.Chapel:
                    decisions.Push((hand, ps, min, max, phase) =>
                    {  
                        var trash = hand.Where(c => c.Type == CardType.Curse);
                        if (pi.TreasureTotal >= 7)
                            trash.Concat(hand.Where(c => c.Type == CardType.Copper));
                        if (true) // todo tady by to chtělo nejakou parametrickou podminku jestli se chceme zbavovat statku nebo ne
                            trash.Concat(hand.Where(c => c.Type == CardType.Estate));
                        return trash.Take(4);
                        // todo taky neco jestli se nezbavovat i dražších karet ktere už nepotřebuji
                        // moneylender kdyz nemam copper v balicku
                        // mine kdyz uz mam jen zlataky
                        // dalsi chapel, kdyz už jsem se zbavil všeho co jsem chtěl
                        // pri market square se muze hodit zbavovat se skoro vseho
                    });
                    break;
                case CardType.Mine:
                    decisions.Push((treasures, ps, min, max, phase) =>
                    {
                        var c = treasures.Where(a => a.Type == CardType.Silver).Take(1);
                        if (c == null)
                            return treasures.OrderBy(a => a.Price).Take(1);
                        return c;
                    });
                    break;
                case CardType.Remodel:
                    decisions.Push((hand, ps, min, max, phase) =>
                    {
                        var trash = hand.Where(c => c.Type == CardType.Curse);
                        if (pi.TreasureTotal >= 7)
                            trash.Concat(hand.Where(c => c.Type == CardType.Copper));
                        if (true) // todo tady by to chtělo nejakou parametrickou podminku jestli se chceme zbavovat statku nebo ne
                            trash.Concat(hand.Where(c => c.Type == CardType.Estate));
                        // todo neco s priority at muzu predelavat i jine veci
                        // treba milice ke konci hry uz vůbec neskodi => predelat na zlatak nebo vevodstvi
                        return trash.Take(1);
                    });
                    break;
                default:
                    break;
            }
        }

        public static IEnumerable<Card> Decide(IEnumerable<Card> cards, PlayerState ps, int min, int max, Card card)
        {
            switch (card.Type)
            {
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
                                var prior = Data.GetPriorityList();
                                card = (from c in cards
                                        let m = cards.Min(a => Score(a, hand, prior, Phase.Attack))
                                        where m == Score(c, hand, prior, Phase.Attack)
                                        select c).FirstOrDefault();
                            }

                            discards.AddLast(card);
                            hand.Remove(card);
                        }
                        return discards;
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        public static int Score(this Card card, IEnumerable<Card> hand, int[] priorityList, Phase phase)
        {
            switch (phase)
            {
                case Phase.Attack:
                case Phase.Action:

                    if (card.Type == CardType.Moneylender && hand.Contains(CardType.Copper))
                        return -1;
                    // todo cardtype.mine


                    // todo ostatni karty co tam ma (soubor playerHeuristics.cpp)

                    return priorityList[(int)card.Type] + 2;

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
