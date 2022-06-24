using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Subroutine/AddCard")]
public class AddCard : Subroutine
{
    public Card card; 
    public override void Execute()
    {
        card = GameObject.Instantiate(card); 

        Debug.Log($"{name} adds {card.name} to {ServerManager.currentRig.name}!");
        ServerManager.currentRig.drawDeck.Add(card); 
    }
}