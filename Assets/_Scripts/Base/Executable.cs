using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class Executable
{
    public string Name { get; set; }
    public string Desc { get; set; }
    
    public intRef cycles;
    public UnityEvent<Executable> updateExe = new UnityEvent<Executable>();

    UnityAction<Executable> exe;

    List<ProgramType> programTypes = new List<ProgramType>(); 
    public List<ProgramType> ProgramTypes
    {
        get { return programTypes; }
        set { programTypes = value; }
    }

    public Executable(UnityAction<Executable> exe)
    {
        this.exe = exe;
        cycles = new intRef(0);
    }
    public Executable(UnityAction<Executable> exe, int cycles)
    {
        this.exe = exe;
        this.cycles = new intRef(cycles);
    }
    public Executable(UnityAction<Executable> exe, intRef cycles)
    {
        this.exe = exe;
        this.cycles = cycles;
    }

    public void Execute() => exe.Invoke(this);

    
}
