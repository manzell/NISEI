using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class Executable
{
    public string name;
    public string description;
    public intRef cycles;
    public bool flushable = true;

    // Optional datapoints? 
    public Program program;
    public ICE ice;
    public Card card;

    public UnityEvent updateExe = new UnityEvent(); 
    public UnityAction<Executable> exe;

    public void Execute() => exe.Invoke(this);

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
}
