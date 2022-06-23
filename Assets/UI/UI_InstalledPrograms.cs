using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InstalledPrograms : MonoBehaviour
{
    [SerializeField] GameObject installedProgramsArea, installedProgramPrefab;

    private void Start()
    {
        FindObjectOfType<Rig>().installEvent.AddListener(Install);
        FindObjectOfType<Rig>().uninstallEvent.AddListener(Uninstall); 
    }

    public void Install(Program program)
    {
        GameObject p = Instantiate(installedProgramPrefab, installedProgramsArea.transform);
        p.GetComponent<UI_Program>().Setup(program); 
    }

    public void Uninstall(Program program)
    {
        foreach(UI_Program installedProgram in installedProgramsArea.GetComponentsInChildren<UI_Program>())
            if (installedProgram.program == program)
                Destroy(installedProgram.gameObject); 
    }
}
