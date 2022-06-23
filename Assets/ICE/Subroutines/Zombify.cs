using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombify : Subroutine
{
    public override void Execute()
    {
        Executable exe = new Executable(DoZombify, 2);

        exe.name = "Zombie Box >";
        exe.description = "Cycles: 2. May spawn clones on execution."; 

        Rig rig = GameObject.FindObjectOfType<Rig>();
        rig.Enqueue(exe); 
    }

    void DoZombify(Executable exe) // Zombify eats your cycles and has a chance of spawning more copies of itself. 
    {
        Debug.Log("Zombie Box pinging LAN"); 
        for(int i = 0; i < 3; i++)
        {
            if (Random.value < 0.1f)
            {
                Debug.Log("Zombie Box adds a new node to the network!");
                Execute();
            }
        }
    }
}
