using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/ModifyRig")]
public class ModifyRig : Card
{
    public Modifier modifier;
    public IModifiable target; 

    public override void Play(Rig rig)
    {
        target.modifiers.Add(modifier);

        Debug.Log($"Overclock[{name}] increasing available cycles by {(int)modifier.Value(target)} qHz");
    }
}
