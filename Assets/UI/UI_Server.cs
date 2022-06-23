using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Server : MonoBehaviour
{
    public List<ICE> installedIce = new List<ICE>();
    [SerializeField] GameObject icePrefab, iceInstallArea;

    public void Start()
    {
        Setup(); 
    }

    public void Setup()
    {
        foreach (Transform t in iceInstallArea.transform)
            Destroy(t.gameObject); 

        foreach(ICE ice in installedIce)
            Instantiate(ice.data.icePrefab == null ? icePrefab : ice.data.icePrefab, iceInstallArea.transform)
                .GetComponent<UI_Ice>().Setup(ice); 
    }
}
