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

    public Rig rig; 
    public List<ICE> ice;

    private void Awake()
    {
        currentRig = GameObject.Instantiate(rig);
        rig = currentRig; 
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
        if (currentIce == null)
        {
            if(ice.Count > 0)
            {
                currentIce = Instantiate(ice[0]); 
                ice.RemoveAt(0);
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
