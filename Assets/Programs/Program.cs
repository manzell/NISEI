using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector; 

public class Program
{
    public string name;
    public ProgramData data; 
    public Rig rig;

    public ICE targetIce => CombatManager.targetIce; 

    public Program(ProgramData data)
    {
        this.data = GameObject.Instantiate(data);
    }

    public void Enqueue()
    {
        // The program must create it's own executable. This means it has to be held in ProgramData


        rig.Enqueue(data.programBehavior.GetExecutable(this)); 
    }
}