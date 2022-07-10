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
    [SerializeField] TextMeshProUGUI linkAmt, traceLevel;
    [SerializeField] GameObject memoryArea, memoryPrefab, cursorPrefab;
    [SerializeField] GameObject yesNoPrompt, infoBox;
    int width = 20;

    private void Start()
    {
        Setup(ServerManager.currentRig);
        
        cursorPrefab = Instantiate(cursorPrefab, transform);
        cursorPrefab.name = "Cursor";
        cursorPrefab.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity); 
        cursorPrefab.GetComponent<RectTransform>().sizeDelta = new Vector2(width, width * 2); 
    }

    public void Update()
    {
        Vector2 screenPosition = Input.mousePosition;

        Debug.Log(screenPosition);
        Debug.Log(Input.mousePosition); 

        screenPosition.x = (int)(screenPosition.x / width) * width + (width / 2);
        screenPosition.y = (int)(screenPosition.y / (width * 2)) * (width * 2) + width;

        Debug.Log(screenPosition);

        cursorPrefab.transform.position = screenPosition; 
    }

    public void Setup(Rig rig)
    {
        rig.updateRig.AddListener(SetUIText);
        rig.cardDiscardEvent.AddListener(card => UpdateMonitor(rig));
        rig.cardDrawEvent.AddListener(card => UpdateMonitor(rig));
        rig.cardPlayEvent.AddListener(card => UpdateMonitor(rig));
        rig.cardTrashEvent.AddListener(card => UpdateMonitor(rig));

        foreach (Transform t in memoryArea.transform)
            Destroy(t.gameObject);

        for (int i = 0; i < rig.memory; i++)
        {
            GameObject mem = Instantiate(memoryPrefab, memoryArea.transform);

            if (i < rig.Programs.Sum(programData => programData.MemoryCost))
                mem.GetComponent<Image>().color = Color.yellow;
        }

        rig.updateRig.Invoke(rig); 
    }

    private void SetUIText(Rig rig)
    {
        clockSpeed.text = $"{(string)rig.clockSpeed}<sup>Q</sup>";
        busWidth.text = $"{(string)rig.busWidth}";
    }

    private void UpdateMonitor(Rig rig)
    {
        drawCount.text = rig.DrawDeck.Count.ToString();
        handCount.text = rig.Hand.Count.ToString();
        discardCount.text = rig.Discards.Count.ToString();
        trashCount.text = rig.Trash.Count.ToString();
        linkAmt.text = rig.link;
        traceLevel.text = rig.trace; 
    }
}