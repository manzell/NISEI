using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Program/Program")]
public abstract class Program : ScriptableObject
{
    public new string name;
    public string version; 
    public List<ProgramType> programTypes;
    public floatRef powerLevel;
    public intRef memoryCost;
    public intRef executionCost;

    public PlayBehavior installBehavior, uninstallBehavior;
    public GameObject prefab;

    public abstract Executable GetExecutable(Program program);     
    public void Enqueue() => ServerManager.currentRig.Enqueue(GetExecutable(this));
}
