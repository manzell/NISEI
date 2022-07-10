using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq;

public class ModifyCardDrawCost : Program
{
    enum CardSource { Any, DrawDeck, Hand, Discard }
    public enum Order { First, Random, Last }

    [SerializeField] Modifier modifier;

    [SerializeField] CardSource source;
    [SerializeField] Order order; 

    public override Executable GetExecutable() => new Executable(Execute); 

    void Execute(UnityAction callback)
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
            card.DrawCost.modifiers.Add(modifier); 

        callback?.Invoke(); 
    }
}
