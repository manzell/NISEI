using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; 
using TMPro; 

public class UI_Program : MonoBehaviour
{
    public Program program;
    [SerializeField] TextMeshProUGUI programName;
    [SerializeField] Button runProgramButton; 
    [SerializeField] GameObject memoryIconArea, memoryIconPrefab;

    private void Start()
    {
        if (program != null) 
            Setup(program); 
    }

    public void Setup(Program program)
    {

        Debug.Log($"Install Program {program.name}");
        this.program = program;
        programName.text = program.name + "()";
        gameObject.name = program.name; 
        
        runProgramButton.onClick.RemoveAllListeners();
        runProgramButton.onClick.AddListener(program.Enqueue); 

        foreach(Transform t in memoryIconArea.transform)
            Destroy(t.gameObject);

        for (int i = 0; i < program.memoryCost; i++)
            Instantiate(program.prefab == null ? memoryIconPrefab : program.prefab, memoryIconArea.transform);
    }
}
