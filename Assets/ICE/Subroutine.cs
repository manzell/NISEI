using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Subroutine
{
    public string name;
    public string description;

    public abstract void Execute(); 
}
