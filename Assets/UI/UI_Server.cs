using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Server : MonoBehaviour
{
    [SerializeField] GameObject icePrefab, iceInstallArea;

    public void Start()
    {
        ServerManager.turnStartEvent.AddListener(Setup); 
    }

    public void Setup()
    {
        foreach (Transform t in iceInstallArea.transform)
            Destroy(t.gameObject); 

        if(ServerManager.currentIce != null)
            Instantiate(ServerManager.currentIce.data.icePrefab == null ? icePrefab : ServerManager.currentIce.data.icePrefab, iceInstallArea.transform)
                .GetComponent<UI_Ice>().Setup(ServerManager.currentIce); 
    }
}
