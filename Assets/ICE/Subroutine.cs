using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subroutine : ScriptableObject
{
    public new string name;
    public string description;
    public int bitCount, bitDepth; 
    public List<Bit> bits { get; private set; } = new List<Bit>(); 

    public abstract void Execute();
}
