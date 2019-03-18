using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Threading;

namespace GameCore
{
    public class Human : User
    {
        Action<IEnumerable<Card>, PlayerState, Phase, string> playCard;
        // set, min, max
        Action<IEnumerable<Card>, PlayerState, int, int, Phase, string> choice;
        Action alternativeChoice;
        Job job;

        public override string GetName() => "Human";

        public Human(Action<IEnumerable<Card>, PlayerState, Phase, string> playCard, 
                     Action<IEnumerable<Card>, PlayerState, int, int, Phase, string> choice, 
                     Action alternativeChoice, Job job)
        {
            this.playCard = playCard;
            this.choice = choice;
            this.alternativeChoice = alternativeChoice;
            this.job = job;
        }

        public override Card PlayCard(IEnumerable<Card> cards, PlayerState ps, Phase phase, string cardName = null)
        {
            Card selectedCard;
            lock (job)
            {
                job.Done = false;
                playCard(cards, ps, phase, cardName);
                while (!job.Done)
                    Monitor.Wait(job);
                selectedCard = job.Result as Card;
            }
            return selectedCard;
        }

        public override IEnumerable<Card> Choose(IEnumerable<Card> cards, PlayerState gs, int min, int max, Phase phase, string desc)
        {
            lock (job)
            {
                job.Done = false;
                choice(cards, gs, min, max, phase, desc);
                while (!job.Done)
                    Monitor.Wait(job);
                return job.Result as IEnumerable<Card>;
            }
        }

        public override bool Choose()
        {
            lock (job)
            {
                job.Done = false;
                alternativeChoice();
                while (!job.Done)
                    Monitor.Wait(job);
                return (bool)job.Result;
            }
        }
    }

    public class Job
    {
        public object Result;
        public bool Done;
    }
}
