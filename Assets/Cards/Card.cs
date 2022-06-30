using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Card/Card")]
public class Card : ScriptableObject
{
    public new string name;
    public intRef cyclePlayCost;
    public intRef drawCost;
    public List<PlayBehavior> playBehaviors; 
    public PlayBehavior discardBehavior, trashBehavior, drawBehavior, flushBehavior;

    public void Play() => Play(ServerManager.currentRig);

    public virtual void Play(Rig rig) 
    {
        foreach(PlayBehavior behavior in playBehaviors)// Todo - make this sequential with a callback?
        {
            behavior.Play(rig, this); 
            rig.cardPlayEvent.Invoke(this);
        }
    }
    /* Todo:  Generic Mapping of Events and Behaviors so that the card cand respond to any event
     */
}
