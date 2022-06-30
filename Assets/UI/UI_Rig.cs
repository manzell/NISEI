using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq; 

public class UI_Rig : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI busWidth, clockSpeed;
    [SerializeField] TextMeshProUGUI drawCount, handCount, discardCount, trashCount;
    [SerializeField] GameObject memoryArea, memoryPrefab;

    private void Start()
    {
        Setup(ServerManager.currentRig); 
    }

    public void Setup(Rig rig)
    {
        rig.updateRig.AddListener(SetUIText);
        rig.updateRig.AddListener(UpdateMonitor);

        foreach (Transform t in memoryArea.transform)
            Destroy(t.gameObject);

        for (int i = 0; i < rig.memory; i++)
        {
            GameObject mem = Instantiate(memoryPrefab, memoryArea.transform);

            if (i < rig.Programs.Sum(programData => programData.memoryCost))
                mem.GetComponent<Image>().color = Color.yellow;
        }

        rig.updateRig.Invoke(rig); 
    }

    private void SetUIText(Rig rig)
    {
        clockSpeed.text = $"{(string)rig.clockSpeed} qHz";
        busWidth.text = $"{(string)rig.busWidth}";
    }

    private void UpdateMonitor(Rig rig)
    {
        drawCount.text = rig.DrawDeck.Count.ToString();
        handCount.text = rig.Hand.Count.ToString();
        discardCount.text = rig.Discards.Count.ToString();
        trashCount.text = rig.Trash.Count.ToString(); 
    }
}