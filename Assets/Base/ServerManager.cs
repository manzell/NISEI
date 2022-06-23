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

    public List<ICEdata> ice;

    private void Start()
    {
        turnStartEvent.AddListener(SpawnNextIce);
        iceBreakEvent.AddListener(SpawnNextIce); 
    }

    void SpawnNextIce()
    {
        if (currentIce == null)
        {
            if(ice.Count > 0)
            {
                currentIce = new ICE(ice.First());
                ice.Remove(currentIce.data);
            }
            else
            {
                Debug.Log("Successful Run!");
                successfulRunEvent.Invoke(); 
            }
        }
    }
}
