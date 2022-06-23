using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Subroutine/AddCard")]
public class AddCard : Subroutine
{
    public CardData cardData; 
    public override void Execute()
    {
        Card card = new Card(cardData);

        Debug.Log($"{name} adds {card.data.name} to {ServerManager.currentRig.name}!");
        ServerManager.currentRig.drawDeck.Add(card); 
    }
}