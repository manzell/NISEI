using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public new string name;
    public intRef cyclePlayCost;
    public PlayBehavior discardBehavior, trashBehavior, drawBehavior, flushBehavior;

    public void Play()
    {
        Rig rig = ServerManager.currentRig;
        Play(rig); 
        rig.cardPlayEvent.Invoke(this);
    }

    public virtual void Play(Rig rig) 
    { 
        // Charge the Player the Cycles
        // is the Play Behavior = The Card? 
    }
    /* Todo:  Generic Mapping of Events and Behaviors so that the card cand respond to any event
     */
}
