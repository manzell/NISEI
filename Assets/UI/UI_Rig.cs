using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq; 

public class UI_Rig : MonoBehaviour
{
    public Rig rig; 
    [SerializeField] TextMeshProUGUI busWidth, clockSpeed;
    [SerializeField] GameObject memoryArea, memoryPrefab;

    private void Start()
    {
        if (rig != null)
            Setup(rig); 
    }

    public void Setup(Rig rig)
    {
        this.rig = rig;
        clockSpeed.text = rig.clockSpeed + " qHz";
        busWidth.text = rig.busWidth.ToString();

        foreach (Transform t in memoryArea.transform)
            Destroy(t.gameObject); 

        for(int i = 0; i < rig.memory; i++)
        {
            GameObject mem = Instantiate(memoryPrefab, memoryArea.transform);

            if (i < rig.installedPrograms.Sum(program => program.data.memoryCost))
                mem.GetComponent<Image>().color = Color.yellow; 
        }
    }
}