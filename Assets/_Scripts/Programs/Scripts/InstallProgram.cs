using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

[CreateAssetMenu(menuName = "Program/InstallProgram")]
public class InstallProgram : Program
{
    [SerializeField] Program installProram;

    public override Executable GetExecutable() => new Executable(Install); 

    void Install(UnityAction callback)
    {
        ServerManager.currentRig.InstallProgram(installProram);
        callback?.Invoke(); 
    }
}
