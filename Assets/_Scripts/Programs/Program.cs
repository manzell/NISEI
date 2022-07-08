using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Program : ScriptableObject
{
    public new string name;
    public string version;

    public List<ProgramType> Types => programTypes;
    public floatRef PowerLevel => powerLevel;
    public intRef MemoryCost => memoryCost;
    public intRef ExecutionCost => executionCost;

    [SerializeField] protected List<ProgramType> programTypes = new List<ProgramType>();
    [SerializeField] protected floatRef powerLevel;
    [SerializeField] protected intRef memoryCost;
    [SerializeField] protected intRef executionCost;

    [SerializeField] private PlayBehavior installBehavior, uninstallBehavior;
    [SerializeField] private GameObject prefab;

    public abstract Executable GetExecutable(Program program);     
    public void Enqueue() => ServerManager.currentRig.Enqueue(GetExecutable(this));
}