using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subroutine : ScriptableObject
{
    public new string name;
    public string description;

    public abstract void Execute(); 
}
