using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Subroutine/Slow")]
public class SlowProcessor : Subroutine
{
    public Modifier modifier; 

    public override void Execute()
    {
        ServerManager.currentRig.clockSpeed.modifiers.Add(modifier); 
        Debug.Log($"{name} reduced {ServerManager.currentRig.name}'s Clock Speed by {(100 * modifier.value).ToString("#.#")}%"); 
    }
}
