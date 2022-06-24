using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Subroutine/InstallProgram")]
public class InstallMaliciousProgram : Subroutine
{
    public Program programData;

    public override void Execute()
    {
        ServerManager.currentRig.InstallProgram(programData); 
        // TODO - Make Uninstall options for Programs
    }
}
