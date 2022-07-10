using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

[CreateAssetMenu(menuName = "Program/EndTheRun")]
public class EndTheRun : Program
{
    public override Executable GetExecutable() => new Executable(Execute);

    void Execute(UnityAction callback)
    {
        ServerManager.failedRunEvent.Invoke();
        callback?.Invoke(); 
    }
}
