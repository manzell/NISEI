using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; 
using TMPro; 

public class UI_Program : MonoBehaviour
{
    public Program programData;
    [SerializeField] TextMeshProUGUI programName, cycleCost, complexity;
    [SerializeField] Button runProgramButton; 
    [SerializeField] GameObject memoryIconArea, memoryIconPrefab;

    private void Start()
    {
        if (programData != null) 
            Setup(programData); 
    }

    public void Setup(Program programData)
    {
        this.programData = programData;
        programName.text = programData.name + "()";
        gameObject.name = programData.name; 
        cycleCost.text = $"Cycle Cost: {(string)programData.cycleCost}<br><br>Complexity: {(string)programData.decryptionLevel}";
        
        runProgramButton.onClick.RemoveAllListeners();
        runProgramButton.onClick.AddListener(programData.Enqueue); 

        foreach(Transform t in memoryIconArea.transform)
            Destroy(t.gameObject);

        for (int i = 0; i < programData.memoryCost; i++)
            Instantiate(programData.prefab == null ? memoryIconPrefab : programData.prefab, memoryIconArea.transform);
    }
}
