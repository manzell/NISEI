using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

[CreateAssetMenu(menuName = "Program/ModifyClockSpeed")]
public class ModifyClockSpeed : Program
{
    [SerializeField] Modifier modifier;

    public override Executable GetExecutable() => new Executable(callback => Play(ServerManager.currentRig, callback)); 

    void Play(Rig rig, UnityAction callback)
    {
        rig.clockSpeed.modifiers.Add(modifier); 
        Debug.Log($"{name} reduced {rig.name}'s Clock Speed by {(100 * modifier.BaseValue).ToString("#.#")}%");
        callback?.Invoke(); 
    }
}
