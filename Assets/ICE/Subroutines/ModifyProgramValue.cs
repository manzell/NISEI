using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

[CreateAssetMenu(menuName ="Subroutine/Weaken")]
public class ModifyProgramValue : Subroutine
{
    [SerializeField] floatRef target;
    [SerializeField] Modifier modifier = new Modifier(Modifier.ModifierType.Multiplicitave, -0.15f);
    [SerializeField] intRef turnDuration; // TODO: Build duration in the Modifiers

    public override void Execute()
    {
        Program program = ServerManager.currentRig.Programs.OrderBy(p => Random.value).First(); 

        if(program != null)
        {
            target.modifiers.Add(modifier);
            ServerManager.turnEndEvent.AddListener(TickDownModifier);
            Debug.Log($"{name} reduces {ServerManager.currentRig.name} Program Strength by {modifier.Value(target)}"); 
        }

        void TickDownModifier()
        {
            turnDuration -= 1; 

            if (turnDuration <= 0)
                program.powerLevel.modifiers.Remove(modifier);

            Debug.Log($"-{modifier.Value(target)} Modifier on {program.name} removed"); 
        }
    }


}
