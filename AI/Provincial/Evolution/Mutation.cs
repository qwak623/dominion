using AI.Shared;
using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Provincial.Evolution
{
    // this class is not thread safe
    abstract class Mutation
    {
        protected ThreadSafeRandom rnd = new ThreadSafeRandom();

        public abstract void Mutate(BuyAgenda agenda, List<Card> kingdom);
    }

    class ReplaceSupplyCardMutation : Mutation
    {
        public override void Mutate(BuyAgenda agenda, List<Card> kingdom)
        {
            int i = rnd.Next(agenda.BuyMenu.Count);
            int j = rnd.Next(kingdom.Count); 

            var tuple = agenda.BuyMenu[i];
            tuple.Card = kingdom[j].Type;
            agenda.BuyMenu[i] = tuple;
        }
    }

    class ModifyPurchaseCountMutation : Mutation
    {
        public override void Mutate(BuyAgenda agenda, List<Card> kingdom)
        {
            int i = rnd.Next(agenda.BuyMenu.Count);
            
            var tuple = agenda.BuyMenu[i];
            tuple.Number += Math.Sign(rnd.Next());

            // if number = 0 card is never bought anyway
            if (tuple.Number == 0)
                agenda.BuyMenu = agenda.BuyMenu.Where(t => t.Number != 0).ToList();

            agenda.BuyMenu[i] = tuple;
        }
    }

    class SwapSupplyCardsMutation : Mutation
    {
        public override void Mutate(BuyAgenda agenda, List<Card> kingdom)
        {
            int i = rnd.Next(agenda.BuyMenu.Count);
            int j = rnd.Next(agenda.BuyMenu.Count);

            var tuple = agenda.BuyMenu[i];
            agenda.BuyMenu[i] = agenda.BuyMenu[j];
            agenda.BuyMenu[j] = tuple;
        }
    }

    class VictoryCardPurchaseMutation : Mutation
    {
        public override void Mutate(BuyAgenda agenda, List<Card> kingdom)
        {
            int i = rnd.Next(3); // TODO zjistovani jestli jsou kolonie ve hre

            switch (i)
            {
                case 0:
                    agenda.Estates += Math.Sign(rnd.Next());
                    break;
                case 1:
                    agenda.Duchies += Math.Sign(rnd.Next());
                    break;
                case 2:
                    agenda.Provinces += Math.Sign(rnd.Next());
                    break;
                default:
                    break;
            }
        }
    }

    class AddCardMutation : Mutation
    {
        public override void Mutate(BuyAgenda agenda, List<Card> kingdom)
        {
            int i = rnd.Next(agenda.BuyMenu.Count);
            int j = rnd.Next(kingdom.Count);

            agenda.BuyMenu.Insert(i, (kingdom[j].Type, rnd.Next(10)));
        }
    }
}
