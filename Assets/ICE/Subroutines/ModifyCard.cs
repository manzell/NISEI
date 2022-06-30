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
                card = rig.GetCardByOrder(order, rig.DrawDeck);
                break;
            case CardSource.Hand:
                card = rig.GetCardByOrder(order, rig.Hand);
                break;
            case CardSource.Discard:
                card = rig.GetCardByOrder(order, rig.Discards);
                break;
            default:
                card = rig.GetCardByOrder(order, rig.GetAllCards());
                break;
        }

        if(card != null)
        {
            Debug.Log($"{name} modifies the {targetName} of {card.name} by {modifier.Value(target)}!");
            target.modifiers.Add(modifier); 
        }
    }
}
