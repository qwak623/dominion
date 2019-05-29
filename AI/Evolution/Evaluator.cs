﻿using GameCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCore.Cards;

namespace AI.Evolution
{
    public abstract class Evaluator
    {
        public abstract double Evaluate(BuyAgenda agenda, BuyAgenda[] leaders, List<Card> k, int minGames, int maxGames);
    }
}
