using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Card/Card")]
public class Card : ScriptableObject
{
    public new string name;
    public string description; 
    [SerializeField] protected intRef playCost, drawCost;
    [SerializeField] protected List<PlayBehavior> playBehaviors; 
    [SerializeField] protected PlayBehavior discardBehavior, trashBehavior, drawBehavior, flushBehavior;

    public void Play() => Play(ServerManager.currentRig);

    protected virtual void Play(Rig rig) 
    {
        rig.ChargeCycles(playCost);

        foreach (PlayBehavior behavior in playBehaviors)// Todo - make this sequential with a callback?
            behavior.Play(rig, this);

        if (rig.Hand.Contains(this)) // TODO Make this a Rig Method
        {
            rig.Hand.Remove(this);
            rig.Discards.Add(this);
        }

        rig.cardPlayEvent.Invoke(this);
    }

    protected virtual bool CanPlay(Rig rig) => rig.cycles >= playCost; 
    public void Try(Rig rig)
    {
        if (CanPlay(rig))
            Play(rig);
        else
            Debug.Log($"Cannot play {name}");
    }

    public int GetDrawCost() => (int)drawCost.Value;
    public int GetPlayCost() => (int)playCost.Value;
}
