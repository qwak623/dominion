using GameCore.Cards;
using GameCore.Cards.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GameCore
{
    /// <summary>
    /// Implements AI interface.
    /// When decision is required calls action given by user interface.
    /// Those method are invoked in to event loop.
    /// Then monitor.wait is called and game thread is waiting until job is done.
    /// Job is done when user clicks on button, that will complete the job 
    /// and calls monitor.pulse and game continues.
    /// </summary>
    public class Human : User
    {
        Action<IEnumerable<Card>, PlayerState, Kingdom, Phase, Card> playCard;
        Action<IEnumerable<Card>, PlayerState, Kingdom, Phase> selectCartToGain;
        Action<IEnumerable<Card>, PlayerState, Kingdom, int, int, Phase, Card> choice;
        Action<PlayerState, Kingdom, Phase, Card, string, string> alternativeChoice;
        Job job;
        string name;
        CancellationTokenSource tokenSource;

        public override string GetName() => name;

        public override void SetCanCelationTokenSource(CancellationTokenSource tokenSource) => this.tokenSource = tokenSource;

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
                if (tokenSource != null && tokenSource.Token.IsCancellationRequested)
                    throw new OperationCanceledException();
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
                if (tokenSource != null && tokenSource.Token.IsCancellationRequested)
                    throw new OperationCanceledException();
                return job.Result as Card;
            }
        }

        private IEnumerable<Card> Choose(IEnumerable<Card> cards, PlayerState ps, Kingdom k, int min, int max, Phase phase, Card card)
        {
            lock (job)
            {
                job.Done = false;
                choice(cards, ps, k, min, max, phase, card);
                while (!job.Done)
                    Monitor.Wait(job);
                if (tokenSource != null && tokenSource.Token.IsCancellationRequested)
                    throw new OperationCanceledException();
                return job.Result as IEnumerable<Card>;
            }
        }

        private bool Choose(PlayerState ps, Kingdom k, Phase phase, string yup, string nay, Card card, Card decisionCard = null)
        {
            lock (job)
            {
                job.Done = false;
                alternativeChoice(ps, k, phase, card, yup, nay);
                while (!job.Done)
                    Monitor.Wait(job);
                if (tokenSource != null && tokenSource.Token.IsCancellationRequested)
                    throw new OperationCanceledException();
                return (bool)job.Result;
            }
        }

        #region cards base
        public override Card BureaucratDiscard(PlayerState ps, Kingdom k) => Choose(ps.Hand.Where(c => c.IsVictory), ps, k, 1, 1, Phase.Attack, Bureaucrat.Get()).Single();

        public override List<Card> CellarDiscard(PlayerState ps, Kingdom k) => Choose(ps.Hand, ps, k, 0, ps.Hand.Count, Phase.Action, Cellar.Get()).ToList();

        public override bool ChancellorDiscard(PlayerState ps, Kingdom k) => Choose(ps, k, Phase.Action, "Do", "Don't", Chancellor.Get());

        public override List<Card> ChapelTrash(PlayerState ps, Kingdom k) => Choose(ps.Hand, ps, k, 0, 4, Phase.Action, Chapel.Get()).ToList();

        public override bool LibrarySkip(PlayerState ps, Kingdom k, Card c) => Choose(ps, k, Phase.Action, $"Skip {c.Name}", $"Keep {c.Name}", Library.Get());

        public override List<Card> MilitiaDiscard(PlayerState ps, Kingdom k, int discardCount) => Choose(ps.Hand, ps, k, discardCount, discardCount, Phase.Attack, Militia.Get()).ToList();

        public override Card MineTrash(PlayerState ps, Kingdom k) => Choose(ps.Hand.Where(c => c.IsTreasure), ps, k, 0, 1, Phase.Action, Mine.Get()).SingleOrDefault();

        public override Card RemodelTrash(PlayerState ps, Kingdom k) => Choose(ps.Hand, ps, k, 0, 1, Phase.Action, Remodel.Get()).SingleOrDefault();

        public override bool SpyDiscard(PlayerState ps, Kingdom k, Card c, Phase p) => Choose(ps, k, p, $"Discard {c.Name}", "Put it back", Spy.Get());

        public override Card ThiefChoose(PlayerState ps, Kingdom k, IEnumerable<Card> cards) => Choose(cards, ps, k, 1, 1, Phase.Action, Thief.Get()).Single();

        public override bool ThiefSteal(PlayerState ps, Kingdom k, Card c) => Choose(ps, k, Phase.Action, $"Steal {c.Name}", $"Trash {c.Name}", Thief.Get());

        public override Card ThroneRoomPlay(PlayerState ps, Kingdom k, IEnumerable<Card> cards) => Choose(cards, ps, k, 0, 1, Phase.Action, ThroneRoom.Get()).SingleOrDefault();
        #endregion
    }

    public class Job
    {
        public object Result;
        public bool Done;
    }
}
