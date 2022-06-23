using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

[CreateAssetMenu]
public class Optimize : PlayBehavior
{
    public override void Do(Rig rig, Card card)
    {
        Executable exe = rig.programExecutionStack.Last(); 

        if(exe != null)
        {
            Debug.Log($"|OPTIMIZE> {card.data.name} reducing {exe.name} Cycle Cost by {exe.cycles.Value / 2}");
            exe.cycles.Value = exe.cycles.Value / 2;
            exe.updateExe.Invoke(); 
        }
        else
        {
            Debug.Log($"*OPTIMIZE> WARNING > No previous programs in stack to optimize. This program has no effect"); 
        }

    }
}
