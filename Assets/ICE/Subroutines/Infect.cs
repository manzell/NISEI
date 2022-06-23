using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Subroutine/Infect")]
public class InfectExecutionStack : Subroutine
{
    public override void Execute()
    {
        Executable exe = new Executable(DoInfect, 2);
        ServerManager.currentRig.Enqueue(exe); 
    }

    void DoInfect(Executable exe) // Zombify eats your cycles and has a chance of spawning more copies of itself. 
    {
        Debug.Log("Infectious BotNet pinging LAN"); 
        for(int i = 0; i < 3; i++)
        {
            if (Random.value < 0.1f)
            {
                Debug.Log($"[[{ServerManager.currentRig.name}]] adds a new node to the network!");
                Execute();
            }
        }
    }
}
