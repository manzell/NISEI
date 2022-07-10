using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

[System.Serializable]
public class Executable
{
    private UnityAction<UnityAction> exe;
    private UnityAction callback;

    public string Name, Desc;    
    public intRef cycles { get; private set; }
    public Program program { get; private set; }
    public UnityEvent<Executable> updateExe { get; private set; } = new UnityEvent<Executable>();

    public Executable(int cycles)
    {
        this.cycles = new intRef(cycles);
    }
    public Executable(UnityAction<UnityAction> exe)
    {
        this.exe = exe;
        cycles = new intRef(0);
    }
    public Executable(UnityAction<UnityAction> exe, int cycles)
    {
        this.exe = exe;
        this.cycles = new intRef(cycles);
    }
    public Executable(UnityAction<UnityAction> exe, intRef cycles)
    {
        this.exe = exe;
        this.cycles = cycles;
    }

    public void Set(UnityAction<UnityAction> exe) => this.exe = exe;
    public void SetCallback(UnityAction action) => callback = action; 

    public void Execute() => exe.Invoke(callback);

    
}
