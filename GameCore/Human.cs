using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameCore
{
    public class Human : User
    {
        public Human(Action<IEnumerable<Card>, PlayerState, Phase> playCard, Action<IEnumerable<Card>, PlayerState, int, int> choice, Action alternativeChoice, Job job)
        {
            this.playCard = playCard;
            this.choice = choice;
            this.alternativeChoice = alternativeChoice;
            this.job = job;
        }

        public override Card PlayCard(IEnumerable<Card> cards, PlayerState gs, Phase phase)
        {
            Card selectedCard;
            lock (job)
            {
                job.Done = false;
                playCard(cards, gs, phase);
                while (!job.Done)
                    Monitor.Wait(job);
                selectedCard = job.Result as Card;
            }
            return selectedCard;
        }

        public override IEnumerable<Card> Choose(IEnumerable<Card> cards, PlayerState gs, int min, int max)
        {
            lock (job)
            {
                job.Done = false;
                choice(cards, gs, min, max);
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

        public override string GetName() => "Human";
    }

    public class Job
    {
        public object Result;
        public bool Done;
    }
}
