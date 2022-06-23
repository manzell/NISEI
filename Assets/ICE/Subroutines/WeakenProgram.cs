using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

//[CreateAssetMenu(menuName ="Subroutine/Weaken")]
public class WeakenProgram : Subroutine
{
    public Modifier modifier = new Modifier(Modifier.ModifierType.Multiplicitave, -0.15f);
    public int turns = 2; 

    public override void Execute()
    {
        Program program = ServerManager.currentRig.installedPrograms.OrderBy(p => Random.value).First(); 

        if(program != null)
        {
            program.data.powerLevel.modifiers.Add(modifier);
            ServerManager.turnEndEvent.AddListener(TickDownModifier);
            Debug.Log($"{name} reduces {ServerManager.currentRig.name} Program Strength by {modifier.Value(program.data.powerLevel)}"); 
        }


        void TickDownModifier()
        {
            turns--;

            if (turns <= 0)
                program.data.powerLevel.modifiers.Remove(modifier);

            Debug.Log($"-{modifier.Value(program.data.powerLevel)} Modifier on {program.data.name} removed"); 
        }
    }


}
