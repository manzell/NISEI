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
        if(card == null)
            card = ServerManager.currentRig.GetAllCards().OrderBy(card => Random.value).First();
        if(card != null)
        {
            Debug.Log($"{name} trashes {card.name} from the {ServerManager.currentRig.SourceDeck(card)}");
            ServerManager.currentRig.RemoveCard(card);
        }
    }
}
