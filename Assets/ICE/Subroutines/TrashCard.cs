using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//[CreateAssetMenu(menuName = "Subroutine/Trash")]
public class TrashCard : Subroutine
{
    public Card card; 

    public override void Execute()
    {
        Rig rig = ServerManager.currentRig; 

        if(card == null)
            card = rig.drawDeck.Union(rig.hand).Union(rig.discard).OrderBy(card => Random.value).First();

        string source = string.Empty; 

        if (rig.drawDeck.Contains(card))
            source = "Draw Deck";
        else if (rig.discard.Contains(card))
            source = "Discard Pile";
        else if (rig.hand.Contains(card))
            source = "Hand";

        Debug.Log($"{name} trashes {card.name} from {rig.name}'s {source}");

        rig.RemoveCard(card); 
    }
}
