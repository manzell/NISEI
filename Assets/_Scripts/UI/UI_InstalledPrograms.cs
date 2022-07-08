using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InstalledPrograms : MonoBehaviour
{
    [SerializeField] GameObject installedProgramsArea;

    private void Start()
    {
        ServerManager.currentRig.installEvent.AddListener(Install);
        ServerManager.currentRig.uninstallEvent.AddListener(Uninstall); 
    }

    public void Install(Program program)
    {
        GameObject p = Instantiate(ServerManager.currentRig.programPrefab, installedProgramsArea.transform);
        p.GetComponent<UI_Program>().Setup(program); 
    }

    public void Uninstall(Program programData)
    {
        foreach(UI_Program installedProgram in installedProgramsArea.GetComponentsInChildren<UI_Program>())
            if (installedProgram.program == programData)
                Destroy(installedProgram.gameObject); 
    }
}
