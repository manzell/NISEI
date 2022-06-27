using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

[CreateAssetMenu(menuName ="Card/ModifyProgramCycleCost")]
public class ModifyProgramCycleCost : PlayBehavior
{
    public Modifier modifier;
    public Executable target; 

    public override void Play(Rig rig, Card card)
    {
        if(target == null) 
            target = rig.programExecutionStack.Last(); 

        if(target != null)
        {
            target.cycles.modifiers.Add(modifier); 
            Debug.Log($"|OPTIMIZE> {card.name} reducing {target.name} Cycle Cost by {modifier.Value(target.cycles)}");
            target.updateExe.Invoke(); 
        }
        else
        {
            Debug.Log($"*OPTIMIZE> WARNING > No previous programs in stack to optimize. This program has no effect"); 
        }
    }
}