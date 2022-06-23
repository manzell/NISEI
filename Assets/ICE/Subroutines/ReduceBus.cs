using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Subroutine/ReduceDraw")]
public class ReduceBus : Subroutine
{
    public intRef busReductionAmount = new intRef(0); 

    public override void Execute()
    {
        Debug.Log($"{name} reduces {ServerManager.currentRig.name} Bus Width by {busReductionAmount}!");
        ServerManager.currentRig.busWidth.Value -= busReductionAmount; 
    }
}
