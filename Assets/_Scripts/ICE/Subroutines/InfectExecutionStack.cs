using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Subroutine/Infect")]
public class InfectExecutionStack : Subroutine
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] int minInfections, maxInfections; 

    public override void Execute()
    {
        Executable exe = new Executable(DoInfect, 2);
        ServerManager.currentRig.Enqueue(exe); 
    }

    void DoInfect(Executable exe) // Zombify eats your cycles and has a chance of spawning more copies of itself. 
    {
        float num = minInfections + Mathf.Max(curve.Evaluate(Random.value) * maxInfections - minInfections, 0);

        Debug.Log($"Adding {(int)num} bots to the network!"); 

        for(int i = 0; i < num; i++)
            Execute();
    }
}
