using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

[CreateAssetMenu(menuName = "Program/AddExecutable")]
public class AddExecutable : Program
{
    [SerializeField] int minInfections, maxInfections;
    [SerializeField] AnimationCurve curve;
    Executable exe = new Executable(2);

    void DoInfect(Rig rig, UnityAction callback) // Zombify eats your cycles and has a chance of spawning more copies of itself. 
    {
        float num = minInfections + Mathf.Max(curve.Evaluate(Random.value) * maxInfections - minInfections, 0);
        Debug.Log($"Adding {(int)num} bots to the network!"); 

        for(int i = 0; i < num; i++)
            rig.Enqueue(exe);

        callback?.Invoke(); 
    }

    public override Executable GetExecutable() => new Executable(callback => DoInfect(ServerManager.currentRig, callback)); 
}
