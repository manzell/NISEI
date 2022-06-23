using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro; 

public class UI_Executable : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI executionName, executionDescription;
    [SerializeField] Outline outline;
    public Executable exe;

    private void Start()
    {
        if (exe != null) Setup(exe); 
    }

    public void Setup(Executable exe)
    {
        this.exe = exe;
        exe.updateExe.AddListener(UpdateText); 

        switch (exe.program?.data.programType)
        {
            case ProgramData.ProgramType.Fracter:
                outline.effectColor = new Color(.2f, .8f, .2f);
                break;
            case ProgramData.ProgramType.Decoder:
                outline.effectColor = new Color(.2f, .2f, .8f);
                break;
            case ProgramData.ProgramType.Killer:
                outline.effectColor = new Color(.8f, 0f, .8f);
                break;
        }
    }

    public void UpdateText()
    {
        executionName.text = exe.name;
        executionDescription.text = exe.description;
    }
}
