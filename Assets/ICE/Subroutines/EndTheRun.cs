using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Subroutine/EndTheRunRun")]
public class EndTheRun : Subroutine
{
    public override void Execute()
    {
        Debug.Log("Ending The Run"); 
        ServerManager.runEndEvent.Invoke(); 
    }
}
