using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq; 

[CreateAssetMenu(menuName = "Program/ModifyProgramPowerLevel")]
public class ModifyProgramPowerLevel : Program
{
    [SerializeField] Modifier modifier;
    [SerializeField] intRef turnDuration; // TODO: Build duration in the Modifiers

    public override Executable GetExecutable() => new Executable(Execute);

    public void Execute(UnityAction callback)
    {
        Program program = ServerManager.currentRig.Programs.OrderBy(p => Random.value).First(); 

        if(program != null && program is Decypher decypher)
        {
            decypher.PowerLevel.modifiers.Add(modifier);
            ServerManager.turnEndEvent.AddListener(TickDownModifier);
            Debug.Log($"{name} reduces {decypher.name} Strength by {modifier.Value(decypher.PowerLevel)}"); 
        }

        void TickDownModifier()
        {
            turnDuration -= 1; 

            if (turnDuration <= 0)
                decypher.PowerLevel.modifiers.Remove(modifier);

            Debug.Log($"-{modifier.Value(decypher.PowerLevel)} Modifier on {decypher.name} removed"); 
        }

        callback?.Invoke(); 
    }
}