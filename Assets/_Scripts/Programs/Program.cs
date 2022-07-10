using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Program : ScriptableObject
{
    public new string name;
    public string description, version;

    public intRef MemoryCost => memoryCost;
    public intRef ExecutionCost => execCost;
    public intRef InstallCost => installCost;

    [SerializeField] intRef memoryCost, execCost, installCost;
    [SerializeField] PlayBehavior installBehavior, uninstallBehavior;
    [SerializeField] GameObject prefab;

    public abstract Executable GetExecutable();     
    public void Enqueue() => ServerManager.currentRig.Enqueue(GetExecutable());
}