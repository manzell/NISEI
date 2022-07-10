using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class ServerManager : MonoBehaviour
{
    public static ICE currentIce;
    public static Rig currentRig;

    public static int attempts, successes, cycles; 

    public static UnityEvent
        gameStartEvent = new UnityEvent(),
        turnStartEvent = new UnityEvent(),
        turnEndEvent = new UnityEvent(),
        successfulRunEvent = new UnityEvent(),
        failedRunEvent = new UnityEvent(),
        jackOutEvent = new UnityEvent(); 

    public static UnityEvent<ICE>
        iceRezEvent = new UnityEvent<ICE>(),
        iceBreakEvent = new UnityEvent<ICE>(); 

    [SerializeField] Rig startingRig; 
    [SerializeField] List<ICE> encounterIce;

    private void Awake()
    {
        DontDestroyOnLoad(this); 
        currentRig = Instantiate(startingRig);
    }

    private void Start()
    {
        turnStartEvent.AddListener(SpawnNextIce);
        iceBreakEvent.AddListener(ice => SpawnNextIce());
        Bit.DecryptEvent.AddListener(() => successes++); 
        currentRig.Boot(); 
    }

    public void StartTurn() => turnStartEvent.Invoke();

    void SpawnNextIce()
    {
        Debug.Log("Spawing Next Ice"); 
        if (currentIce == null)
        {
            if(encounterIce.Count > 0)
            {
                currentIce = Instantiate(encounterIce.First());
                encounterIce.Remove(encounterIce.First());

                currentIce.Rez(); 
            }
            else
            {
                Debug.Log("Successful Run!");
                successfulRunEvent.Invoke(); 
            }
        }
    }
}
