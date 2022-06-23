using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Overclock : PlayBehavior
{
    public floatRef speedIncrease; 

    public override void Do(Rig rig, Card card)
    {
        int speedEnhancement = (int)(rig.clockSpeed * speedIncrease.baseValue);
        rig.clockSpeed += speedEnhancement;
        ServerManager.turnEndEvent.AddListener(() => rig.clockSpeed -= speedEnhancement);

        Debug.Log($"Overclock[{card.data.name}] increasing available cycles by {speedEnhancement} qHz");
    }
}
