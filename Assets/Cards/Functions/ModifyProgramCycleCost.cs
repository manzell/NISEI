using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

[CreateAssetMenu(menuName ="Card/ModifyProgramCycleCost")]
public class ModifyProgramCycleCost : Card
{
    public Modifier modifier; 

    public override void Play(Rig rig)
    {
        Executable exe = rig.programExecutionStack.Last(); 

        if(exe != null)
        {
            exe.cycles.modifiers.Add(modifier); 
            Debug.Log($"|OPTIMIZE> {name} reducing {exe.name} Cycle Cost by {modifier.Value(exe.cycles)}");
            exe.updateExe.Invoke(); 
        }
        else
        {
            Debug.Log($"*OPTIMIZE> WARNING > No previous programs in stack to optimize. This program has no effect"); 
        }
    }
}