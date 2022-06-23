using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//[CreateAssetMenu(menuName = "Subroutine/Bloat")]
public class BloatCard : Subroutine
{
    public intRef cycleCostIncrease; 
    public override void Execute()
    {
        // Bloat Card takes a random card and increases it's Cycle Play Cost
        Rig rig = ServerManager.currentRig;

        Card card = rig.drawDeck.Union(rig.hand).Union(rig.discard).OrderBy(card => Random.value).First(); 

        if(card != null)
        {
            Debug.Log($"{name} increases the Play Cost of {card.data.name} by {cycleCostIncrease}!");
            card.data.cyclePlayCost.Value += cycleCostIncrease; 
        }
    }
}
