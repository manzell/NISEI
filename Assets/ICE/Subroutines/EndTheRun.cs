using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Subroutine/EndTheRun")]
public class EndTheRun : Subroutine
{
    public override void Execute()
    {
        Debug.Log("Ending The Run"); 
        ServerManager.failedRunEvent.Invoke(); 
    }
}
