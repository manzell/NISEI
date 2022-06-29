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
            if(t.GetComponent<UI_Ice>() == null || t.GetComponent<UI_Ice>().ice.broken == true)
                Destroy(t.gameObject);

        // TODO Fix this so it doesn't reinstantiate a non-broken ICE each turn. 
        if(ServerManager.currentIce != null)
        {
            Instantiate(ServerManager.currentIce.icePrefab == null ? icePrefab : ServerManager.currentIce.icePrefab, iceInstallArea.transform)
                .GetComponent<UI_Ice>().Setup(ServerManager.currentIce);
        }
    }
}
