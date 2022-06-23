using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardData : ScriptableObject
{
    public new string name;
    public intRef cyclePlayCost;
    public PlayBehavior playBehavior, discardBehavior, trashBehavior, drawBehavior, flushBehavior; 


    /* Todo:  Generic Mapping of Events and Behaviors so that the card cand respond to any event
     */
}
