using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

[CreateAssetMenu(menuName = "Program/Modify Bus")]
public class ModifyBus : Program
{
    [SerializeField] Modifier modifier;

    public override Executable GetExecutable() => new Executable(callback => Execute(ServerManager.currentRig, modifier, callback)); 

    public void Execute(Rig rig, Modifier modifier, UnityAction callback)
    {
        Debug.Log($"{name} reduces {rig.name}'s Bus Width by {modifier.Value(rig.busWidth)}!");
        rig.busWidth.modifiers.Add(modifier);
        callback?.Invoke(); 
    }
}
