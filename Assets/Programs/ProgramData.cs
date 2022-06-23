using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProgramData : ScriptableObject
{
    public enum ProgramType { Fracter, Decoder, Killer }

    public new string name;
    public string version; 
    public ProgramType programType;
    public int decryptionLevel;
    public int memoryCost;
    public int cycleCost; // This determines our guesses per bit // 

    public ProgramBehavior programBehavior;
    public PlayBehavior installBehavior, uninstallBehavior;

    public GameObject prefab; 
}