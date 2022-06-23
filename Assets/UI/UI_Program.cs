using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; 
using TMPro; 

public class UI_Program : MonoBehaviour
{
    public Program program;
    [SerializeField] TextMeshProUGUI programName, cycleCost, complexity;
    [SerializeField] Button runProgramButton; 
    [SerializeField] GameObject memoryIconArea, memoryIconPrefab;

    private void Start()
    {
        if (program != null) 
            Setup(program); 
    }

    public void Setup(Program program)
    {
        this.program = program;
        programName.text = program.data.name + "()";
        gameObject.name = program.data.name; 
        cycleCost.text = $"Cycle Cost: {program.data.cycleCost}<br><br>Complexity: {program.data.decryptionLevel}";
        
        runProgramButton.onClick.RemoveAllListeners();
        runProgramButton.onClick.AddListener(program.Enqueue); 

        foreach(Transform t in memoryIconArea.transform)
            Destroy(t.gameObject);

        for (int i = 0; i < program.data.memoryCost; i++)
            Instantiate(program.data.prefab == null ? memoryIconPrefab : program.data.prefab, memoryIconArea.transform);
    }
}
