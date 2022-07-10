using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Card/Card")]
public class Card : ScriptableObject
{
    public new string name;
    public string description;

    public intRef PlayCost => playCost;
    public intRef DrawCost => drawCost; 

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
    public bool Try(Rig rig)
    {
        bool can = CanPlay(rig); 
        if (can)
            Play(rig);
        else
            Debug.Log($"Cannot play {name}");

        return can; 
    }

    public int GetDrawCost() => (int)drawCost.Value;
    public int GetPlayCost() => (int)playCost.Value;
}
