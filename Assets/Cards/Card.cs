using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Card/Card")]
public class Card : ScriptableObject
{
    public new string name;
    [SerializeField] intRef playCost;
    [SerializeField] intRef drawCost;
    [SerializeField] List<PlayBehavior> playBehaviors; 
    [SerializeField] PlayBehavior discardBehavior, trashBehavior, drawBehavior, flushBehavior;

    public void Play() => Play(ServerManager.currentRig);

    public virtual void Play(Rig rig) 
    {
        foreach(PlayBehavior behavior in playBehaviors)// Todo - make this sequential with a callback?
        {
            behavior.Play(rig, this); 
            rig.cardPlayEvent.Invoke(this);
        }
    }

    public int GetDrawCost() => (int)drawCost.Value;
    public int GetPlayCost() => (int)playCost.Value;
}
