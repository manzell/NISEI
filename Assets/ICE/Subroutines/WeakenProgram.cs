using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

//[CreateAssetMenu(menuName ="Subroutine/Weaken")]
public class WeakenProgram : Subroutine
{
    public intRef turns;
    public Modifier modifier = new Modifier(Modifier.ModifierType.Multiplicitave, -0.15f);

    public override void Execute()
    {
        Program programData = ServerManager.currentRig.installedPrograms.OrderBy(p => Random.value).First(); 

        if(programData != null)
        {
            programData.powerLevel.modifiers.Add(modifier);
            ServerManager.turnEndEvent.AddListener(TickDownModifier);
            Debug.Log($"{name} reduces {ServerManager.currentRig.name} Program Strength by {modifier.Value(programData.powerLevel)}"); 
        }

        void TickDownModifier()
        {
            turns -= 1; 

            if (turns <= 0)
                programData.powerLevel.modifiers.Remove(modifier);

            Debug.Log($"-{modifier.Value(programData.powerLevel)} Modifier on {programData.name} removed"); 
        }
    }


}
