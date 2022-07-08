using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Program/Jack Out")]
public class JackOut : Program
{
    public override Executable GetExecutable(Program program) => new Executable(a => Execute());

    void Execute()
    {
        Debug.Log("Jacking Out"); 
        ServerManager.failedRunEvent.Invoke(); 
    }
}
