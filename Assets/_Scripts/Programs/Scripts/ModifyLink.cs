using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

[CreateAssetMenu(menuName ="Program/Modify Link")]
public class ModifyLink : Program
{
    [SerializeField] Modifier modifier;
    public override Executable GetExecutable() => new Executable(Execute);

    void Execute(UnityAction callback)
    {
        ServerManager.currentRig.link.modifiers.Add(modifier);

        if (ServerManager.currentRig.link.Value <= 0)
            ServerManager.failedRunEvent.Invoke(); 
        else
            callback?.Invoke(); 
    }
}