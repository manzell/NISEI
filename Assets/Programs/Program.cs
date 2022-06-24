using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Program : ScriptableObject
{
    public enum ProgramType { Fracter, Decoder, Killer }

    public new string name;
    public string version; 
    public ProgramType programType;
    public floatRef powerLevel, widthFactor;
    public intRef decryptionLevel;
    public intRef memoryCost;
    public intRef cycleCost; // This determines our guesses per bit // 

    public PlayBehavior installBehavior, uninstallBehavior;
    public GameObject prefab;

    public abstract Executable GetExecutable(Program program); 
    
    public void Enqueue()
    {
        // The program must create it's own executable. This means it has to be held in ProgramData
        ServerManager.currentRig.Enqueue(GetExecutable(this));
    }
}
