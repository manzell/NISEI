using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ModifyCard : Subroutine
{
    public Modifier modifier;
    public intRef target;
    public string targetName;

    public CardSource source;
    public Order order; 
    public enum CardSource { Any, DrawDeck, Hand, Discard }
    public enum Order { First, Random, Last }

    public override void Execute()
    {
        Rig rig = ServerManager.currentRig;
        Card card; 

        switch(source)
        {
            case CardSource.DrawDeck:
                card = GetCardByOrder(order, rig.drawDeck);
                break;
            case CardSource.Hand:
                card = GetCardByOrder(order, rig.hand);
                break;
            case CardSource.Discard:
                card = GetCardByOrder(order, rig.discard);
                break;
            default:
                card = GetCardByOrder(order, rig.drawDeck.Union(rig.hand).Union(rig.discard));
                break;
        }

        if(card != null)
        {
            Debug.Log($"{name} modifies the {targetName} of {card.name} by {modifier.Value(target)}!");
            target.modifiers.Add(modifier); 
        }

        Card GetCardByOrder(Order order, IEnumerable<Card> cards)
        {
            switch (order)
            {
                case Order.First:
                    return cards.First();
                case Order.Last:
                    return cards.Last();
                default:
                    return cards.OrderBy(card => Random.value).First(); 
            }
        }
    }
}
