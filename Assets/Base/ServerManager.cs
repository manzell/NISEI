using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class ServerManager : MonoBehaviour
{
    public static ICE currentIce;
    public static Rig currentRig;

    public static UnityEvent
        gameStartEvent = new UnityEvent(),
        turnStartEvent = new UnityEvent(),
        turnEndEvent = new UnityEvent(),
        iceBreakEvent = new UnityEvent(),
        successfulRunEvent = new UnityEvent(),
        runEndEvent = new UnityEvent();

    [SerializeField] Rig startingRig; 
    [SerializeField] List<ICE> encounterIce;

    private void Awake()
    {
        currentRig = GameObject.Instantiate(startingRig);
    }

    private void Start()
    {
        turnStartEvent.AddListener(SpawnNextIce);
        iceBreakEvent.AddListener(SpawnNextIce);
        currentRig.Boot(); 
    }

    [ContextMenu("Start Turn")]
    public void StartTurn() => turnStartEvent.Invoke();

    void SpawnNextIce()
    {
        Debug.Log($"SpawnNextIce({currentIce})");
        if (currentIce == null)
        {
            if(encounterIce.Count > 0)
            {
                currentIce = Instantiate(encounterIce[0]); 
                encounterIce.RemoveAt(0);
                currentIce.OnEncounter(); 
            }
            else
            {
                Debug.Log("Successful Run!");
                successfulRunEvent.Invoke(); 
            }
        }
    }
}
