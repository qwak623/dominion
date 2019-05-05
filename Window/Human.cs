using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Threading;

namespace GameCore
{
    public class Human : User
    {
        Action<IEnumerable<Card>, PlayerState, Kingdom, Phase, Card> playCard;
        Action<IEnumerable<Card>, PlayerState, Kingdom, Phase> selectCartToGain;
        Action<IEnumerable<Card>, PlayerState, Kingdom, int, int, Phase, Card> choice;
        Action<PlayerState, Kingdom, Phase, Card, string, string> alternativeChoice;
        Job job;
        string name;

        public override string GetName() => name; 

        public Human(Action<IEnumerable<Card>, PlayerState, Kingdom, Phase, Card> playCard,
                     Action<IEnumerable<Card>, PlayerState, Kingdom, Phase> selectCartToGain,
                     Action<IEnumerable<Card>, PlayerState, Kingdom, int, int, Phase, Card> choice, 
                     Action<PlayerState, Kingdom, Phase, Card, string, string> alternativeChoice, 
                     Job job, string name = nameof(Human))
        {
            this.playCard = playCard;
            this.selectCartToGain = selectCartToGain;
            this.choice = choice;
            this.alternativeChoice = alternativeChoice;
            this.job = job;
            this.name = name;
        }

        public override Card PlayCard(IEnumerable<Card> cards, PlayerState ps, Kingdom k, Phase phase, Card card)
        {
            lock (job)
            {
                job.Done = false;
                playCard(cards, ps, k, phase, card);
                while (!job.Done)
                    Monitor.Wait(job);
                return job.Result as Card;
            }
        }

        public override Card SelectCardToGain(KingdomWrapper wrapper, PlayerState ps, Kingdom k, Phase phase)
        {
            lock (job)
            {
                job.Done = false;
                selectCartToGain(wrapper.AvailableCards, ps, k, phase);
                while (!job.Done)
                    Monitor.Wait(job);
                return job.Result as Card;
            }
        }

        public override IEnumerable<Card> Choose(IEnumerable<Card> cards, PlayerState gs, Kingdom k, int min, int max, Phase phase, Card card)
        {
            lock (job)
            {
                job.Done = false;
                choice(cards, gs, k, min, max, phase, card);
                while (!job.Done)
                    Monitor.Wait(job);
                return job.Result as IEnumerable<Card>;
            }
        }

        public override bool Choose(PlayerState ps, Kingdom k, Phase phase, string yup, string nay, Card card)
        {
            lock (job)
            {
                job.Done = false;
                alternativeChoice(ps, k, phase, card, yup, nay);
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
