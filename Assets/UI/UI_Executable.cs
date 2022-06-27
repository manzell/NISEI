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
    [SerializeField] ProgramType fracter, decoder, killer; 
    public Executable exe;


    private void Start()
    {
        if (exe != null) Setup(exe); 
    }

    public void Setup(Executable exe)
    {
        this.exe = exe;
        exe.updateExe.AddListener(UpdateText); 

        if(exe.program.programTypes.Contains(fracter))
            outline.effectColor = new Color(.2f, .8f, .2f);
        if (exe.program.programTypes.Contains(decoder))
            outline.effectColor = new Color(.2f, .2f, .8f);
        if (exe.program.programTypes.Contains(killer))
            outline.effectColor = new Color(.8f, 0f, .8f);
    }

    public void UpdateText()
    {
        executionName.text = exe.name;
        executionDescription.text = exe.description;
    }
}
