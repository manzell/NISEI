using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProgramBehavior: ScriptableObject
{
    public abstract Executable GetExecutable(Program program); 
}