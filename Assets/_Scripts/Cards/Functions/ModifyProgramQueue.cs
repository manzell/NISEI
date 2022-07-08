using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

[CreateAssetMenu(menuName ="Card/ModifyProgramQueue")]
public class ModifyProgramQueue : PlayBehavior
{
    public enum TargetType { First, Last, Random }
    [SerializeField] Modifier modifier;
    [SerializeField] TargetType QueueTargetType; 

    [SerializeField] string onTargetMessage, noTargetMessage; 

    public override void Play(Rig rig, Card card)
    {
        Executable target = null; 
        switch (QueueTargetType)
        {
            case TargetType.First:
                target = rig.ExecutionStack.First();
                break; 
            case TargetType.Last:
                target = rig.ExecutionStack.Last();
                break;
            case TargetType.Random:
                target = rig.ExecutionStack.OrderBy(x => Random.value).First();
                break;
        }

        if(target != null)
        {
            target.cycles.modifiers.Add(modifier); 
            Debug.Log(onTargetMessage);
            target.updateExe.Invoke(target); 
        }
        else
        {
            Debug.Log(noTargetMessage); 
        }
    }
}