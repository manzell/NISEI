using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq; 

public class UI_Rig : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI busWidth, clockSpeed;
    [SerializeField] GameObject memoryArea, memoryPrefab;

    private void Start()
    {
        Setup(ServerManager.currentRig); 
    }

    public void Setup(Rig rig)
    {
        clockSpeed.text = $"{(string)rig.clockSpeed} qHz";
        busWidth.text = $"{(string)rig.busWidth}";

        foreach (Transform t in memoryArea.transform)
            Destroy(t.gameObject); 

        for(int i = 0; i < rig.memory; i++)
        {
            GameObject mem = Instantiate(memoryPrefab, memoryArea.transform);

            if (i < rig.installedPrograms.Sum(programData => programData.memoryCost))
                mem.GetComponent<Image>().color = Color.yellow; 
        }
    }
}